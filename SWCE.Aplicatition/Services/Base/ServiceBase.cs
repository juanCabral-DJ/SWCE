using SWCE.Application.Base;
using SWCE.Domain.Base;
using SWCE.Infraestructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Services.Base
{
    public abstract class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : AuditEntity
    {
        protected readonly IRepositoryBase<TEntity> _repository;
        protected readonly ILoggerBase<ServiceBase<TEntity>> _logger;

        public ServiceBase(IRepositoryBase<TEntity> repository, ILoggerBase<ServiceBase<TEntity>> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public virtual async Task<OperationResult> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Recuperando {typeof(TEntity).Name} con ID: {id}");
                // Asumimos que GetbyIdasync del repositorio devuelve TEntity
                var entity = await _repository.GetbyIdasync(id);

                if (entity == null)
                {
                    _logger.LogInformation($"{typeof(TEntity).Name} con ID: {id} no encontrado.");
                    return OperationResult.Failure($"{typeof(TEntity).Name} con ID: {id} no encontrado.");
                }

                _logger.LogInformation($"{typeof(TEntity).Name} con ID: {id} recuperado.");
                return OperationResult.Success($"{typeof(TEntity).Name} recuperado exitosamente.", entity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al recuperar {typeof(TEntity).Name} con ID: {id}: {ex.Message}");
                return OperationResult.Failure($"Ocurrió un error al recuperar el {typeof(TEntity).Name}: {ex.Message}");
            }
        }

        public virtual async Task<OperationResult> GetAllAsync()
        {
            try
            {
                _logger.LogInformation($"Recuperando todos los {typeof(TEntity).Name}s.");
                // Asumimos que GetAllasync del repositorio devuelve List<TEntity>
                var entities = await _repository.GetAllasync();

                if (entities == null)
                {
                    _logger.LogInformation($"No se encontraron {typeof(TEntity).Name}s.");
                    return OperationResult.Failure($"No se encontraron {typeof(TEntity).Name}s.");
                }

                _logger.LogInformation($"Se recuperaron exitosamente");
                return OperationResult.Success($"{typeof(TEntity).Name}s recuperados exitosamente.", entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al recuperar todos los {typeof(TEntity).Name}s: {ex.Message}");
                return OperationResult.Failure($"Ocurrió un error al recuperar todos los {typeof(TEntity).Name}s: {ex.Message}");
            }
        }

        public virtual async Task<OperationResult> CreateAsync(TEntity entity)
        {
            try
            {
                _logger.LogInformation($"Intentando crear {typeof(TEntity).Name}.");
                if (entity == null)
                {
                    _logger.LogError($"Se intentó crear un {typeof(TEntity).Name} nulo.");
                    return OperationResult.Failure($"El {typeof(TEntity).Name} no puede ser nulo.");
                }

                // El repositorio devuelve OperationResult
                var result = await _repository.Createasync(entity);

                if (!result.IsSuccess)
                {
                    _logger.LogError($"Error al crear {typeof(TEntity).Name}: {result.Message}");
                    return OperationResult.Failure($"No se pudo crear el {typeof(TEntity).Name}: {result.Message}");
                }

                _logger.LogInformation($"{typeof(TEntity).Name} creado exitosamente.");
                return OperationResult.Success($"{typeof(TEntity).Name} creado exitosamente.", result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inesperado al crear {typeof(TEntity).Name}: {ex.Message}");
                return OperationResult.Failure($"Ocurrió un error inesperado al crear el {typeof(TEntity).Name}: {ex.Message}");
            }
        }

        public virtual async Task<OperationResult> UpdateAsync(TEntity entity)
        {
            try
            {
                _logger.LogInformation($"Intentando actualizar {typeof(TEntity).Name}.");
                if (entity == null)
                {
                    _logger.LogError($"Se intentó actualizar un {typeof(TEntity).Name} nulo.");
                    return OperationResult.Failure($"El {typeof(TEntity).Name} no puede ser nulo para la actualización.");
                }

                // El repositorio devuelve OperationResult
                var result = await _repository.Updateasync(entity);

                if (!result.IsSuccess)
                {
                    _logger.LogError($"Error al actualizar {typeof(TEntity).Name}: {result.Message}");
                    return OperationResult.Failure($"No se pudo actualizar el {typeof(TEntity).Name}: {result.Message}");
                }

                _logger.LogInformation($"{typeof(TEntity).Name} actualizado exitosamente.");
                return OperationResult.Success($"{typeof(TEntity).Name} actualizado exitosamente.", result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inesperado al actualizar {typeof(TEntity).Name}: {ex.Message}");
                return OperationResult.Failure($"Ocurrió un error inesperado al actualizar el {typeof(TEntity).Name}: {ex.Message}");
            }
        }

        public virtual async Task<OperationResult> DisableAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Intentando deshabilitar {typeof(TEntity).Name} con ID: {id}");

                var getResult = await GetByIdAsync(id);
                if (!getResult.IsSuccess || getResult.Data == null)
                {
                    _logger.LogInformation($"{typeof(TEntity).Name} con ID: {id} no encontrado para deshabilitar.");
                    return OperationResult.Failure($"{typeof(TEntity).Name} con ID: {id} no encontrado o error al recuperarlo.");
                }

                var entityToDisable = getResult.Data as TEntity;
                if (entityToDisable == null)
                {
                    // Esto no debería ocurrir si TEntity es AuditEntity, pero es una buena verificación de seguridad.
                    _logger.LogError($"Error interno: Objeto recuperado para {typeof(TEntity).Name} {id} no es del tipo correcto.");
                    return OperationResult.Failure("Error interno al procesar la entidad.");
                }

                if (entityToDisable.IsDeleted == true)
                {
                    _logger.LogInformation($"El {typeof(TEntity).Name} con ID: {id} ya está deshabilitado (marcado como borrado).");
                    return OperationResult.Success($"El {typeof(TEntity).Name} ya está deshabilitado.", entityToDisable);
                }

                entityToDisable.IsDeleted = true;

                /* if (entityToDisable is IAuditable auditableEntity)
                   {
                     auditableEntity.UpdatedDate = DateTime.UtcNow;
                   }
                */
                var updateResult = await _repository.Updateasync(entityToDisable);

                if (!updateResult.IsSuccess)
                {
                    _logger.LogError($"Error al deshabilitar {typeof(TEntity).Name} con ID: {id}: {updateResult.Message}");
                    return OperationResult.Failure($"No se pudo deshabilitar el {typeof(TEntity).Name}: {updateResult.Message}");
                }

                _logger.LogInformation($"{typeof(TEntity).Name} con ID: {id} deshabilitado.");
                return OperationResult.Success($"{typeof(TEntity).Name} deshabilitado exitosamente.", updateResult.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inesperado al deshabilitar {typeof(TEntity).Name} con ID: {id}: {ex.Message}");
                return OperationResult.Failure($"Ocurrió un error inesperado al deshabilitar el {typeof(TEntity).Name}: {ex.Message}");
            }
        }
    }
}
