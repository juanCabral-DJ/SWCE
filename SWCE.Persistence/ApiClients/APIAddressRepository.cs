using SWCE.Aplicatition.Dtos.Address;
using SWCE.Aplicatition.Interfaces.Repositories.API_Interface;
using SWCE.Domain.Base;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Persistence.ApiClients
{
    public class APIAddressRepository : IAPIAddressRepository
    {
        private readonly HttpClient _client;

        public APIAddressRepository(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("Client");
        }

        public async Task<OperationResult> CreateAddressAsync(CreateAddressDto address)
        {
            OperationResult result = new OperationResult();

            try
            {
                var response = await _client.PostAsJsonAsync($"Address/CreateAddressDto", address);

                if (response.IsSuccessStatusCode)
                {

                    result = await response.Content.ReadFromJsonAsync<OperationResult>();

                }
                else
                {
                    result = OperationResult.Failure("Error creating Adress");
                }


            }
            catch (Exception ex)
            {
                result = OperationResult.Failure($"Error creating Address: {ex.Message}");
            }
            finally
            {

            }
            return result;
        }

        public async Task<OperationResult> DisableAddressAsync(UpdateOrDisableAddressDto address)
        {
            OperationResult result = new OperationResult();

            try
            {
                var response = await _client.PostAsJsonAsync($"Address/DisableAddressDto", address);

                if (response.IsSuccessStatusCode)
                {

                    result = await response.Content.ReadFromJsonAsync<OperationResult>();

                }
                else
                {
                    result = OperationResult.Failure("Error disabling Adress");
                }


            }
            catch (Exception ex)
            {
                result = OperationResult.Failure($"Error disabling Address: {ex.Message}");
            }
            finally
            {

            }
            return result;
        }

        public async Task<OperationResult> GetAddressByIdAsync(int id)
        {

            OperationResult result = new OperationResult();

            try
            {
                var response = await _client.GetAsync($"Address/{id}");
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadFromJsonAsync<OperationResult>();

                }
                else
                {
                    result = OperationResult.Failure("Error retrieving Address");
                }


            }
            catch (Exception ex)
            {
                result = OperationResult.Failure($"Error retrieving Address: {ex.Message}");
            }
            finally
            {

            }
            return result;
        }

        public async Task<OperationResult> GetAddressByUserIdAsync(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                var response = await _client.GetAsync($"Address/idUser/{id}");
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadFromJsonAsync<OperationResult>();

                }
                else
                {
                    result = OperationResult.Failure("Error retrieving Addresses");
                }


            }
            catch (Exception ex)
            {
                result = OperationResult.Failure($"Error retrieving Addresses: {ex.Message}");
            }
            finally
            {

            }
            return result;
        }

        public async Task<OperationResult> GetAllAddressesAsync()
        {
            OperationResult result = new OperationResult();

            try
            {
                var response = await _client.GetAsync("Address");
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadFromJsonAsync<OperationResult>();

                }
                else
                {
                    result = OperationResult.Failure("Error retrieving Addresses");
                }


            }
            catch (Exception ex)
            {
                result = OperationResult.Failure($"Error retrieving Addresses: {ex.Message}");
            }
            finally
            {

            }
            return result;
        }

        public async Task<OperationResult> GetByUseridAdressPredeterminada(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                var response = await _client.GetAsync($"Address/Predeterminada/id?id={id}");
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadFromJsonAsync<OperationResult>();

                }
                else
                {
                    result = OperationResult.Failure("Error retrieving Address");
                }


            }
            catch (Exception ex)
            {
                result = OperationResult.Failure($"Error retrieving Address: {ex.Message}");
            }
            finally
            {

            }
            return result;
        }

        public async Task<OperationResult> UpdateAddressAsync(UpdateOrDisableAddressDto address)
        {
            OperationResult result = new OperationResult();

            try
            {
                var response = await _client.PostAsJsonAsync($"Address/UpdateAddressDto", address);

                if (response.IsSuccessStatusCode)
                {

                    result = await response.Content.ReadFromJsonAsync<OperationResult>();

                }
                else
                {
                    result = OperationResult.Failure("Error updating Adress");
                }


            }
            catch (Exception ex)
            {
                result = OperationResult.Failure($"Error updating Address: {ex.Message}");
            }
            finally
            {

            }
            return result;
        }
    }
}
