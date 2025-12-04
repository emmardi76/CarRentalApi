using CarRentalApi.Domain.Entities;

namespace CarRentalApi.Domain.Interfaces.Repository;

public interface ICustomerRepository : IUoW
{
    Task<Customer?> GetCustomerById(int id);
    Task<List<Customer>> GetCustomersById(List<int> ids);
    Task<IEnumerable<Customer>> GetAllCustomers();
}
