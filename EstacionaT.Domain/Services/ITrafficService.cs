namespace EstacionaT.Domain.Services;

public interface ITrafficService
{
    Task ActivateCongestionModeAsync(int extraToleranceMinutes, bool unlimitedMode);
    Task DeactivateCongestionModeAsync();
}