using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Infraestructure.Logging
{
    public interface ILoggerBase<TEntity> where TEntity : class
    {
        void LogInformation(string mensaje, Object entity);
        void LogError(string mensaje, Exception ex);
        void LogError(string mensaje);
    }
}
