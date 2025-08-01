using SWCE.Web.Models.Base;
using System.Net.Http.Json;
using System.Text.Json;

namespace SWCE.Web.Services.Base;

public abstract class ServiceBase
{
    protected readonly HttpClient _httpClient;
    protected readonly JsonSerializerOptions _jsonOptions;

    protected ServiceBase(IHttpClientFactory httpClientFactory, string clientName = "Client")
    {
        _httpClient = httpClientFactory.CreateClient(clientName);
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    protected async Task<ModelResponse<T>> GetAsync<T>(string endpoint) where T : class
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<ModelResponse<T>>(endpoint, _jsonOptions);
            return response ?? ModelResponse<T>.Failure("La respuesta de la API fue nula.");
        }
        catch (Exception ex)
        {

            return ModelResponse<T>.Failure($"Error de comunicación (GET): {ex.Message}");
        }
    }

    protected async Task<ModelResponse<TResponse>> PostAsync<TRequest, TResponse>(string endpoint, TRequest data)
        where TRequest : class
        where TResponse : class
    {
        try
        {
            var httpResponse = await _httpClient.PostAsJsonAsync(endpoint, data, _jsonOptions);

            if (!httpResponse.IsSuccessStatusCode)
            {
                return ModelResponse<TResponse>.Failure($"Error del API (POST): {httpResponse.ReasonPhrase}");
            }

            return await httpResponse.Content.ReadFromJsonAsync<ModelResponse<TResponse>>(_jsonOptions)
                   ?? ModelResponse<TResponse>.Failure("La respuesta de la API fue nula.");
        }
        catch (Exception ex)
        {
            return ModelResponse<TResponse>.Failure($"Error de comunicación (POST): {ex.Message}");
        }
    }

}