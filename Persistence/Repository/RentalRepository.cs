using CarRentalApi.Domain.Entities;
using CarRentalApi.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApi.Persistence.Repository;

public class RentalRepository : BaseRepository, IRentalRepository
{
    public RentalRepository(CarRentalContext context) : base(context)
    {
    }

    public async Task<List<Rental>> GetAllRentals()
    {
        return await _context.Rentals
                             .Include(r => r.Customer)
                             .Include(r => r.Car)
                                 .ThenInclude(c => c!.CarModel)
                             .Include(r => r.Car)
                                 .ThenInclude(c => c!.CarType)
                             .ToListAsync();
    }

    public async Task<Rental?> GetRentalById(int rentalId)
    {
        return await _context.Rentals.FirstOrDefaultAsync(r => r.Id == rentalId);
    }

    public void AddRentals(ICollection<Rental> rentals)
    {
        _context.Rentals.AddRange(rentals);
    }

    public Task<List<Rental>> GetRentalsById(List<int> rentalIds)
    {
        return _context.Rentals
                       .Where(r => rentalIds.Contains(r.Id))
                       .Include(r => r.Customer)
                       .Include(r => r.Car)
                           .ThenInclude(c => c!.CarModel)
                       .Include(r => r.Car)
                           .ThenInclude(c => c!.CarType)
                       .ToListAsync();
    }   
}