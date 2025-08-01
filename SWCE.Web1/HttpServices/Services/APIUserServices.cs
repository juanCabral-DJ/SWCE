using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using SWCE.Aplicatition.Dtos.User;
using SWCE.Aplicatition.Interfaces.Repositories.API_Interface;
using SWCE.Domain.Base;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using SWCE.Web1.HttpServices.Interfaces;
using SWCE.Web1.HttpServices.MappingAPI;
using SWCE.Web1.Models.Base;
using SWCE.Web1.Models.User;
using System.Text.Json;

namespace SWCE.Web1.HttpServices.Services
{
    public class APIUserServices : IAPIUserServices
    {
        private readonly IAPIUserRepository _apiUser;
        private readonly MapUser _map;

        public APIUserServices(IAPIUserRepository apiUserRepository, MapUser map)
        {
            _apiUser  = apiUserRepository;
            _map = map;
        }

        public async Task<ModelResponse<UserModelCreate>> CreateUserAsync(UserModelCreate user)
        {
             
                var createUserDto = _map.MapToCreateDto(user);
 
                var result = await _apiUser.CreateUserAsync(createUserDto);

                return ProcessRepositoryResponse<User, UserModelCreate>(result, _map.MapToUserModelCreate);
        }

        public async Task<ModelResponse<DisableUserModel>> DisableUserAsync(DisableUserModel user)
        { 
                var DisableUserDto = _map.MapToDisableDto(user);

                return ProcessRepositoryResponse<User, DisableUserModel>(
                await _apiUser.DisableUserAsync(DisableUserDto), 
                _map.MapToDisableUserModel);
        }

        public async Task<ModelResponse<List<UserModel>>> GetAllUsersAsync()
        { 
                var result = await _apiUser.GetAllUsersAsync();
                return ProcessRepositoryResponse<List<User>, List<UserModel>>(
                result, 
                _map.MapToModelList);
        }

        public async Task<ModelResponse<UserModel>> GetUserByEmailAsync(string email)
        { 
                var result = await _apiUser.GetUserByEmailAsync(email);
                return ProcessRepositoryResponse<User, UserModel>(
                result, 
                _map.MapToModel);
        }

        public async Task<ModelResponse<UserModel>> GetUserByIdAsync(int id)
        { 
                var result = await _apiUser.GetUserByIdAsync(id);

                return ProcessRepositoryResponse<User, UserModel>(result, _map.MapToModel);
        }

        public async Task<ModelResponse<UserModelEdit>> UpdateUserAsync(UserModelEdit user)
        { 
                var UpdateUserDto = _map.MapToUpdateDto(user);

                return ProcessRepositoryResponse<User, UserModelEdit>(
                    await _apiUser.UpdateUserAsync(UpdateUserDto),
                    _map.MapToUserModelEdit
                );
            }

        
        
        // metodo auxiliar para cumplir con el principio DRY y evitar duplicación de código
        private ModelResponse<TModel> ProcessRepositoryResponse<TData, TModel>(OperationResult repoResult,
        Func<TData, TModel> mapper) where TModel : class
        {
            // 1. Verificación del repos.
            if (!repoResult.IsSuccess)
            {
                return ModelResponse<TModel>.Failure(repoResult.Message);
            }

            try
            {
                // 2. Deserializa el JsonElement del repositorio.
                var dataObject = JsonSerializer.Deserialize<TData>(
                    repoResult.Data.GetRawText(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                // 3. Usa la función de mapeo para convertir el DTO al Modelo final (TModel).
                var model = mapper(dataObject);

                // 4. Devuelve la respuesta exitosa con el modelo ya transformado.
                return ModelResponse<TModel>.Success(model);
            }
            catch (JsonException ex)
            {
                // Maneja cualquier error durante la deserialización.
                return ModelResponse<TModel>.Failure($"Error al procesar la respuesta de la API: {ex.Message}");
            }
        }
    }
}
