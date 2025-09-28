using EstacionaT.Domain.Entities;

namespace EstacionaT.Domain.Services;

public interface ITicketService
{
    Task<Ticket> CreateTicketAsync();
    Task<bool> ValidateExitAsync(int ticketId);
    Task<bool> PayTicketAsync(int ticketId);
}