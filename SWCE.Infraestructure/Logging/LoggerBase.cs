using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Infraestructure.Logging
{
    public class LoggerBase<T> : ILoggerBase<T>
    {
        private readonly ILogger<T> _logger;

        public LoggerBase(ILogger<T> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void LogError(string mensaje, Exception ex)
        {
            _logger.LogError(ex, mensaje); // La excepción va primero en el método de extensión
        }

        public void LogError(string mensaje)
        {
            _logger.LogError(mensaje);
        }

        public void LogInformation(string mensaje, params object[] args)
        {
            _logger.LogInformation(mensaje, args);
        }

        public void LogInformation(string mensaje)
        {
            _logger.LogInformation(mensaje);
        }
    }
}