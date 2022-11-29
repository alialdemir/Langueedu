namespace Langueedu.Components.Interfaces;

public interface ICookieService
{
  Task<T> GetItemAsync<T>(string key);
}