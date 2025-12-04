using CarRentalApi.Domain.Entities;

namespace CarRentalApi.Domain.Interfaces.Repository;
public interface IRentalRepository: IUoW
{
    Task<List<Rental>> GetAllRentals();
    Task<Rental?> GetRentalById(int rentalId);
    Task<List<Rental>> GetRentalsById(List<int> rentalIds);
    void AddRentals(ICollection<Rental> rentals);
}