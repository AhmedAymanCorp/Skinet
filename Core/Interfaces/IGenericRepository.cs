using Core.Entities;
using Core.Specification;

namespace Core.Interfaces;

public interface IGenericRepository<T> where T:BaseEntity
{
    Task<T> GetByIdAsync(int Id);

    Task<IReadOnlyList<T>> ListALLAsync();

    Task<T> GetEntityWithSpec(ISpecification<T> Spec);
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> Spec);

}
