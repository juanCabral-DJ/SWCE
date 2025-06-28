using SWCE.Aplicatition.Interfaces.Repositories.EnvioModule;
using SWCE.Domain.Base;
using Microsoft.Extensions.Logging;
using SWCE.Persistence.Context;
using FluentValidation;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SWCE.Persistence.Base;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SWCE.Persistence.Repositories
{
    public class EnvioRepository : RepositoryBase<EnvioBase>, IEnvioRepository
    {
        private readonly E_commerceContext _context;
        private readonly ILogger<EnvioRepository> _logger;
        private readonly IValidator<EnvioBase> _validator;

        public EnvioRepository(E_commerceContext context, ILogger<EnvioRepository> logger, IValidator<EnvioBase> validator) : base(context)
        {
            _context = context; 
            _logger = logger;
            _validator = validator;
        }
        public async Task<OperationResult> GetByIdAsync(Guid id)
        {
            OperationResult result = new OperationResult();
            try
            {
                var envio = await _context.Envios.FindAsync(id);
                if (envio == null)
                {
                    _logger.LogWarning("No se encontro un envio con ID {id}", id);
                    result = OperationResult.Failure("No se encontro envio con Id");
                }

                _logger.LogInformation("Se obtuvo el envio con ID");
                return OperationResult.Susscess("Se obtuvo el envio con id", id);
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error al obtener envio con ID");
                result = OperationResult.Failure("Ocurrio un error mientras se recuperaba el envio");
            }
            return result;
        }

        public async Task<OperationResult> GetAllAsync()
        {
            OperationResult result = new OperationResult();
            try
            {
                _logger.LogInformation("Recuperando Envios");
                var envios = await base.GetAllasync();
                result = OperationResult.Susscess("Recuperando envios", envios);
            }
            catch (Exception)
            {
                _logger.LogError("Error recuperando envios");
                result = OperationResult.Failure("Ha ocurrido un error recuperando los envios");
            }
            return result;
        }

        public async Task<OperationResult> CreateAsync(EnvioBase envio)
        {
            OperationResult result = new OperationResult();
            try
            {
                _logger.LogInformation("Agregando envio: ${@envio}", envio);

                if (envio == null)
                {
                    _logger.LogError("Inserte valores para agregar");
                    return OperationResult.Failure("La entidad envio no puede ser nulo");
                }

                var ValidationResult = await _validator.ValidateAsync(envio);
                if (!ValidationResult.IsValid)
                {
                    _logger.LogError("Validacion Fallida al realizar envios");
                    return OperationResult.Failure("Validation failed" + string.Join(", ", ValidationResult.Errors.Select(e => e.ErrorMessage)));
                }

                await base.Createasync(envio);

                _logger.LogInformation("Agregando envio: ${@envio}", envio);
                result = OperationResult.Susscess("Envio Agregado exitosamente", envio);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Un error ha ocurrido agregando el envio {ex.Message}";
                _logger.LogError("Un error ha ocurrido agregando el envio: {Message}", ex);
            }
            return result; 

        }
        public async Task<OperationResult> UpdateAsync(EnvioBase envio)
        {
            OperationResult result = new OperationResult();
            try
            {
                _logger.LogInformation("Actualizando envio: ${@envio}", envio);

                if (envio == null)
                {
                    _logger.LogError("Inserte valores para actualizar");
                    return OperationResult.Failure("La entidad envio no puede ser nulo");
                }

                await base.Updateasync(envio);

                _logger.LogInformation("Actualizando envio: ${@envio}", envio);
                result = OperationResult.Susscess("Envio actualizado exitosamente", envio);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Un error ha ocurrido actualizando el envio {ex.Message}";
                _logger.LogError("Un error ha ocurrido actualizando el envio: {Message}", ex);
            }
            return result;
        }
        public async Task<OperationResult> GetByUserId(int userId)
        {
            OperationResult result = new OperationResult();

            try
            {
                _logger.LogInformation("Recuperando Envios mediante UserId");
                var envios = await _context.Envios.Where(a => a.UsuarioId == userId).ToListAsync();

                result = OperationResult.Susscess("Recuperando envios", envios);
            }
            catch (Exception)
            {
                _logger.LogError("Error recuperando envios");
                result = OperationResult.Failure("Ha ocurrido un error recuperando los envios");
            }
            return result;
   
        }
    }
}
