namespace EstacionaT.Domain.Entities;

public class Ticket
{
    public int Id { get; set; }
    public DateTime EntryTime { get; set; }
    public DateTime? ExitTime { get; set; }
    public bool IsPaid { get; set; }
    public bool Validated { get; set; }
    public DateTime? PaymentTime { get; set; }
}