using Microsoft.Extensions.Options;
using SWCE.Aplicatition.Dtos.Address;
using SWCE.Aplicatition.Dtos.User;
using SWCE.Aplicatition.Interfaces.Repositories.API_Interface;
using SWCE.Domain.Base;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using SWCE.Web1.Interfaces;
using SWCE.Web1.Models.Address;
using SWCE.Web1.Models.Base;
using SWCE.Web1.Models.User;
using System.Text.Json;

namespace SWCE.Web1.Services
{
    public class APIAddressServices : IAPIAddressServices
    {
        private readonly IAPIAddressRepository _api ;

        public APIAddressServices(IAPIAddressRepository api)
        {
            _api = api;
        }

        public async Task<ModelResponse<CreateAddressModel>> CreateAddressAsync(CreateAddressModel address)
        {
            try
            {
                var CreateAddressDto = new CreateAddressDto
                {
                    calle = address.calle,
                    ciudad = address.ciudad,
                    estado_provincia = address.estado_provincia,
                    codigo_postal = address.codigo_postal,
                    pais = address.pais,
                    ID_Usuario = address.iD_Usuario,
                    Es_predeterminada = address.es_predeterminada
                };

                var result = await _api.CreateAddressAsync(CreateAddressDto);

                if (result.IsSuccess)
                {
                    // 1. Obtiene el JsonElement
                    JsonElement dataElement = result.Data;


            
                    var AddressCreate = JsonSerializer.Deserialize<Address>(
                        dataElement.GetRawText(),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );

                    var  Models = new CreateAddressModel
                    {
                        calle = AddressCreate.calle,
                        ciudad = AddressCreate.ciudad,
                        estado_provincia = AddressCreate.estado_provincia,
                        codigo_postal = AddressCreate.codigo_postal,
                        pais = AddressCreate.pais,
                        iD_Usuario = AddressCreate.ID_Usuario,
                        es_predeterminada = AddressCreate.Es_predeterminada
                    };

                    return ModelResponse<CreateAddressModel>.Success(Models);
                }
                else
                {
                    return ModelResponse<CreateAddressModel>.Failure(result.Message);
                }
            }
            catch (Exception ex)
            {
                return ModelResponse<CreateAddressModel>.Failure($"Error creating address: {ex.Message}");
            }
            finally
            {

            }
        }

        public async Task<ModelResponse<DisableAddressModel>> DisableAddressAsync(DisableAddressModel address)
        {
            try
            {
                var DisableAddressDto = new UpdateOrDisableAddressDto
                {
                    id = address.id,
                };

                var result = await _api.DisableAddressAsync(DisableAddressDto);

                if (result.IsSuccess)
                {
                    // 1. Obtiene el JsonElement
                    JsonElement dataElement = result.Data;


                    //    Asumiendo que User es la clase que representa los datos que vienen de la API
                    var AddressDisable = JsonSerializer.Deserialize<Address>(
                        dataElement.GetRawText(),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );

                    var Models = new DisableAddressModel
                    {
                        id = AddressDisable.id,
                    };

                    return ModelResponse<DisableAddressModel>.Success(Models);
                }
                else
                {
                    return ModelResponse<DisableAddressModel>.Failure(result.Message);
                }
            }
            catch (Exception ex)
            {
                return ModelResponse<DisableAddressModel>.Failure($"Error Disabling Address: {ex.Message}");
            }
            finally
            {

            }
        }

        public async Task<ModelResponse<AddressModel>> GetAddressByIdAsync(int id)
        {
            try
            {
                var result = await _api.GetAddressByIdAsync(id);

                if (result.IsSuccess)
                {
                    // 1. Obtiene el JsonElement
                    JsonElement dataElement = result.Data;

                     
                    var Address = JsonSerializer.Deserialize<Address>(
                        dataElement.GetRawText(),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );

                    var Models = new AddressModel
                    {
                        id = Address.id,
                        calle = Address.calle,
                        ciudad = Address.ciudad,
                        estado_provincia = Address.estado_provincia,
                        codigo_postal = Address.codigo_postal,
                        pais = Address.pais,
                        iD_Usuario = Address.ID_Usuario,
                        es_predeterminada = Address.Es_predeterminada,

                    };

                    return ModelResponse<AddressModel>.Success(Models);
                }
                else
                {
                    return ModelResponse<AddressModel>.Failure(result.Message);
                }
            }
            catch (Exception ex)
            {
                return ModelResponse<AddressModel>.Failure($"Error retrieving Address: {ex.Message}");
            }
            finally
            {

            }
        }

