using CarRentalApi.Application.Services.ServiceInterfaces;
using CarRentalApi.CrossCutting.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApi.Controllers;
/// <summary>
/// Customer Controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CustomerController : Controller
{
    private readonly ICustomerApplicationService _customerApplicationService;

    public CustomerController(ICustomerApplicationService customerApplicationService)
    {
        _customerApplicationService = customerApplicationService;
    }

    [HttpGet("All")]
    public async Task<IActionResult> GetAllCustomers()
    {
        var customers = await _customerApplicationService.GetAllCustomers();
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerById(int id)
    {
        try
        {
            var customer = await _customerApplicationService.GetCustomerById(id);
            return Ok(customer);
        }
        catch (ValidationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }   
}
