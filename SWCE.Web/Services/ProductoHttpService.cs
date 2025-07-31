using SWCE.Web.Models.Producto;
using SWCE.Web.Services.Interfaces;

namespace SWCE.Web.Services
{
    public class ProductoHttpService : IProductoHttpService
    {
        private readonly HttpClient _httpClient;

        public ProductoHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Client");
        }
        public async Task<GetAllProductoResponse> GetAllProductosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("Producto/GetAll");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                return System.Text.Json.JsonSerializer.Deserialize<GetAllProductoResponse>(content)!;
            }
            catch (Exception ex)
            {
                return new GetAllProductoResponse
                {
                    isSuccess = false,
                    message = $"Error al obtener los Productos: {ex.Message}"
                };
            }
        }

        public async Task<GetProductoByIdResponse> GetProductoByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Producto/GetProductoById?id={id}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return System.Text.Json.JsonSerializer.Deserialize<GetProductoByIdResponse>(content)!;
            }
            catch(Exception ex)
            {
                return new GetProductoByIdResponse
                {
                    isSuccess = false,
                    message = $"Error al obtener el Producto con ID {id}: {ex.Message}"
                };
            }
        }

        public async Task<CreateProductoResponse> CreateProductoAsync(CreateProductoModel producto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Producto/CreateProducto", producto);
                var content = await response.Content.ReadAsStringAsync();
                var apiResponse = System.Text.Json.JsonSerializer.Deserialize<CreateProductoResponse>(content)!;

                if(!response.IsSuccessStatusCode || !apiResponse.isSuccess)
                {
                    apiResponse.message = apiResponse.message ?? $"La API devolvió un error de estado: {response.StatusCode}.";
                }
                return apiResponse;
            }
            catch (Exception ex)
            {
                return new CreateProductoResponse
                {
                    isSuccess = false,
                    message = $"Error al crear el Producto: {ex.Message}"
                };
            }
        }

        public async Task<UpdateProductoResponse> UpdateProductoAsync(UpdateProductoModel producto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("Producto/Update", producto);
                var content = await response.Content.ReadAsStringAsync();
                var apiResponse = System.Text.Json.JsonSerializer.Deserialize<UpdateProductoResponse>(content)!;

                if (!response.IsSuccessStatusCode || !apiResponse.isSuccess)
                {
                    apiResponse.message = apiResponse.message ?? $"La API devolvió un error de estado: {response.StatusCode}.";
                }
                return apiResponse;
            }
            catch (Exception ex)
            {
                return new UpdateProductoResponse
                {
                    isSuccess = false,
                    message = $"Error al actualizar el Producto: {ex.Message}"
                };
            }
        }

        public async Task<DisableProductoResponse> DisableProductoAsync(int id)
        {
            try
            {
                var response = await _httpClient.PostAsync($"Producto/DisableProduct?id={id}", null);
                var content = await response.Content.ReadAsStringAsync();
                var apiResponse = System.Text.Json.JsonSerializer.Deserialize<DisableProductoResponse>(content)!;

                if (!response.IsSuccessStatusCode || !apiResponse.isSuccess)
                {
                    apiResponse.message = apiResponse.message ?? $"La API devolvió un error de estado: {response.StatusCode}.";
                }
                return apiResponse;
            }
            catch (Exception ex)
            {
                return new DisableProductoResponse
                {
                    isSuccess = false,
                    message = $"Error al deshabilitar el Producto con ID {id}: {ex.Message}"
                };
            }
        }
    }
}