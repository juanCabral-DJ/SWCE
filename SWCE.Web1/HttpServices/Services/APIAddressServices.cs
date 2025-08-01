using Microsoft.Extensions.Options;
using SWCE.Aplicatition.Dtos.Address;
using SWCE.Aplicatition.Dtos.User;
using SWCE.Aplicatition.Interfaces.Repositories.API_Interface;
using SWCE.Domain.Base;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using SWCE.Web1.HttpServices.Interfaces;
using SWCE.Web1.HttpServices.MappingAPI;
using SWCE.Web1.Models.Address;
using SWCE.Web1.Models.Base;
using SWCE.Web1.Models.User;
using System.Text.Json;

namespace SWCE.Web1.HttpServices.Services
{
    public class APIAddressServices : IAPIAddressServices
    {
        private readonly IAPIAddressRepository _api;
        private readonly MapAddress _map;
        public APIAddressServices(IAPIAddressRepository api, MapAddress map)
        {
            _api = api;
            _map = map;
        }

        public async Task<ModelResponse<CreateAddressModel>> CreateAddressAsync(CreateAddressModel address)
        {
            var CreateAddressDto = _map.MapToCreateDto(address);

            var result = await _api.CreateAddressAsync(CreateAddressDto);

            return ProcessRepositoryResponse<Address, CreateAddressModel>(result, _map.MapToCreateAddressModel);
        }

        public async Task<ModelResponse<DisableAddressModel>> DisableAddressAsync(DisableAddressModel address)
        {
            var DisableAddressDto = _map.MapToDisableDto(address);

            var result = await _api.DisableAddressAsync(DisableAddressDto);

            return ProcessRepositoryResponse<Address, DisableAddressModel>(result, _map.MapToDisableAddressModel);
        }

        public async Task<ModelResponse<AddressModel>> GetAddressByIdAsync(int id)
        {

            var result = await _api.GetAddressByIdAsync(id);

            return ProcessRepositoryResponse<Address, AddressModel>(result, _map.MapToModel);

        }

        public async Task<ModelResponse<List<AddressModel>>> GetAddressByUserIdAsync(int userId)
        {

            var result = await _api.GetAddressByUserIdAsync(userId);

            return ProcessRepositoryResponse<List<Address>, List<AddressModel>>(result, _map.MapToModelList);
        }

        public async Task<ModelResponse<List<AddressModel>>> GetAllAddressesAsync()
        {

            var result = await _api.GetAllAddressesAsync();

            return ProcessRepositoryResponse<List<Address>, List<AddressModel>>(result, _map.MapToModelList);
        }

        public async Task<ModelResponse<AddressModel>> GetByUseridAdressPredeterminada(int ID_Usuario)
        {

            var result = await _api.GetByUseridAdressPredeterminada(ID_Usuario);

            return ProcessRepositoryResponse<Address, AddressModel>(result, _map.MapToModel);
        }

        public async Task<ModelResponse<EditAddressModel>> UpdateAddressAsync(EditAddressModel address)
        {

            var UpdateAddressDto = _map.MapToUpdateDto(address);

            var result = await _api.UpdateAddressAsync(UpdateAddressDto);

            return ProcessRepositoryResponse<Address ,  EditAddressModel>(result, _map.MapToEditAddressModel);
        }


        //Metodo auxiliar para cumplir con el principio DRY y evitar duplicación de código
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
