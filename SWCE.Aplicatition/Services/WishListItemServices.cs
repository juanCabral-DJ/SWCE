using AutoMapper;
using Microsoft.Extensions.Configuration;
using SWCE.Aplicatition.Base;
using SWCE.Aplicatition.Dtos.WishListItem;
using SWCE.Aplicatition.Interfaces.Repositories.User_Perfil;
using SWCE.Aplicatition.Interfaces.Services;
using SWCE.Domain.Base;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using SWCE.Infraestructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Aplicatition.Services
{
     public sealed class WishListItemServices : IWishListItemServices
    {
        public readonly IRepositoryWishListItem _repository;
        public readonly Itemmapper _mapper;
        private readonly ILoggerBase<WishListItemServices> _logger;
        private readonly IConfiguration _configuration;

        public WishListItemServices(IRepositoryWishListItem repository, Itemmapper mapper, ILoggerBase<WishListItemServices> logger, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> Createasync(CreateItemDto entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                _logger.LogInformation("creating WishListItem entity"); 

               //Falta mapear
               var item = _mapper.MapToEntityCreate(entity);
                result = await _repository.Createasync(item);

                _logger.LogInformation("succefully created WishListItem entity");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while creating WishListItem", ex);
                result = OperationResult.Failure("An error occurred while creating WishListItem");
            }
            finally
            {
            }
            return result;
        }

        public async Task<OperationResult> Disableasync(DisableItemDto entity)
        {
            OperationResult result = new OperationResult();
            try
            {

                _logger.LogInformation("Disabling WishListItem with ID {id}.", entity.Id);

               //Falta mapear
                var ItemExist = _mapper.MapToEntity(entity);
                result = await _repository.DisableAsync(ItemExist);

                _logger.LogInformation("Successfully disabled WishListItem with ID {id}.", entity.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while disabling WishListItem.", ex);
                result = OperationResult.Failure("An error occurred while disabling the WishListItem.");
            }
            return result;
        }

        public async Task<OperationResult> GetAllasync(Expression<Func<WishListItem, bool>> filter)
        {
            OperationResult result = new OperationResult();
            try
            {
                _logger.LogInformation("Fetching all WishListItem.");

                result = await _repository.GetAllasync(a => a.IsDeleted == false);

                result = OperationResult.Success("WishListItem retrieved successfully.", result.Data);
                _logger.LogInformation("Successfully fetched WishListItem.");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while getting WishListItem.", ex);
                result = OperationResult.Failure("An error occurred while getting WishListItem.");
            }
            finally
            {

            }
            return result;
        }

        public async Task<OperationResult> Getbyid(int id)
        {
            OperationResult result = new OperationResult();
            try
            {
                _logger.LogInformation("Fetching WishListItem by id.");

                result = await _repository.GetbyIdasync(id);

                result = OperationResult.Success("WishListItem retrieved successfully.", result.Data);
                _logger.LogInformation("Successfully fetched WishListItem.");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while getting WishListItem.", ex);
                result = OperationResult.Failure("An error occurred while getting WishListItem.");
            }
            finally
            {

            }
            return result;
        }
        public async Task<OperationResult> GetbyUserId(int userId)
        {
            OperationResult result = new OperationResult();
            try
            {
                _logger.LogInformation("Fetching All Address by id.");

                result = await _repository.GetbyUserid(userId);

                result = OperationResult.Success("Address retrieved successfully.", result.Data);
                _logger.LogInformation("Successfully fetched Address.");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while getting Address.", ex);
                result = OperationResult.Failure("An error occurred while getting Address.");
            }
            finally
            {

            }
            return result;
        }
    } 
}
