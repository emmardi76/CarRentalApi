using CarRentalApi.Domain.Interfaces;

namespace CarRentalApi.Persistence.Repository;

public class BaseRepository: IUoW
{
    protected readonly CarRentalContext _context;

    public BaseRepository(CarRentalContext context) 
    { 
        _context = context; 
    }
    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }
}