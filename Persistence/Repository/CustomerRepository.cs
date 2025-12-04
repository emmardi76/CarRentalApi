using CarRentalApi.Domain.Entities;
using CarRentalApi.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApi.Persistence.Repository;

public class CustomerRepository : BaseRepository, ICustomerRepository
{
    public CustomerRepository(CarRentalContext carRentalContext) : base(carRentalContext)
    {
    }    

    public async Task<IEnumerable<Customer>> GetAllCustomers()
    {
        return await _context.Customers.OrderBy(c => c.Id).ToListAsync();
    }

    public async Task<Customer?> GetCustomerById(int id)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);            
    }

    public async Task<List<Customer>> GetCustomersById(List<int> ids)
    {
        return await _context.Customers
                             .Where(c => ids.Contains(c.Id))
                             .ToListAsync();            
    }
}