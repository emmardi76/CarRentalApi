using CarRentalApi.CrossCutting.Exceptions;
using CarRentalApi.Domain.Entities;
using CarRentalApi.Domain.Interfaces.Repository;
using CarRentalApi.Domain.Interfaces.Service;

namespace CarRentalApi.Domain.Services;

public class RentalDomainService: IRentalDomainService
{
    private ICarRepository _carRepository;
    private ICustomerRepository _customerRepository;
    private IRentalRepository _rentalRepository;   

    public RentalDomainService(ICarRepository carRepository, ICustomerRepository customerRepository, IRentalRepository rentalRepository)
    {
        _carRepository = carRepository;
        _customerRepository = customerRepository;
        _rentalRepository = rentalRepository;        
    }

    #region Rent Cars
    public async Task<ICollection<Rental>> RentCars(ICollection<Rental> rentals)
    {        
        foreach (var rental in rentals)
        {
            await ValidateAndCompleteRental(rental);                
        }        
        _rentalRepository.AddRentals(rentals);

        return rentals;
    }    

    private async Task ValidateAndCompleteRental(Rental rental) 
    {
        if (rental.ExpectedDaysBooked < 1)
        {
            throw new ValidationException("Days rented must be a positive integer.");
        }

        var car = await _carRepository.GetCarById(rental.CarId);
        if (car == null)
        {
            throw new ValidationException($"Car with Id {rental.CarId} not found.");
        }
        if (car.IsRented)
        {
            throw new ValidationException($"Car {car.Id} (plate {car.PlateNumber}) is already rented.");
        }
        car.IsRented = true; // Entity framework tracking will handle the update
        rental.Car = car;

        var customer = await _customerRepository.GetCustomerById(rental.CustomerId);
        if (customer == null)
        {
            throw new ValidationException($"Customer with Id {rental.CustomerId} not found.");
        }
        
        // Ensure right start date
        rental.StartDate = DateTime.UtcNow;
        CalculateCost(rental);
    }        

    private decimal GetSuvRentalCost(int daysRented, decimal currentSuvPrice)                    
    {            
        decimal cost = 0;
        switch (daysRented)
        {
            case <= 7:
                cost = daysRented * currentSuvPrice;
                break;
            case <= 30:
                cost = 7 * currentSuvPrice + (daysRented - 7) * currentSuvPrice * 0.8m;
                break;
            default:
                cost = 7 * currentSuvPrice + 23 * currentSuvPrice * 0.8m + (daysRented - 30) * currentSuvPrice * 0.5m;
                break;
        }

        return cost;       
    }

    private decimal GetSmallRentalCost(int daysRented, decimal currentSmallPrice)
    {
        decimal cost = 0;

        if (daysRented <= 7)
            cost = daysRented * currentSmallPrice;
        else
            cost = (7 * currentSmallPrice) + ((daysRented - 7) * currentSmallPrice * 0.6m);

        return cost;
    }

    private void CalculateCost(Rental rental)
    {
        switch (rental.Car?.CarType?.Name.ToLower())
        {
            case "small":
                rental.Cost = GetSmallRentalCost(rental.ExpectedDaysBooked, rental.Car.CarType.PricePerDay);
                break;
            case "suv":
                rental.Cost = GetSuvRentalCost(rental.ExpectedDaysBooked, rental.Car.CarType.PricePerDay);
                break;
            case "premium":
                rental.Cost = rental.ExpectedDaysBooked * rental.Car.CarType.PricePerDay;
                break;
            default:
                throw new ValidationException("Unknown car type.");
        }
    }

    #endregion

    #region Return Cars
    public async Task<List<Rental>> ReturnCars(List<int> rentalIds)
    {
        var rentals = await _rentalRepository.GetRentalsById(rentalIds);

        foreach (var rental in rentals)
        {
            await ReturnRental(rental);
        }
        
        return rentals;
    }

    private async Task<Rental> ReturnRental(Rental rental) 
    {
       if (rental.ReturnedDate.HasValue)
            throw new ValidationException($"Rental {rental.Id} already returned");

        decimal totalSurcharge = 0m;
        int extraDays = (DateTime.Now - rental.StartDate).Days - rental.ExpectedDaysBooked;
        
        if (extraDays > 0)
        {
            switch (rental.Car!.CarType!.Name.ToLower())
            {
                case "small":
                    totalSurcharge = await GetSmallSurcharge(extraDays);
                    break;
                case "premium":
                    totalSurcharge = GetPremiumSurcharge(extraDays, rental.Car!.CarType.PricePerDay);
                    break;
                case "suv":
                    totalSurcharge = await GetSuvSurcharge(extraDays, rental.Car.CarType.PricePerDay);
                    break;
                default:
                    throw new ArgumentException("Unknown car type.");
            }
        }
        // Entity framework tracking will handle the updates.
        // Update rental.
        rental.Surcharges = totalSurcharge;
        rental.ExtraDaysBooked = Math.Max(0, extraDays);
        // Update car status to not rented.
        rental.Car!.IsRented = false; 
        rental.ReturnedDate = DateTime.UtcNow; // Set actual return date.

        return rental;
    }

    private decimal GetPremiumSurcharge(int extraDays, decimal currentPremiumPrice)
    {
        return extraDays * currentPremiumPrice * 1.2m;
    }

    private async Task<decimal> GetSuvSurcharge(int extraDays, decimal currentSuvPrice)
    {
        decimal smallCarPrice = await _carRepository.GetSmallCarDailyPrice() ?? 0m;
        return extraDays * (currentSuvPrice + smallCarPrice * 0.6m);
    }

    private async Task<decimal> GetSmallSurcharge(int extraDays)
    {
        decimal smallCarPrice = await _carRepository.GetSmallCarDailyPrice() ?? 0m;
        return extraDays * smallCarPrice * 0.8m;
    }
    #endregion

}
