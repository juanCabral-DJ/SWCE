using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Infraestructure.Logging
{
    public interface ILoggerBase
    {
        void LogInformation(string mensaje);
        void LogError(string mensaje, Exception ex);
    }
}
