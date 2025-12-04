namespace CarRentalApi.Domain.Interfaces;

public interface IUoW
{
    Task Commit();
}
