namespace CarRentalApi.Application.Services.ServiceInterfaces;

public interface IHealthApplicationService
{
    Task<bool> IsHealthy();
}