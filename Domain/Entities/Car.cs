namespace CarRentalApi.Domain.Entities;

public class Car
{
    public int Id { get; set; }
    public string PlateNumber { get; set; }       
    public bool IsRented { get; set; }

    // Relationships
    public int CarTypeId { get; set; } // Foreign key property
    public virtual CarType? CarType { get; set; } // Navigation Property. Virtual in case we want to use lazy loading

    public int CarModelId { get; set; } // Foreign key property
    public virtual CarModel? CarModel { get; set; } // Navigation Property. Virtual in case we want to use lazy loading

    public ICollection<Rental>? Rentals { get; set; } = new List<Rental>();
}
