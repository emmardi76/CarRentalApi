namespace CarRentalApi.Domain.Entities;

public class Rental
{
    public int Id { get; set; }    
    public int CarId { get; set; }
    public virtual Car? Car { get; set; }

    public int CustomerId { get; set; }    
    public virtual Customer? Customer { get; set; }

    public int ExpectedDaysBooked { get; set; }
    public int ExtraDaysBooked { get; set; }
    public DateTime StartDate { get; set; }
    //Actual return date
    public DateTime? ReturnedDate { get; set; } 
    public decimal Cost { get; set; }
    public decimal Surcharges { get; set; }
    // Calculated property (not stored in DB)
    public decimal TotalCost => Cost + Surcharges;
}