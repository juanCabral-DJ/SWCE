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
    public class APIAddressRepository : APIRepositoryBase, IAPIAddressRepository
    {

        public APIAddressRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
             
        }

        public async Task<OperationResult> CreateAddressAsync(CreateAddressDto address)
        {
            return await PostAsync<CreateAddressDto>("Address/CreateAddressDto", address);
        }

        public async Task<OperationResult> DisableAddressAsync(UpdateOrDisableAddressDto address)
        {
            return await PostAsync<UpdateOrDisableAddressDto>("Address/DisableAddressDto", address);
        }

        public async Task<OperationResult> GetAddressByIdAsync(int id)
        {

            return await GetAsync($"Address/{id}");
        }

        public async Task<OperationResult> GetAddressByUserIdAsync(int id)
        {
            return await GetAsync($"Address/idUser/{id}");
        }

        public async Task<OperationResult> GetAllAddressesAsync()
        {
            return await GetAsync("Address");
        }

        public async Task<OperationResult> GetByUseridAdressPredeterminada(int id)
        {
                return await GetAsync($"Address/Predeterminada/id?id={id}");
        }

        public async Task<OperationResult> UpdateAddressAsync(UpdateOrDisableAddressDto address)
        {
            return await PostAsync<UpdateOrDisableAddressDto>("Address/UpdateAddressDto", address);
        }
    }
}
