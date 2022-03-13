namespace Langueedu.Core.Entities.User;

public interface IUser
{
  string Id { get; set; }
  string UserName { get; set; }
  string Email { get; set; }
  bool IsActive { get; set; }
}

