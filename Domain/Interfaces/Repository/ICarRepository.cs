using CarRentalApi.Domain.Entities;

namespace CarRentalApi.Domain.Interfaces.Repository;

public interface ICarRepository
{
    Task<ICollection<Car>> GetCars();
    Task<Car?> GetCarById(int carId);
    Task<ICollection<Car>> GetCarsById(List<int> carIds);
    Task<decimal?> GetSmallCarDailyPrice();
}
