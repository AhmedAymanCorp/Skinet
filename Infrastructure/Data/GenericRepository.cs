using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly StoreContext _context;

    public GenericRepository(StoreContext context)
    {
        _context = context;
    }

    public async Task<T> GetByIdAsync(int Id)
    {
        return await _context.Set<T>().FindAsync(Id);
    }


    public async Task<IReadOnlyList<T>> ListALLAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> GetEntityWithSpec(ISpecification<T> Spec)
    {
        return await ApplySpecification(Spec).FirstOrDefaultAsync();
    }
    public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> Spec)
    {
        return await ApplySpecification(Spec).ToListAsync();
    }
    private IQueryable<T> ApplySpecification(ISpecification<T> Spec)
    {
        return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), Spec);
    }

}
