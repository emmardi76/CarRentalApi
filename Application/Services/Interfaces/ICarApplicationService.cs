using CarRentalApi.Application.Dtos;

namespace CarRentalApi.Application.Services.ServiceInterfaces;

public interface ICarApplicationService
{
    Task<ICollection<CarListDto>> GetCars();
}