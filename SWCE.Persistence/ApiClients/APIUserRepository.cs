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
    public class APIUserRepository : IAPIUserRepository
    {
        private readonly HttpClient _client;

        public APIUserRepository(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("Client");
        }

        public async Task<OperationResult> CreateUserAsync(CreateUserDto user)
        {
            OperationResult result = new OperationResult();

            try
            {
                var response = await _client.PostAsJsonAsync($"User/CreateUserDto", user);

                if (response.IsSuccessStatusCode)
                {

                    result = await response.Content.ReadFromJsonAsync<OperationResult>();

                }
                else
                {
                    result = OperationResult.Failure("Error creating user");
                }


            }
            catch (Exception ex)
            {
                result = OperationResult.Failure($"Error creating user: {ex.Message}");
            }
            finally
            {

            }
            return result;
        }

        public async Task<OperationResult> DisableUserAsync(DisableUserDto user)
        {
            OperationResult result = new OperationResult();

            try
            {
                var response = await _client.PostAsJsonAsync($"User/DisableUserDto", user);

                if (response.IsSuccessStatusCode)
                {

                    result = await response.Content.ReadFromJsonAsync<OperationResult>();

                }
                else
                {
                    result = OperationResult.Failure("Error retrieving user");
                }


            }
            catch (Exception ex)
            {
                result = OperationResult.Failure($"Error retrieving user: {ex.Message}");
            }
            finally
            {

            }
            return result;
        }

        public async Task<OperationResult> GetAllUsersAsync()
        {
             OperationResult result = new OperationResult();

            try
            {
                  var response = await _client.GetAsync("User/GetUser");
                if (response.IsSuccessStatusCode) 
                { 
                    result = await response.Content.ReadFromJsonAsync<OperationResult>();
                    
                }
                else
                {
                    result = OperationResult.Failure("Error retrieving users");
                }
                    

            }
            catch(Exception ex)
            {
                result = OperationResult.Failure($"Error retrieving users: {ex.Message}");
            }
            finally
            {

            }
            return result;
        }

        public async Task<OperationResult> GetUserByEmailAsync(string email)
        {

            OperationResult result = new OperationResult();

            try
            {
                var response = await _client.GetAsync($"User/Email?email={email}");
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadFromJsonAsync<OperationResult>();

                }
                else
                {
                    result = OperationResult.Failure("Error retrieving user");
                }


            }
            catch (Exception ex)
            {
                result = OperationResult.Failure($"Error retrieving user: {ex.Message}");
            }
            finally
            {

            }
            return result;
        }

        public async Task<OperationResult> GetUserByIdAsync(int id)
        {

            OperationResult result = new OperationResult();

            try
            {
                var response = await _client.GetAsync($"User/{id}");
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadFromJsonAsync<OperationResult>();

                }
                else
                {
                    result = OperationResult.Failure("Error retrieving user");
                }


            }
            catch (Exception ex)
            {
                result = OperationResult.Failure($"Error retrieving user: {ex.Message}");
            }
            finally
            {

            }
            return result;
        }

        public async Task<OperationResult> UpdateUserAsync(UpdateUserDto user)
        {
            OperationResult result = new OperationResult();

            try
            {
                var response = await _client.PostAsJsonAsync($"User/UpdateUserDto", user);

                if (response.IsSuccessStatusCode)
                {

                    result = await response.Content.ReadFromJsonAsync<OperationResult>();

                }
                else
                {
                    result = OperationResult.Failure("Error retrieving user");
                }


            }
            catch (Exception ex)
            {
                result = OperationResult.Failure($"Error retrieving user: {ex.Message}");
            }
            finally
            {

            }
            return result;
        }
    }
}
