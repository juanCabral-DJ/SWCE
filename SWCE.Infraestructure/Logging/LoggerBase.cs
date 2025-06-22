using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Infraestructure.Logging
{
    public class LoggerBase : ILoggerBase
    {
        public readonly ILogger<LoggerBase> _Logger;
        public void LogError(string mensaje, Exception ex)
        {
            _Logger.LogError(mensaje, ex);
        }

        public void LogInformation(string mensaje)
        {
            _Logger.LogInformation(mensaje);
        }
    }
}
