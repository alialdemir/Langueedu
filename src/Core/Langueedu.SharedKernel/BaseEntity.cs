using System.ComponentModel;

namespace Langueedu.SharedKernel;

public abstract class BaseEntityNoId
{
  public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();

  public DateTime? ModifiedDate { get; set; } = DateTime.Now;

  [ReadOnly(true)]
  public DateTime CreatedDate { get; set; } = DateTime.Now;
}

public abstract class BaseEntity : BaseEntity<int>
{

}

// This can be modified to BaseEntity<TId> to support multiple key types (e.g. Guid)
public abstract class BaseEntity<TId> : BaseEntityNoId
{
  public TId Id { get; set; }
}

