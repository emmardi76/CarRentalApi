namespace CarRentalApi.Application.Dtos;

public class CarListDto
{
    public int Id { get; set; }
    public string PlateNumber { get; set; }  
    public string CarModelName { get; set; }   
    public string CarTypeName { get; set; }
    public bool IsRented { get; set; }
}