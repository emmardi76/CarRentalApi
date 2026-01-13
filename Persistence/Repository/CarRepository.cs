using CarRentalApi.Domain.Entities;
using CarRentalApi.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApi.Persistence.Repository;

public class CarRepository : BaseRepository, ICarRepository
{
    public CarRepository(CarRentalContext carRentalContext) : base(carRentalContext)
    {
    }

    public async Task<ICollection<Car>> GetCars()
    {
        _context.Database.CanConnectAsync();

        return await _context.Cars
            .Include(c => c.CarType)
            .Include(c => c.CarModel)
            .OrderBy(c => c.Id)
            .ToListAsync();
    }



    public async Task<ICollection<Car>> GetCarsById(List<int> carIds)
    {
        return await _context.Cars
            .Where(c => carIds.Contains(c.Id))
            .Include(c => c.CarType)
            .Include(c => c.CarModel)
            .ToListAsync();
    }

    public async Task<Car?> GetCarById(int id)
    {
        return await _context.Cars
            .Include(c => c.CarType)
            .Include(c => c.CarModel)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    //add a function that retrieves the daily price for a small car
    public async Task<decimal?> GetSmallCarDailyPrice()
    {
        var smallCarType = await _context.CarTypes
            .FirstOrDefaultAsync(ct => ct.Name.ToLower() == "small");
        return smallCarType?.PricePerDay;
    }
}