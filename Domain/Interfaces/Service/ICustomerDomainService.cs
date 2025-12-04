using CarRentalApi.Domain.Entities;

namespace CarRentalApi.Domain.Interfaces.Service;

public interface ICustomerDomainService
{
    void AddLoyaltyPoints(ICollection<Rental> rentals);
}
