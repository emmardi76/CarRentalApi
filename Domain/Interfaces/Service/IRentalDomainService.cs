using CarRentalApi.Domain.Entities;

namespace CarRentalApi.Domain.Interfaces.Service;
public interface IRentalDomainService
{
    Task<ICollection<Rental>> RentCars(ICollection<Rental> rentals);
    Task<List<Rental>> ReturnCars(List<int> rentalId);    
}