        public async Task<ModelResponse<List<AddressModel>>> GetAddressByUserIdAsync(int userId)
        {
            try
            {
                var result = await _api.GetAddressByUserIdAsync(userId);

                if (result.IsSuccess)
                {
                    // 1. Obtiene el JsonElement
                    JsonElement dataElement = result.Data;


                    var List = JsonSerializer.Deserialize<List<Address>>(
                        dataElement.GetRawText(),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );

                    var Models = List.Select(u => new AddressModel
                    {
                        id = u.id,
                        calle = u.calle,
                        ciudad = u.ciudad,
                        estado_provincia = u.estado_provincia,
                        codigo_postal = u.codigo_postal,
                        pais = u.pais,
                        es_predeterminada = u.Es_predeterminada,
                        iD_Usuario = u.ID_Usuario,

                    }).ToList();

                    return ModelResponse<List<AddressModel>>.Success(Models);
                }
                else
                {
                    return ModelResponse<List<AddressModel>>.Failure(result.Message);
                }
            }
            catch (Exception ex)
            {
                return ModelResponse<List<AddressModel>>.Failure($"Error retrieving Address: {ex.Message}");
            }
            finally
            {

            }
        }

        public async Task<ModelResponse<List<AddressModel>>> GetAllAddressesAsync()
        {
            try
            {
                var result = await _api.GetAllAddressesAsync();

                if (result.IsSuccess)
                {
                    // 1. Obtiene el JsonElement
                    JsonElement dataElement = result.Data;

                     
                    var List = JsonSerializer.Deserialize<List<Address>>(
                        dataElement.GetRawText(),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );

                    var  Models = List.Select(u => new AddressModel
                    {
                        id = u.id,
                       calle = u.calle,
                          ciudad = u.ciudad,
                            estado_provincia = u.estado_provincia,
                            codigo_postal = u.codigo_postal,
                            pais = u.pais,
                            es_predeterminada = u.Es_predeterminada,
                        iD_Usuario = u.ID_Usuario,

                    }).ToList();

                    return ModelResponse<List<AddressModel>>.Success(Models);
                }
                else
                {
                    return ModelResponse<List<AddressModel>>.Failure(result.Message);
                }
            }
            catch (Exception ex)
            {
                return ModelResponse<List<AddressModel>>.Failure($"Error retrieving Address: {ex.Message}");
            }
            finally
            {

            }
        }

        public async Task<ModelResponse<AddressModel>> GetByUseridAdressPredeterminada(int ID_Usuario)
        {
            try
            {
                var result = await _api.GetByUseridAdressPredeterminada(ID_Usuario);

                if (result.IsSuccess)
                {
                    // 1. Obtiene el JsonElement
                    JsonElement dataElement = result.Data;
                     
                    var Address = JsonSerializer.Deserialize<Address>(dataElement, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    var Models = new AddressModel
                    {
                        id = Address.id,
                        calle = Address.calle,
                        ciudad = Address.ciudad,
                        estado_provincia = Address.estado_provincia,
                        codigo_postal = Address.codigo_postal,
                        pais = Address.pais,
                        iD_Usuario = Address.ID_Usuario,
                        es_predeterminada = Address.Es_predeterminada,

                    };

                    return ModelResponse<AddressModel>.Success(Models);
                }
                else
                {
                    return ModelResponse<AddressModel>.Failure(result.Message);
                }
            }
            catch (Exception ex)
            {
                return ModelResponse<AddressModel>.Failure($"Error retrieving Address: {ex.Message}");
            }
            finally
            {

            }
        }

        public async Task<ModelResponse<EditAddressModel>> UpdateAddressAsync(EditAddressModel address)
        {
            try
            {
                var UpdateAddressDto = new UpdateOrDisableAddressDto
                {
                    id = address.id,  
                };

                var result = await _api.UpdateAddressAsync(UpdateAddressDto);

                if (result.IsSuccess)
                {
                    // 1. Obtiene el JsonElement
                    JsonElement dataElement = result.Data;


                    //    Asumiendo que User es la clase que representa los datos que vienen de la API
                    var AddressUpdate = JsonSerializer.Deserialize<Address>(
                        dataElement.GetRawText(),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );

                    var  Models = new EditAddressModel
                    {
                        id = AddressUpdate.id,
                    };

                    return ModelResponse<EditAddressModel>.Success(Models);
                }
                else
                {
                    return ModelResponse<EditAddressModel>.Failure(result.Message);
                }
            }
            catch (Exception ex)
            {
                return ModelResponse<EditAddressModel>.Failure($"Error updating Address: {ex.Message}");
            }
            finally
            {

            }
        }
    }
}
 