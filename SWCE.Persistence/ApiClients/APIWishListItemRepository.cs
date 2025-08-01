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
    public class APIWishListItemRepository : APIRepositoryBase, IAPIWishListItemRepository
    {
        private readonly HttpClient _client;

        public APIWishListItemRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {

        }

        public async Task<OperationResult> CreateItemasync(CreateItemDto entity)
        {
            return await PostAsync("WishListItem/CreateItemDto", entity);

        }

        public async Task<OperationResult> DisableItemAsync(DisableItemDto entity)
        {
            return await PostAsync("WishListItem/DisableItemDto", entity);

        }

        public async Task<OperationResult> GetAllItemasync()
        {
            return await GetAsync("WishListItem");

        }

        public async Task<OperationResult> GetbyIdasync(int id)
        {
            return await GetAsync($"WishListItem/{id}");
        
        }

        public async Task<OperationResult> GetbyUserid(int id)
        {
            return await GetAsync($"WishListItem/Item/{id}");
        
        }
    }
}
