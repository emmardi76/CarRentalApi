using CarRentalApi.Domain.Entities;
using CarRentalApi.Domain.Interfaces.Service;

namespace CarRentalApi.Domain.Services;
public class CustomerDomainService : ICustomerDomainService
{
    public void AddLoyaltyPoints(ICollection<Rental> rentals)
    {
        foreach (var rental in rentals)
        {
            if (rental.Car?.CarType?.LoyaltyPoints > 0 && rental.Customer is not null)
            {
                rental.Customer.LoyaltyPoints += rental.Car?.CarType.LoyaltyPoints ?? 0;
            }
        }
    }
}
