namespace CarRentalApi.Domain.Interfaces.Repository;

public interface IHealthRepository
{
    Task<bool> CanConnectAsync();
}