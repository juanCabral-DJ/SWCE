using SWCE.Aplicatition.Dtos.User;
using SWCE.Aplicatition.Dtos.WishListItem;
using SWCE.Aplicatition.Interfaces.Repositories.API_Interface;
using SWCE.Domain.Base;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using SWCE.Web1.HttpServices.Interfaces;
using SWCE.Web1.HttpServices.MappingAPI;
using SWCE.Web1.Models.Address;
using SWCE.Web1.Models.Base;
using SWCE.Web1.Models.User;
using SWCE.Web1.Models.WishListItem;
using System.Text.Json;

namespace SWCE.Web1.HttpServices.Services
{
    public class APIWishListItemServices : IAPIWishListItemServices
    {
        public readonly IAPIWishListItemRepository _api;
        public readonly MapItem _map;

        public APIWishListItemServices(IAPIWishListItemRepository api, MapItem map)
        {
            _api = api;
            _map = map;
        }

        public async Task<ModelResponse<CreateItemModel>> CreateItemasync(CreateItemModel entity)
        {
                var createDto = _map.MapToCreateDto(entity);

                var result = await _api.CreateItemasync(createDto);

                return ProcessRepositoryResponse<WishListItem, CreateItemModel>(
                    result,
                    _map.MapToCreateItemModel
                );
            }

        public async Task<ModelResponse<DisableItemModel>> DisableItemAsync(DisableItemModel entity)
        {
                var disableDto = _map.MapToDisableDto(entity);

                var result = await _api.DisableItemAsync(disableDto);

                return ProcessRepositoryResponse<WishListItem, DisableItemModel>(
                    result,
                    _map.MapToDisableItemModel
                );
        }

        public async Task<ModelResponse<List<ItemModel>>> GetAllItemasync()
        {
                var result = await _api.GetAllItemasync();
            return ProcessRepositoryResponse<List<WishListItem>, List<ItemModel>>(
                result,
                _map.MapToModelList
            );
        }

        public async Task<ModelResponse<ItemModel>> GetbyIdasync(int id)
        {
            var result = await _api.GetbyIdasync(id);

            return ProcessRepositoryResponse<WishListItem, ItemModel>(result, _map.MapToModel);
        }

        public async Task<ModelResponse<List<ItemModel>>> GetbyUserid(int userId)
        {
                var result = await _api.GetbyUserid(userId);

                return ProcessRepositoryResponse<List<WishListItem>, List<ItemModel>>(
                    result,
                    _map.MapToModelList
                );
        }

        // Metodo auxiliar para cumplir con el principio DRY y evitar duplicación de código
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