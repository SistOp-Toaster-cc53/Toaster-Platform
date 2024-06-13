using Cafeteria.Toaster.API.Shared.Domain.Repositories;
using Cafeteria.Toaster.API.Shared.Infrastructure.Persistence.Relational.EFC.Configuration;

namespace Cafeteria.Toaster.API.Shared.Infrastructure.Persistence.Relational.EFC.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context) => _context = context;

    public async Task CompleteAsync() => await _context.SaveChangesAsync();
}