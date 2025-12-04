namespace CarRentalApi.Domain.Entities;

public class CarModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<Car>? Cars { get; set; }
}
