using CarRentalApi.Application.Dtos;

namespace CarRentalApi.Application.Services.ServiceInterfaces;

public interface ICustomerApplicationService
{    
    Task<CustomerDto> GetCustomerById(int customerId);
    Task<IEnumerable<CustomerDto>> GetAllCustomers();   
}
