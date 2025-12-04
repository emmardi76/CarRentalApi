using CarRentalApi.Application.Dtos;
using CarRentalApi.Application.Services.Interfaces;
using CarRentalApi.CrossCutting.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RentalController : Controller
{  
    private readonly IRentalApplicationService _rentalApplicationService;

    public RentalController(IRentalApplicationService rentalApplicationService)
    {        
       _rentalApplicationService = rentalApplicationService;
    }

    //Endpoint for rent a car
    [HttpPost]    
    [Route("Rent")]
    public async Task<IActionResult> Rent([FromBody] RentRequestDto request)
    {
        try 
        {
            var result = await _rentalApplicationService.RentCars(request);
            return Ok(result);
        }
        catch (ValidationException ex)
        {
            return BadRequest($"Validation error: {ex.Message}");
        }
        catch (Exception ex) 
        { 
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    //Endpoint for return a car and calculate surcharges(if exist)
    [HttpPost]
    [Route("Return")]
    public async Task<IActionResult> Return([FromBody] ReturnRequestDto request)
    {
        try
        {
            var result = await _rentalApplicationService.ReturnCars(request);
            return Ok(result);
        }
        catch (ValidationException ex)
        {
            return BadRequest($"Validation error: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }       
    }

    //Endpoint to get all rentals by customer
    [HttpGet]
    [Route("AllByCustomer")]
    public async Task<IActionResult> GetAllRentals()
    {
        var rentals = await _rentalApplicationService.GetAllRentals();
        return Ok(rentals);
    }
}
