using CarRentalApi.Domain.Entities;
using CarRentalApi.Application.Dtos;
using Mapster;

namespace CarRentalApi.Application.Adapter;
public class RentalMapper: IRegister
{
    // Adds a static method to configure mapping
    void IRegister.Register(TypeAdapterConfig config)
    {
        config.NewConfig<Rental, ReturnResponseDto>()
            .Map(dest => dest.RentalId, src => src.Id)
            .Map(dest => dest.CarId, src => src.Car!.Id)
            .Map(dest => dest.PlateNumber, src => src.Car!.PlateNumber)
            .Map(dest => dest.CarModel, src => src.Car!.CarModel!.Name)
            .Map(dest => dest.CarType, src => src.Car!.CarType!.Name)
            .Map(dest => dest.ExpectedDaysBooked, src => src.ExpectedDaysBooked)
            .Map(dest => dest.ExtraDaysBooked, src => src.ExtraDaysBooked)
            .Map(dest => dest.StartDate, src => src.StartDate)           
            .Map(dest => dest.ReturnedDate, src => src.ReturnedDate) // Actual return date
            .Map(dest => dest.Cost, src => src.Cost)
            .Map(dest => dest.Surcharges, src => src.Surcharges)
            .Map(dest => dest.TotalCost, src => src.TotalCost);

        //Rental mapping -> RentalResultItemDto
        config.NewConfig<Rental, RentalResultItemDto>()
           .Map(dest => dest.Id, src => src.Id)
           .Map(dest => dest.CarId, src => src.Car!.Id)
           .Map(dest => dest.PlateNumber, src => src.Car!.PlateNumber)
           .Map(dest => dest.CarType, src => src.Car!.CarType!.Name)
           .Map(dest => dest.DaysBooked, src => src.ExpectedDaysBooked + src.ExtraDaysBooked)
           .Map(dest => dest.Cost, src => src.TotalCost);

        // Mapping from IEnumerable<Rental> to RentResponseDto (grouped by CustomerId)
        config.NewConfig<IEnumerable<Rental>, List<RentResponseDto>>()
           .Map(dest => dest, src => src
               .GroupBy(r => r.CustomerId)
               .Select(group => new RentResponseDto
               {
                   CustomerId = group.Key,
                   TotalCost = group.Sum(r => r.TotalCost),
                   Items = group.Adapt<List<RentalResultItemDto>>() // use the previous mapping
               })
               .ToList()
           );
    }
}
