using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Infraestructure.Logging
{
    public interface ILoggerBase<T>
    {
        void LogError(string mensaje, Exception ex);
        void LogError(string mensaje);
        void LogInformation(string mensaje, params object[] args); // Con parámetros
        void LogInformation(string mensaje); // Sin parámetros
    }
}
