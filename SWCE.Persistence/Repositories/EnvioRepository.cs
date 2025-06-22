using SWCE.Aplicatition.Interfaces.Repositories.EnvioModule;
using SWCE.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SWCE.Persistence.Context;

namespace SWCE.Persistence.Repositories
{
    public class EnvioRepository : IEnvioRepository
    {
        private readonly E_commerceContext _context;
        private readonly ILogger<EnvioRepository> _logger;
        //private readonly IValidator<EnvioBase> _validator;

        public EnvioRepository(E_commerceContext context, ILogger<EnvioRepository> logger)
        {
            _context = context; 
            _logger = logger;
            //_validator = validator;
        }

        public Task CrearAsync(EnvioBase envio)
        {
            var validationResult = envio.Validar();
        }
        public Task ActualizarAsync(EnvioBase envio)
        {
            throw new NotImplementedException();
        }

        public Task EliminarAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<EnvioBase> ObtenerPorIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<EnvioBase>> ObtenerTodosAsync()
        {
            throw new NotImplementedException();
        }
    }
}
