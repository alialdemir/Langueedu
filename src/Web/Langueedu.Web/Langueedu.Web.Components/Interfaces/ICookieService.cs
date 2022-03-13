namespace Langueedu.Web.Components.Interfaces;

public interface ICookieService
{
  Task<T> GetItemAsync<T>(string key);
}