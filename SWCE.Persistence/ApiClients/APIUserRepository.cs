using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using SWCE.Aplicatition.Dtos.User;
using SWCE.Aplicatition.Interfaces.Repositories.API_Interface;
using SWCE.Domain.Base;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static NHibernate.Engine.Query.CallableParser;

namespace SWCE.Persistence.ApiClients
{
    public class APIUserRepository : APIRepositoryBase, IAPIUserRepository
    {
        private readonly HttpClient _client;

        public APIUserRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
          
        }

        public async Task<OperationResult> CreateUserAsync(CreateUserDto user)
        {
            return await  PostAsync($"User/CreateUserDto", user);
 
        }

        public async Task<OperationResult> DisableUserAsync(DisableUserDto user)
        {
            return await  PostAsync($"User/DisableUserDto", user);
        }

        public async Task<OperationResult> GetAllUsersAsync()
        {
             return await GetAsync("User/GetUser");
        }

        public async Task<OperationResult> GetUserByEmailAsync(string email)
        {
            return  await  GetAsync($"User/Email?email={email}");
        }

        public async Task<OperationResult> GetUserByIdAsync(int id)
        {
            return await GetAsync($"User/{id}");
        }
        public async Task<OperationResult> UpdateUserAsync(UpdateUserDto user)
        {
            return await PostAsync($"User/UpdateUserDto", user);
        }
    }
}
