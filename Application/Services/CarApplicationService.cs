namespace CarRentalApi.Application.Services;

using CarRentalApi.Application.Dtos;
using CarRentalApi.Application.Services.ServiceInterfaces;
using CarRentalApi.Domain.Interfaces.Repository;
using MapsterMapper;

public class CarApplicationService : ICarApplicationService
{
    private readonly ICarRepository _carRepository;
    private readonly IMapper _mapper;

    public CarApplicationService(ICarRepository carRepository, IMapper mapper)
    {
        _carRepository = carRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<CarListDto>> GetCars()
    {
        var ListCars = await _carRepository.GetCars();
        return _mapper.Map<ICollection<CarListDto>>(ListCars);
    }
}
