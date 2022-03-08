using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Langueedu.Sdk.Interfaces;

namespace Langueedu.Sdk
{
  public abstract class ServiceBase
  {
    protected readonly HttpClient _httpClient;
    private readonly IToastService _toastService;
    private readonly JsonSerializerOptions _serializerSettings;
    public ServiceBase(HttpClient httpClient, IToastService toastService)
    {
      _httpClient = httpClient;
      _toastService = toastService;
      _serializerSettings = new JsonSerializerOptions
      {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonStringEnumConverter() }
      };
    }


    /// <summary>
    /// Request to get
    /// </summary>
    /// <typeparam name="TResult">Returns the value at the requested type</typeparam>
    /// <param name="url">Endpoint</param>
    /// <returns>Data of type TResult </returns>
    protected Task<Result<TResult>> GetAsync<TResult>(string url)
    {
      return SendAsync<Result<TResult>>(HttpMethod.Get, url);
    }

    /// <summary>
    /// Request to post
    /// </summary>
    /// <typeparam name="TResult">Returns the value at the requested type</typeparam>
    /// <param name="url">Endpoint</param>
    /// <param name="data">Content data</param>
    protected Task<Result<TResult>> PostAsync<TResult>(string url, object data = null)
    {
      return SendAsync<Result<TResult>>(HttpMethod.Post, url, data);
    }

    /// <summary>
    /// Request to delete
    /// </summary>
    /// <typeparam name="TResult">Returns the value at the requested type</typeparam>
    /// <param name="url">Endpoint</param>
    /// <returns>Data of type TResult </returns>
    protected Task<Result<TResult>> DeleteAsync<TResult>(string url)
    {
      return SendAsync<Result<TResult>>(HttpMethod.Delete, url);
    }

    /// <summary>
    /// Set http request header
    /// </summary>
    /// <param name="name">Header key</param>
    /// <param name="value">Header value</param>
    /// <returns>Service base class</returns>
    protected ServiceBase SetRequestHeader(string name, string value)
    {
      if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(value))
      {
        _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(name, value);
      }

      return this;
    }

    protected async Task<TResult> SendAsync<TResult>(HttpMethod httpMethod, string url, object data = null)
    {
      HttpResponseMessage response = null;

      try
      {
        switch (httpMethod)
        {
          case HttpMethod.Get:
            response = await _httpClient.GetAsync(url);
            break;

          case HttpMethod.Post:
            response = await _httpClient.PostAsJsonAsync(url, data);
            break;

          case HttpMethod.Delete:
            response = await _httpClient.DeleteAsync(url);
            break;
        }

        return await RequestAsResultAsync<TResult>(response);
      }
      catch (Exception ex)
      {
        Console.WriteLine("Service base error: \n {0}", ex.Message);

        _toastService.ShowToast(ex.Message, ToastLevel.Error);

        return Result<TResult>.Error(ex.Message);
      }
    }

    protected async Task<TResult> RequestAsResultAsync<TResult>(HttpResponseMessage response)
    {
      if (!response.IsSuccessStatusCode)
      {
        var errors = await response.Content.ReadFromJsonAsync<Result<string>>(_serializerSettings);
        foreach (var error in errors.Errors)
        {
          _toastService.ShowToast(error, ToastLevel.Error);
        }

        return default(TResult);
      }

      var result = await response.Content.ReadFromJsonAsync<TResult>(_serializerSettings);
      return result;
    }
  }
}