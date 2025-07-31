using SWCE.Aplicatition.Dtos.WishListItem;
using SWCE.Aplicatition.Interfaces.Repositories.API_Interface;
using SWCE.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Persistence.ApiClients
{
    public class APIWishListItemRepository : IAPIWishListItemRepository
    {
        private readonly HttpClient _client;

        public APIWishListItemRepository(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("Client");
        }

        public async Task<OperationResult> CreateItemasync(CreateItemDto entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                var response = await _client.PostAsJsonAsync("WishListItem/CreateItemDto", entity);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadFromJsonAsync<OperationResult>();

                }
                else
                {
                    result = OperationResult.Failure("Error creating Items");
                }


            }
            catch (Exception ex)
            {
                result = OperationResult.Failure($"Error creating Items: {ex.Message}");
            }
            finally
            {

            }
            return result;
        }

        public async Task<OperationResult> DisableItemAsync(DisableItemDto entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                var response = await _client.PostAsJsonAsync("WishListItem/DisableItemDto", entity);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadFromJsonAsync<OperationResult>();

                }
                else
                {
                    result = OperationResult.Failure("Error disabling Items");
                }


            }
            catch (Exception ex)
            {
                result = OperationResult.Failure($"Error disabling Items: {ex.Message}");
            }
            finally
            {

            }
            return result;
        }

        public async Task<OperationResult> GetAllItemasync()
        {
            OperationResult result = new OperationResult();

            try
            {
                var response = await _client.GetAsync("WishListItem");
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadFromJsonAsync<OperationResult>();

                }
                else
                {
                    result = OperationResult.Failure("Error retrieving Items");
                }


            }
            catch (Exception ex)
            {
                result = OperationResult.Failure($"Error retrieving Items: {ex.Message}");
            }
            finally
            {

            }
            return result;
        }

        public async Task<OperationResult> GetbyIdasync(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                var response = await _client.GetAsync($"WishListItem/{id}");
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadFromJsonAsync<OperationResult>();

                }
                else
                {
                    result = OperationResult.Failure("Error retrieving Items");
                }


            }
            catch (Exception ex)
            {
                result = OperationResult.Failure($"Error retrieving Items: {ex.Message}");
            }
            finally
            {

            }
            return result;
        }

        public async Task<OperationResult> GetbyUserid(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                var response = await _client.GetAsync($"WishListItem/Item/{id}");
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadFromJsonAsync<OperationResult>();

                }
                else
                {
                    result = OperationResult.Failure("Error retrieving Items");
                }


            }
            catch (Exception ex)
            {
                result = OperationResult.Failure($"Error retrieving Items: {ex.Message}");
            }
            finally
            {

            }
            return result;
        }
    }
}
