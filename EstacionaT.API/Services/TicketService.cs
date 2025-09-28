using EstacionaT.Domain.Entities;
using EstacionaT.Domain.Services;
using EstacionaT.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EstacionaT.API.Services;

public class TicketService : ITicketService
{
    private readonly ParkingDbContext _context;

    public TicketService(ParkingDbContext context)
    {
        _context = context;
    }

    public async Task<Ticket> CreateTicketAsync()
    {
        var ticket = new Ticket
        {
            EntryTime = DateTime.UtcNow,
            IsPaid = false,
            Validated = false
        };

        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();
        return ticket;
    }

    public async Task<bool> PayTicketAsync(int ticketId)
    {
        var ticket = await _context.Tickets.FindAsync(ticketId);
        if (ticket == null)
        {
            return false;
        }

        ticket.IsPaid = true;
        ticket.PaymentTime = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ValidateExitAsync(int ticketId)
    {
        var ticket = await _context.Tickets.FindAsync(ticketId);
        if (ticket == null || ticket.Validated)
        {
            return false;
        }

        var settings = await _context.Settings.FirstAsync();
        var trafficStatus = await _context.TrafficStatus.FirstAsync();
        var now = DateTime.UtcNow;

        var stayDuration = (now - ticket.EntryTime).TotalMinutes;

        // Rule: Free exit if stay is less than free minutes
        if (stayDuration < settings.FreeMinutes)
        {
            ticket.Validated = true;
            ticket.ExitTime = now;
            await _context.SaveChangesAsync();
            return true;
        }

        // Rule: Paid ticket with tolerance
        if (ticket.IsPaid && ticket.PaymentTime.HasValue)
        {
            var timeAfterPayment = (now - ticket.PaymentTime.Value).TotalMinutes;
            var totalTolerance = settings.ToleranceMinutes;

            // Rule: Congestion mode
            if (trafficStatus.IsCongestionActive)
            {
                if (trafficStatus.UnlimitedMode)
                {
                    ticket.Validated = true;
                    ticket.ExitTime = now;
                    await _context.SaveChangesAsync();
                    return true;
                }
                totalTolerance += trafficStatus.ExtraToleranceMinutes;
            }

            if (timeAfterPayment <= totalTolerance)
            {
                ticket.Validated = true;
                ticket.ExitTime = now;
                await _context.SaveChangesAsync();
                return true;
            }
        }

        return false;
    }
}