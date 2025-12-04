namespace CarRentalApi.Application.Dtos;

public class ReturnResponseDto
{
    public int RentalId { get; set; }
    public int CarId { get; set; }
    public string PlateNumber { get; set; } = string.Empty;
    public string CarModel { get; set; } = string.Empty;
    public string CarType { get; set; } = string.Empty;
    public int ExpectedDaysBooked { get; set; }
    public int ExtraDaysBooked { get; set; }
    public DateTime StartDate { get; set; }  
    public DateTime? ReturnedDate { get; set; } // Actual return date
    public decimal Cost { get; set; }
    public decimal Surcharges { get; set; }
    public decimal TotalCost { get; set; }
}
