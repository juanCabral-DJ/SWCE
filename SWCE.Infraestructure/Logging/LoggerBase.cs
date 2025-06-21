using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Infraestructure.Logging
{
    public class LoggerBase : ILoggerBase<LoggerBase>
    {
        public readonly ILogger<LoggerBase> _Logger;
        public void LogError(string mensaje, Exception ex)
        {
            _Logger.LogError(mensaje, ex);
        }
        public void LogError(string mensaje)
        {
            _Logger.LogError(mensaje);
        }
        public void LogInformation(string mensaje, Object e)
        {
            _Logger.LogInformation(mensaje, e);
        }
    }
}
