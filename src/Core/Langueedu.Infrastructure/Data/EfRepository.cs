using Ardalis.Specification.EntityFrameworkCore;
using Langueedu.SharedKernel.Interfaces;

namespace Langueedu.Infrastructure.Data;

// inherit from Ardalis.Specification type
public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
  private readonly AppDbContext _dbContext;

  public EfRepository(AppDbContext dbContext) : base(dbContext)
  {
    _dbContext = dbContext;
  }
}

