using EstacionaT.API.Services;
using EstacionaT.Domain.Entities;
using EstacionaT.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EstacionaT.Tests;

public class TicketServiceTests
{
    private ParkingDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ParkingDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var dbContext = new ParkingDbContext(options);
        dbContext.Database.EnsureCreated();
        return dbContext;
    }

    [Fact]
    public async Task ValidateExit_FreeExit_UnderFreeMinutes()
    {
        // Arrange
        var dbContext = GetInMemoryDbContext();
        var ticketService = new TicketService(dbContext);
        var ticket = await ticketService.CreateTicketAsync();

        // Act
        var result = await ticketService.ValidateExitAsync(ticket.Id);

        // Assert
        Assert.True(result);
        var validatedTicket = await dbContext.Tickets.FindAsync(ticket.Id);
        Assert.NotNull(validatedTicket);
        Assert.True(validatedTicket.Validated);
    }

    [Fact]
    public async Task ValidateExit_PaidTicket_WithTolerance()
    {
        // Arrange
        var dbContext = GetInMemoryDbContext();
        var ticketService = new TicketService(dbContext);
        var ticket = await ticketService.CreateTicketAsync();
        await ticketService.PayTicketAsync(ticket.Id);

        // Act
        var result = await ticketService.ValidateExitAsync(ticket.Id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ValidateExit_ExpiredTicket_WithoutCongestion_Blocked()
    {
        // Arrange
        var dbContext = GetInMemoryDbContext();
        var settings = await dbContext.Settings.FirstAsync();
        settings.ToleranceMinutes = 1;
        await dbContext.SaveChangesAsync();

        var ticketService = new TicketService(dbContext);
        var ticket = await ticketService.CreateTicketAsync();
        // Ensure the ticket is old enough to require payment
        ticket.EntryTime = DateTime.UtcNow.AddMinutes(-30);
        await dbContext.SaveChangesAsync();

        await ticketService.PayTicketAsync(ticket.Id);

        var paidTicket = await dbContext.Tickets.FindAsync(ticket.Id);
        Assert.NotNull(paidTicket);
        paidTicket.PaymentTime = DateTime.UtcNow.AddMinutes(-5); // 5 minutes ago
        await dbContext.SaveChangesAsync();

        // Act
        var result = await ticketService.ValidateExitAsync(ticket.Id);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task ValidateExit_ExpiredTicket_WithCongestion_Allowed()
    {
        // Arrange
        var dbContext = GetInMemoryDbContext();
        var settings = await dbContext.Settings.FirstAsync();
        settings.ToleranceMinutes = 1;
        var trafficStatus = await dbContext.TrafficStatus.FirstAsync();
        trafficStatus.IsCongestionActive = true;
        trafficStatus.ExtraToleranceMinutes = 10;
        await dbContext.SaveChangesAsync();

        var ticketService = new TicketService(dbContext);
        var ticket = await ticketService.CreateTicketAsync();
        await ticketService.PayTicketAsync(ticket.Id);

        var paidTicket = await dbContext.Tickets.FindAsync(ticket.Id);
        Assert.NotNull(paidTicket);
        paidTicket.PaymentTime = DateTime.UtcNow.AddMinutes(-5); // 5 minutes ago
        await dbContext.SaveChangesAsync();

        // Act
        var result = await ticketService.ValidateExitAsync(ticket.Id);

        // Assert
        Assert.True(result);
    }
}