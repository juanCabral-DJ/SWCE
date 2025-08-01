using Microsoft.AspNetCore.Mvc;
using SWCE.Web.Models.Base;
using SWCE.Web.Models.Cupones.CuponPorcentaje;

namespace SWCE.Web.Services.Base
{
    public abstract class HttpServiceBase
    {
        protected readonly HttpClient _httpClient;

        protected HttpServiceBase(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Client");
        }

        protected async Task<ModelResponse<T>> GetAsync<T>(string endpoint) where T : class
        {
            try
            {
                var response = await _httpClient.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ModelResponse<T>>();
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return new ModelResponse<T>
                    {
                        isSuccess = false,
                        message = $"Error de la API: {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ModelResponse<T>
                {
                    isSuccess = false,
                    message = $"Error de conexión. {ex}"
                };
            }
        }

        protected async Task<ModelResponse<T>> PostAsync<T>(string endpoint, T data) where T : class
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(endpoint, data);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ModelResponse<T>>();
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return new ModelResponse<T>
                    {
                        isSuccess = false,
                        message = $"Error de la API: {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ModelResponse<T>
                {
                    isSuccess = false,
                    message = $"Error de conexión. {ex}"
                };
            }
        }

        protected async Task<ModelResponse<T>> PutAsync<T>(string endpoint, T data) where T : class
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(endpoint, data);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ModelResponse<T>>();
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return new ModelResponse<T>
                    {
                        isSuccess = false,
                        message = $"Error de la API: {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ModelResponse<T>
                {
                    isSuccess = false,
                    message = $"Error de conexión. {ex}"
                };
            }
        }
    }
}
