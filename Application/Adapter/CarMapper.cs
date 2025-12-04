using CarRentalApi.Domain.Entities;
using CarRentalApi.Application.Dtos;
using Mapster;
namespace CarRentalApi.Application.Adapter;

public class CarMapper
{
    // Adds a static method to configure mapping
    public static void Configure()
    {
        TypeAdapterConfig<Car, CarListDto>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.PlateNumber, src => src.PlateNumber)
            .Map(dest => dest.CarModelName, src => src.CarModel != null ? src.CarModel.Name : string.Empty)
            .Map(dest => dest.CarTypeName, src => src.CarType != null ? src.CarType.Name : string.Empty)
            .Map(dest => dest.IsRented, src => src.IsRented);
    }
}
