using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Repository
{
    public interface IRepositoryBase<T> where T : class
    {
        List<T> GetAll();
        bool Create(T entity);
        bool Update(T entity);
        bool Delete(T entity);

    }
}
