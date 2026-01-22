using CarRentalApi.Domain.Interfaces.Repository;

namespace CarRentalApi.Persistence.Repository;

public class HealthRepository : BaseRepository, IHealthRepository
{
    public HealthRepository(CarRentalContext context) : base(context)
    {
    }

    public async Task<bool> CanConnectAsync()
    {
        return await _context.Database.CanConnectAsync();
    }
}