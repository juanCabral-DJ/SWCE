using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using SWCE.Aplicatition.Dtos.User;
using SWCE.Aplicatition.Interfaces.Repositories.API_Interface;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using SWCE.Web1.Interfaces;
using SWCE.Web1.Models.Base;
using SWCE.Web1.Models.User;
using System.Text.Json;

namespace SWCE.Web1.Services
{
    public class APIUserServices : IAPIUserServices
    {
        private readonly IAPIUserRepository _apiUser;

        public APIUserServices(IAPIUserRepository apiUserRepository)
        {
            _apiUser  = apiUserRepository;
        }

        public async Task<ModelResponse<UserModelCreate>> CreateUserAsync(UserModelCreate user)
        {
            try
            {
                var createUserDto = new CreateUserDto
                {
                    Nombre =  user.nombre,
                    apellido = user.apellido,
                    email = user.email,
                    password = user.password,
                    id_rol =  user.id_rol,
                    Fecha_Creacion = DateTime.Now

                };

                var result = await _apiUser.CreateUserAsync(createUserDto);

                if (result.IsSuccess)
                {
                    // 1. Obtiene el JsonElement
                    JsonElement dataElement = result.Data;


                    //    Asumiendo que User es la clase que representa los datos que vienen de la API
                    var UserCreate = JsonSerializer.Deserialize<User>(
                        dataElement.GetRawText(),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );

                    var userModels = new UserModelCreate
                    {
                        id_rol = UserCreate.id_rol,
                        nombre = UserCreate.Nombre,
                        apellido = UserCreate.apellido,
                        email = UserCreate.email,
                        fecha_Creacion = UserCreate.Fecha_Creacion

                    };

                    return ModelResponse<UserModelCreate>.Success(userModels);
                }
                else
                {
                    return ModelResponse<UserModelCreate>.Failure(result.Message);
                }
            }
            catch (Exception ex)
            {
                return ModelResponse<UserModelCreate>.Failure($"Error retrieving users: {ex.Message}");
            }
            finally
            {

            }
        }

        public async Task<ModelResponse<DisableUserModel>> DisableUserAsync(DisableUserModel user)
        {
            try
            {
                var DisableUserDto = new DisableUserDto
                {
                    id = user.id,
                };

                var result = await _apiUser.DisableUserAsync(DisableUserDto);

                if (result.IsSuccess)
                {
                    // 1. Obtiene el JsonElement
                    JsonElement dataElement = result.Data;


                    //    Asumiendo que User es la clase que representa los datos que vienen de la API
                    var UserDisable = JsonSerializer.Deserialize<User>(
                        dataElement.GetRawText(),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );

                    var userModels = new DisableUserModel 
                    {

                        id = UserDisable.id,

                    };

                    return ModelResponse<DisableUserModel>.Success(userModels);
                }
                else
                {
                    return ModelResponse<DisableUserModel>.Failure(result.Message);
                }
            }
            catch (Exception ex)
            {
                return ModelResponse<DisableUserModel>.Failure($"Error retrieving users: {ex.Message}");
            }
            finally
            {

            }
        }

        public async Task<ModelResponse<List<UserModel>>> GetAllUsersAsync()
        {
            
            try
            {
                var result = await _apiUser.GetAllUsersAsync();

                if(result.IsSuccess)
        {
                    // 1. Obtiene el JsonElement
                    JsonElement dataElement = result.Data;

                    // 2. CORRECCIÓN: Deserializa el JSON a una lista de DTOs (UserDto)
                    //    Asumiendo que UserDto es la clase que representa los datos que vienen de la API
                    var userList = JsonSerializer.Deserialize<List<User>>(
                        dataElement.GetRawText(),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );

                    var userModels = userList.Select(u => new UserModel
                    {
                         id = u.id,
                         nombre = u.Nombre,
                         apellido = u.apellido,
                         email = u.email,
                         fecha_Creacion = u.Fecha_Creacion

                    }).ToList();

                    return ModelResponse<List<UserModel>>.Success(userModels);
                }
                else
                {
                    return ModelResponse<List<UserModel>>.Failure(result.Message);
                }
            }
            catch (Exception ex)
            {
                return ModelResponse<List<UserModel>>.Failure($"Error retrieving users: {ex.Message}");
            }
            finally
            {

            }
        }

        public async Task<ModelResponse<UserModel>> GetUserByEmailAsync(string email)
        {
            try
            {
                var result = await _apiUser.GetUserByEmailAsync(email);

                if (result.IsSuccess)
                {
                    // 1. Obtiene el JsonElement
                    JsonElement dataElement = result.Data;

                     
                    //    Asumiendo que User es la clase que representa los datos que vienen de la API
                    var user = JsonSerializer.Deserialize<User>(
                        dataElement.GetRawText(),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );

                    var userModels = new UserModel
                    {
                        id = user.id,
                        nombre = user.Nombre,
                        apellido = user.apellido,
                        email = user.email,
                        fecha_Creacion = user.Fecha_Creacion

                    };

                    return ModelResponse<UserModel>.Success(userModels);
                }
                else
                {
                    return ModelResponse<UserModel>.Failure(result.Message);
                }
            }
            catch (Exception ex)
            {
                return ModelResponse<UserModel>.Failure($"Error retrieving users: {ex.Message}");
            }
            finally
            {

            }
        }

        public async Task<ModelResponse<UserModel>> GetUserByIdAsync(int id)
        {
            try
            {
                var result = await _apiUser.GetUserByIdAsync(id);

                if (result.IsSuccess)
                {
                    // 1. Obtiene el JsonElement
                    JsonElement dataElement = result.Data;

                    // 2. CORRECCIÓN: Deserializa el JSON a una lista de DTOs (UserDto)
                    //    Asumiendo que UserDto es la clase que representa los datos que vienen de la API
                    var user  = JsonSerializer.Deserialize<User>(
                        dataElement.GetRawText(),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );

                    var userModels =  new UserModel
                    {
                        id = user.id,
                        nombre = user.Nombre,
                        apellido = user.apellido,
                        email = user.email,
                        fecha_Creacion = user.Fecha_Creacion

                    };

                    return ModelResponse<UserModel>.Success(userModels);
                }
                else
                {
                    return ModelResponse<UserModel>.Failure(result.Message);
                }
            }
            catch (Exception ex)
            {
                return ModelResponse<UserModel>.Failure($"Error retrieving users: {ex.Message}");
            }
            finally
            {

            }
        }

        public async Task<ModelResponse<UserModelEdit>> UpdateUserAsync(UserModelEdit user)
        {
            try
            {
                var UpdateUserDto = new UpdateUserDto
                {
                    id = user.id,
                    email = user.email,
                    password = user.password,
                };

                var result = await _apiUser.UpdateUserAsync(UpdateUserDto);

                if (result.IsSuccess)
                {
                    // 1. Obtiene el JsonElement
                    JsonElement dataElement = result.Data;


                    //    Asumiendo que User es la clase que representa los datos que vienen de la API
                    var UserUpdate = JsonSerializer.Deserialize<User>(
                        dataElement.GetRawText(),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );

                    var userModels = new UserModelEdit
                    {
                         
                        email = UserUpdate.email,
                        password = UserUpdate.password,

                    };

                    return ModelResponse<UserModelEdit>.Success(userModels);
                }
                else
                {
                    return ModelResponse<UserModelEdit>.Failure(result.Message);
                }
            }
            catch (Exception ex)
            {
                return ModelResponse<UserModelEdit>.Failure($"Error retrieving users: {ex.Message}");
            }
            finally
            {

            }
        }
    }
}
