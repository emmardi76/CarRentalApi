namespace CarRentalApi.Application.Dtos;

public class RentRequestDto
{
    public int CustomerId { get; set; }
    public List<RentItemDto> Items { get; set; } = new();
}

public class RentItemDto
{
    public int CarId { get; set; }
    public DateTime StartDate { get; set; }
    public int DaysBooked { get; set; }
}