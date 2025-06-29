using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Infraestructure.Logging
{
    public class LoggerBase<T> : ILoggerBase<T> where T : class
    {
        public readonly ILogger<T> _Logger;

        public LoggerBase(ILogger<T> logger)
        {
            _Logger = logger;
        }
        public void LogError(string mensaje, Exception ex)
        {
            _Logger.LogError(mensaje, ex);
        }

        public void LogError(string mensaje)
        {
            _Logger.LogError(mensaje);
        }

        public void LogInformation(string mensaje)
        {
            _Logger.LogInformation(mensaje);
        }

        public void LogInformation(string mensaje, object entity)
        {
            _Logger.LogInformation(mensaje, entity);
        }
    }
}
