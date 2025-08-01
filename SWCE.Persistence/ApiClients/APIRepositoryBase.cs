using SWCE.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SWCE.Persistence.ApiClients
{
    public class APIRepositoryBase
    {
        protected readonly HttpClient _httpClient;
        protected readonly JsonSerializerOptions _jsonOptions;

        protected APIRepositoryBase(IHttpClientFactory httpClientFactory, string clientName = "Client")
        {
           _httpClient = httpClientFactory.CreateClient(clientName); 
           _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
         
        };  
    }
 
        protected async Task<OperationResult> GetAsync(string endpoint)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<OperationResult>(endpoint, _jsonOptions);
                return response ?? OperationResult.Failure("La respuesta de la API fue nula.");  
        }
            catch (Exception ex)
            {
               return OperationResult.Failure($"Error de comunicación (GET): {ex.Message}");  
        }
        }
 
        protected async Task<OperationResult> PostAsync<TRequest>(string endpoint, TRequest data)
            where TRequest : class
        {
            try
            {
                var httpResponse = await _httpClient.PostAsJsonAsync(endpoint, data, _jsonOptions);
                if (!httpResponse.IsSuccessStatusCode)
                {
                   return OperationResult.Failure($"Error del API (POST): {httpResponse.ReasonPhrase}"); 
            }

                return await httpResponse.Content.ReadFromJsonAsync<OperationResult>(_jsonOptions)
                       ?? OperationResult.Failure("La respuesta de la API fue nula.");  
        }
            catch (Exception ex)
            {
                 return OperationResult.Failure($"Error de comunicación (POST): {ex.Message}");  
        }
        }
         
    }
}
