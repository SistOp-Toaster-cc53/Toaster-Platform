namespace Cafeteria.Toaster.API.Shared.Domain.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}