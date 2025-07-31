using SWCE.Web.Models.Categoria;
using SWCE.Web.Services.Interfaces;

namespace SWCE.Web.Services
{
    public class CategoriaHttpService : ICategoriaHttpService
    {
        private readonly HttpClient _httpClient;

        public CategoriaHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Client");
        }

        public async Task<GetAllCategoriaResponse> GetAllCategoriasAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("Categoria/Getall");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                return System.Text.Json.JsonSerializer.Deserialize<GetAllCategoriaResponse>(content)!;
            }
            catch (Exception ex)
            {
                return new GetAllCategoriaResponse
                {
                    isSuccess = false,
                    message = $"Error al obtener las Categorias {ex.Message}"
                };
            }
        }

        public async Task<GetCategoriaByIdResponse> GetCategoriaByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Categoria/GetCategoriaById?id={id}");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                return System.Text.Json.JsonSerializer.Deserialize<GetCategoriaByIdResponse>(content)!;
            }
            catch(Exception ex)
            {
                return new GetCategoriaByIdResponse
                {
                    isSuccess = false,
                    message = $"Error al obtener la Categoria con ID {id}: {ex.Message}"
                };
            }
        }

        public async Task<CreateCategoriaResponse> CreateCategoriaAsync(CategoriaModel categoria)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Categoria/CreateCategoria", categoria);
                var content = await response.Content.ReadAsStringAsync();
                var apiResponse = System.Text.Json.JsonSerializer.Deserialize<CreateCategoriaResponse>(content);

                if (!response.IsSuccessStatusCode || !apiResponse!.isSuccess)
                {
                    apiResponse!.message = apiResponse.message ?? $"La API retorno un error de estado {response.StatusCode}.";
                }
                return apiResponse!;
            }
            catch (Exception ex)
            {
                return new CreateCategoriaResponse
                {
                    isSuccess = false,
                    message = $"Error al crear la Categoria: {ex.Message}"
                };
            }
        }

        public async Task<UpdateCategoriaResponse> UpdateCategoriaAsync(UpdateCategoriaModel categoria)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("Categoria/UpdateCategoria", categoria);
                var content = await response.Content.ReadAsStringAsync();
                var apiResponse = System.Text.Json.JsonSerializer.Deserialize<UpdateCategoriaResponse>(content);

                if(!response.IsSuccessStatusCode || !apiResponse!.isSuccess)
                {
                    apiResponse!.message = apiResponse.message ?? $"La API retorno un error: {response.StatusCode}";
                }
                return apiResponse;
            }
            catch (Exception ex)
            {
                return new UpdateCategoriaResponse
                {
                    isSuccess = false,
                    message = $"Error al actualizar la Categoria: {ex.Message}"
                };
            }
        }

        public async Task<DisableCategoriaResponse> DisableCategoriaAsync(int id)
        {
            try
            {
                var response = await _httpClient.PostAsync($"Categoria/DisableCategoria?id={id}", null);
                var content = await response.Content.ReadAsStringAsync();
                var apiResponse = System.Text.Json.JsonSerializer.Deserialize<DisableCategoriaResponse>(content);

                if (!response.IsSuccessStatusCode || !apiResponse!.isSuccess)
                {
                    apiResponse!.message = apiResponse.message ?? $"La API devolvió un error de estado: {response.StatusCode}.";
                }
                return apiResponse;
            }
            catch(Exception ex)
            {
                return new DisableCategoriaResponse
                {
                    isSuccess = false,
                    message = $"Error al deshabilitar la Categoria con ID {id}: {ex.Message}"
                };
            }
        }
    }
}
