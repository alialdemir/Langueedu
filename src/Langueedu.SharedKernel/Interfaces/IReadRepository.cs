using Ardalis.Specification;

namespace Langueedu.SharedKernel.Interfaces;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}

