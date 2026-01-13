using CarRentalApi.Application.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApi.Controllers;
/// <summary>
/// Car Controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CarController : Controller
{
    private readonly ICarApplicationService _carApplicationService;

    public CarController(ICarApplicationService carApplicationService)
    {
        _carApplicationService = carApplicationService;
    }

    [HttpGet]    
    public async Task<IActionResult> GetCars()
    {
        var cars = await _carApplicationService.GetCars();
        return Ok(cars);
    }

    [HttpGet]
    [Route("Healty")]
    public async Task<IActionResult> HelathCar()
    {
        try
        { 
           var cars = await _carApplicationService.GetCars();
            return  Ok("Healthy");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Unhealthy: {ex.Message}");
        }        
      
    }
}
