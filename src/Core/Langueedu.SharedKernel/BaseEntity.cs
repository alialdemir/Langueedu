using System.ComponentModel;

namespace Langueedu.SharedKernel;

// This can be modified to BaseEntity<TId> to support multiple key types (e.g. Guid)
public abstract class BaseEntity : BaseEntity<int>
{

}

public abstract class BaseEntity<T>
{
  public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();
  public T Id { get; set; }

  public DateTime? ModifiedDate { get; set; } = DateTime.Now;

  [ReadOnly(true)]
  public DateTime CreatedDate { get; set; } = DateTime.Now;
}

