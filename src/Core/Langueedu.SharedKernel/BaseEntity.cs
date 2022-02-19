using System.ComponentModel;

namespace Langueedu.SharedKernel;

// This can be modified to BaseEntity<TId> to support multiple key types (e.g. Guid)
public abstract class BaseEntity : BaseEntity<int>
{
  public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();

  public DateTime? ModifiedDate { get; set; }

  [ReadOnly(true)]
  public DateTime CreatedDate { get; set; }
}

public abstract class BaseEntity<T>
{
  public T Id { get; set; }
}

