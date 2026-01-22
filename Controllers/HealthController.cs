using CarRentalApi.Application.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : Controller
{
    private readonly IHealthApplicationService _healthService;

    public HealthController(IHealthApplicationService healthService)
    {
        _healthService = healthService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var healthy = await _healthService.IsHealthy();
            if (healthy) return Ok("Healthy");
            return StatusCode(500, "Unhealthy");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Unhealthy: {ex.Message}");
        }
    }
}