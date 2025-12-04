using CarRentalApi.Application.Dtos;

namespace CarRentalApi.Application.Services.Interfaces;

public interface IRentalApplicationService
{
    Task<RentResponseDto> RentCars(RentRequestDto rentRequestDto);
    Task<List<ReturnResponseDto>> ReturnCars(ReturnRequestDto returnRequestDto);
    Task<List<RentResponseDto>> GetAllRentals();
}