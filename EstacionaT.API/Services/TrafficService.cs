using EstacionaT.Domain.Services;
using EstacionaT.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EstacionaT.API.Services;

public class TrafficService : ITrafficService
{
    private readonly ParkingDbContext _context;

    public TrafficService(ParkingDbContext context)
    {
        _context = context;
    }

    public async Task ActivateCongestionModeAsync(int extraToleranceMinutes, bool unlimitedMode)
    {
        var trafficStatus = await _context.TrafficStatus.FirstAsync();
        trafficStatus.IsCongestionActive = true;
        trafficStatus.ExtraToleranceMinutes = extraToleranceMinutes;
        trafficStatus.UnlimitedMode = unlimitedMode;
        await _context.SaveChangesAsync();
    }

    public async Task DeactivateCongestionModeAsync()
    {
        var trafficStatus = await _context.TrafficStatus.FirstAsync();
        trafficStatus.IsCongestionActive = false;
        trafficStatus.ExtraToleranceMinutes = 0;
        trafficStatus.UnlimitedMode = false;
        await _context.SaveChangesAsync();
    }
}