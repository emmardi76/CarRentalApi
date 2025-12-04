using CarRentalApi.Application.Dtos;
using CarRentalApi.Application.Services.ServiceInterfaces;
using CarRentalApi.CrossCutting.Exceptions;
using CarRentalApi.Domain.Interfaces.Repository;
using CarRentalApi.Domain.Interfaces.Service;
using MapsterMapper;

namespace CarRentalApi.Application.Services;

public class CustomerApplicationService : ICustomerApplicationService
{

    private readonly ICustomerRepository _customerRepository;
    private readonly ICustomerDomainService _customerDomainService;  
    private readonly IMapper _mapper;

    public CustomerApplicationService(ICustomerRepository customerRepository, IMapper mapper, ICustomerDomainService customerDomainService)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _customerDomainService = customerDomainService;     
    }   
    
    public async Task<CustomerDto> GetCustomerById(int customerId)
    {
        var itemCustomer = await _customerRepository.GetCustomerById(customerId);

        if (itemCustomer == null)
        {
            throw new ValidationException($"The customer with id {customerId} does not exist.");
        }

        return _mapper.Map<CustomerDto>(itemCustomer);
    }

    public async Task<IEnumerable<CustomerDto>> GetAllCustomers()
    {
        var customers = await _customerRepository.GetAllCustomers();

        return _mapper.Map<IEnumerable<CustomerDto>>(customers);
    }
}
