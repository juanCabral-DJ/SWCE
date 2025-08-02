using SWCE.Web.Common;
using SWCE.Web.Models;
using SWCE.Web.Repositories.Interfaces;
using System.Text.Json;

namespace SWCE.Web.Repositories
{
    public class EnvioHttpService : IEnvioHttpService
    {
        private readonly HttpClient _httpClient;

        public EnvioHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result<IEnumerable<EnvioModel>>> GetAllEnviosAsync()
        {
            try
            { 
            var response = await _httpClient.GetAsync("Envio/GetEnvios");
            var result = await DeserializeResponseAsync<GetAllEnvioModelResponse>(response);
            return Result<IEnumerable<EnvioModel>>.Success(result.data ?? new List<EnvioModel>());
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<EnvioModel>>.Failure($"Error al obtener los envios: {ex.Message}");
            }
        }

        public async Task<Result<EnvioModel>> GetEnvioByIdAsync(int id)
        {
            try
            {
            var response = await _httpClient.GetAsync($"Envio/{id}");
            var result = await DeserializeResponseAsync<GetEnvioByIdModelResponse>(response);
            return Result<EnvioModel>.Success(result.data ?? new EnvioModel());
            }
            catch (Exception ex)
            {
                return Result<EnvioModel>.Failure($"Error al obtener el envio con ID {id}: {ex.Message}");
            }
        }

        public async Task<Result<EnvioCreateModelResponse>> CreateEnvioAsync(EnvioCreateModel model)
        {
            try
            { 
            var response = await _httpClient.PostAsJsonAsync("Envio/CreateEnvio", model);
            var result = await DeserializeResponseAsync<EnvioCreateModelResponse>(response);
            return Result<EnvioCreateModelResponse>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<EnvioCreateModelResponse>.Failure($"Error al crear el envio: {ex.Message}");
            }
        }

        public async Task<Result<EnvioEditModelResponse>> UpdateEnvioAsync(EnvioEditModel model)
        {
            try 
            { 
            var response = await _httpClient.PostAsJsonAsync($"Envio/UpdateEnvioDto", model);
            var result = await DeserializeResponseAsync<EnvioEditModelResponse>(response);
            return Result<EnvioEditModelResponse>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<EnvioEditModelResponse>.Failure($"Error al actualizar el envio: {ex.Message}");
            }
        }
        
        private async Task<T> DeserializeResponseAsync<T>(HttpResponseMessage jsonResponse)
        {
            var json = await jsonResponse.Content.ReadAsStringAsync();

            if (!jsonResponse.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error al llamar al servicio: {json}");
            }

            var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
            var result = JsonSerializer.Deserialize<T>(json, options);
            if (result == null)
            {
                throw new Exception("Error al deserializar la respuesta del servidor al crear el envio.");
            }
            return result;
        }
    }

}
