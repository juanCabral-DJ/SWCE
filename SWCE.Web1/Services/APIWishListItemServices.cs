using SWCE.Aplicatition.Dtos.User;
using SWCE.Aplicatition.Dtos.WishListItem;
using SWCE.Aplicatition.Interfaces.Repositories.API_Interface;
using SWCE.Domain.Base;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using SWCE.Web1.Interfaces;
using SWCE.Web1.Models.Address;
using SWCE.Web1.Models.Base;
using SWCE.Web1.Models.User;
using SWCE.Web1.Models.WishListItem;
using System.Text.Json;

namespace SWCE.Web1.Services
{
    public class APIWishListItemServices : IAPIWishListItemServices
    {
        public readonly IAPIWishListItemRepository _api;

        public APIWishListItemServices(IAPIWishListItemRepository api)
        {
            _api = api;
        }

        public async Task<ModelResponse<CreateItemModel>> CreateItemasync(CreateItemModel entity)
        {
            try
            {
                var createDto = new CreateItemDto
                { 
                    id_producto = entity.id_producto,
                    Id_Usuario = entity.id_Usuario
                };

                var result = await _api.CreateItemasync(createDto);

                if (result.IsSuccess)
                {
                    // 1. Obtiene el JsonElement
                    JsonElement dataElement = result.Data;


                    //    Asumiendo que User es la clase que representa los datos que vienen de la API
                    var ItemCreate = JsonSerializer.Deserialize<WishListItem>(
                        dataElement.GetRawText(),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );

                    var Models = new CreateItemModel
                    {
                        id_producto = ItemCreate.id_producto,
                        id_Usuario = ItemCreate.Id_Usuario
                    };
                     

                    return ModelResponse<CreateItemModel>.Success(Models);
                }
                else
                {
                    return ModelResponse<CreateItemModel>.Failure(result.Message);
                }
            }
            catch (Exception ex)
            {
                return ModelResponse<CreateItemModel>.Failure($"Error creating item: {ex.Message}");
            }
            finally
            {

            }
        }

        public async Task<ModelResponse<DisableItemModel>> DisableItemAsync(DisableItemModel entity)
        {
            try
            {
                var disableDto = new DisableItemDto
                {
                    Id = entity.id, 
                };

                var result = await _api.DisableItemAsync(disableDto);

                if (result.IsSuccess)
                {
                    // 1. Obtiene el JsonElement
                    JsonElement dataElement = result.Data;


                    //    Asumiendo que User es la clase que representa los datos que vienen de la API
                    var ItemCreate = JsonSerializer.Deserialize<WishListItem>(
                        dataElement.GetRawText(),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );

                    var Models = new DisableItemModel
                    {
                        id = ItemCreate.id,
                    };


                    return ModelResponse<DisableItemModel>.Success(Models);
                }
                else
                {
                    return ModelResponse<DisableItemModel>.Failure(result.Message);
                }
            }
            catch (Exception ex)
            {
                return ModelResponse<DisableItemModel>.Failure($"Error disabling item: {ex.Message}");
            }
            finally
            {

            }
        }

        public async Task<ModelResponse<List<ItemModel>>> GetAllItemasync()
        {
            try
            {
                var result = await _api.GetAllItemasync();

                if (result.IsSuccess)
                {
                    // 1. Obtiene el JsonElement
                    JsonElement dataElement = result.Data;


                    var List = JsonSerializer.Deserialize<List<WishListItem>>(
                        dataElement.GetRawText(),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );

                    var Models = List.Select(u => new ItemModel
                    {
                        id = u.id,
                        id_producto = u.id_producto,
                        id_Usuario = u.Id_Usuario,
                    }).ToList();

                    return ModelResponse<List<ItemModel>>.Success(Models);
                }
                else
                {
                    return ModelResponse<List<ItemModel>>.Failure(result.Message);
                }
            }
            catch (Exception ex)
            {
                return ModelResponse<List<ItemModel>>.Failure($"Error retrieving Items: {ex.Message}");
            }
            finally
            {

            }
        }

        public async Task<ModelResponse<ItemModel>> GetbyIdasync(int id)
        {
            try
            {
                var result = await _api.GetbyIdasync(id);

                if (result.IsSuccess)
                {
                    // 1. Obtiene el JsonElement
                    JsonElement dataElement = result.Data;


                    var Item = JsonSerializer.Deserialize<WishListItem>(
                        dataElement.GetRawText(),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );

                    var Models = new ItemModel 
                    {
                        id = Item.id,
                        id_producto = Item.id_producto,
                        id_Usuario = Item.Id_Usuario,
                    };

                    return ModelResponse<ItemModel>.Success(Models);
                }
                else
                {
                    return ModelResponse<ItemModel>.Failure(result.Message);
                }
            }
            catch (Exception ex)
            {
                return ModelResponse<ItemModel>.Failure($"Error retrieving Items: {ex.Message}");
            }
            finally
            {

            }
        }

        public async Task<ModelResponse<List<ItemModel>>> GetbyUserid(int userId)
        {
            try
            {
                var result = await _api.GetbyUserid(userId);

                if (result.IsSuccess)
                {
                    // 1. Obtiene el JsonElement
                    JsonElement dataElement = result.Data;


                    var List = JsonSerializer.Deserialize<List<WishListItem>>(
                        dataElement.GetRawText(),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );

                    var Models = List.Select(u => new ItemModel
                    {
                        id = u.id,
                        id_producto = u.id_producto,
                        id_Usuario = u.Id_Usuario,
                    }).ToList();

                    return ModelResponse<List<ItemModel>>.Success(Models);
                }
                else
                {
                    return ModelResponse<List<ItemModel>>.Failure(result.Message);
                }
            }
            catch (Exception ex)
            {
                return ModelResponse<List<ItemModel>>.Failure($"Error retrieving Items: {ex.Message}");
            }
            finally
            {

            }
        }
    }
}
