using CarRentalApi.Application.Services.ServiceInterfaces;
using CarRentalApi.Domain.Interfaces.Repository;

namespace CarRentalApi.Application.Services;

public class HealthApplicationService : IHealthApplicationService
{
    private readonly IHealthRepository _healthRepository;

    public HealthApplicationService(IHealthRepository healthRepository)
    {
        _healthRepository = healthRepository;
    }

    public async Task<bool> IsHealthy()
    {
        return await _healthRepository.CanConnectAsync();
    }
}