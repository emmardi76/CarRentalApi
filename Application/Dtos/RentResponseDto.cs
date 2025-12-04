namespace CarRentalApi.Application.Dtos;

public class RentResponseDto
{
    public int CustomerId { get; set; }
    public decimal TotalCost { get; set; }
    public List<RentalResultItemDto> Items { get; set; } = new();
}

public class RentalResultItemDto
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public string PlateNumber { get; set; } = string.Empty;
    public string CarType { get; set; } = string.Empty;
    public int DaysBooked { get; set; }
    public decimal Cost { get; set; }
}