namespace EstacionaT.Domain.Entities;

public class TrafficStatus
{
    public int Id { get; set; }
    public bool IsCongestionActive { get; set; }
    public int ExtraToleranceMinutes { get; set; }
    public bool UnlimitedMode { get; set; }
}