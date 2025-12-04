namespace CarRentalApi.Domain.Entities;

public class CarType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal PricePerDay { get; set; }

    public int LoyaltyPoints { get; set; }

    public ICollection<Car>? Cars { get; set; }
}
