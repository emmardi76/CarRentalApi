using CarRentalApi.Application.Dtos;
using CarRentalApi.Application.Services.Interfaces;
using CarRentalApi.CrossCutting.Exceptions;
using CarRentalApi.Domain.Entities;
using CarRentalApi.Domain.Interfaces.Repository;
using CarRentalApi.Domain.Interfaces.Service;
using MapsterMapper;

namespace CarRentalApi.Application.Services;

public class RentalApplicationService : IRentalApplicationService
{
    private readonly IRentalDomainService _rentalDomainService;
    private readonly IRentalRepository _rentalRepository;
    private readonly ICustomerDomainService _customerDomainService;
    private readonly IMapper _mapper;

    public RentalApplicationService(IRentalDomainService rentalDomainService, ICustomerDomainService customerDomainService, IRentalRepository rentalRepository, IMapper mapper)
    {
        _rentalDomainService = rentalDomainService;
        _customerDomainService = customerDomainService;
        _rentalRepository = rentalRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Rent cars
    /// </summary>
    /// <param name="rentRequestDto"></param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    public async Task<RentResponseDto> RentCars(RentRequestDto rentRequestDto)
    {
        // Dto validations
        if (rentRequestDto.Items == null || !rentRequestDto.Items.Any())
            throw new ValidationException("Rent request must contain at least one rental item.");

        if (rentRequestDto.CustomerId <= 0)
            throw new ValidationException("Invalid customer ID.");


        // Dto adaptation to domain entities
        var rentals = rentRequestDto.Items.Select(item => new Rental
        {
            CarId = item.CarId,
            CustomerId = rentRequestDto.CustomerId,
            ExpectedDaysBooked = item.DaysBooked,
            StartDate = DateTime.UtcNow
        }).ToList();

        // Apply business logic, domain service orchestration
        var fullRentals = await _rentalDomainService.RentCars(rentals);
        _customerDomainService.AddLoyaltyPoints(fullRentals);

        // All changes persistence. Transactional behavior of changes ensured in application layer.
        await _rentalRepository.Commit();

        // Adaptation domain entities to response dto
        var responseDto = new RentResponseDto
        {

            CustomerId = rentRequestDto.CustomerId,
            TotalCost = fullRentals.Sum(r => r.Cost),
            Items = fullRentals.Select(r => new RentalResultItemDto
            {
                Id = r.Id,
                CarId = r.CarId,
                PlateNumber = r.Car?.PlateNumber ?? string.Empty,
                CarType = r.Car?.CarType?.Name ?? string.Empty,
                DaysBooked = r.ExpectedDaysBooked,
                Cost = r.Cost
            }).ToList()
        };

        return responseDto;
    }


    /// <summary>
    /// ReturnCar
    /// </summary>
    /// <param name="returnRequestDto"></param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    public async Task<List<ReturnResponseDto>> ReturnCars(ReturnRequestDto returnRequestDto)
    {
        //Validate params
        if (returnRequestDto.RentalIds == null || !returnRequestDto.RentalIds.Any())
            throw new ValidationException("Return request must contain at least one rental to return.");

        // Apply Business logic
        var rentals = await _rentalDomainService.ReturnCars(returnRequestDto.RentalIds);

        // Persist
        await _rentalRepository.Commit();

        // Build response DTO
        return _mapper.Map<List<ReturnResponseDto>>(rentals);
    }

    /// <summary>
    /// Get all rentals grouped by customer
    /// </summary>
    /// <returns></returns>
    public async Task<List<RentResponseDto>> GetAllRentals()
    {
        var rentals = await _rentalRepository.GetAllRentals();

        var response = rentals
            .GroupBy(r => r.CustomerId)
            .Select(group => new RentResponseDto
            {
                CustomerId = group.Key,
                TotalCost = group.Sum(r => r.TotalCost),
                Items = group.Select(r => new RentalResultItemDto
                {
                    Id = r.Id,
                    CarId = r.CarId,
                    PlateNumber = r.Car!.PlateNumber,
                    CarType = r.Car!.CarType!.Name,
                    DaysBooked = r.ExpectedDaysBooked + r.ExtraDaysBooked,
                    Cost = r.TotalCost
                }).ToList()
            })
            .ToList();

        return response;

    }
}
