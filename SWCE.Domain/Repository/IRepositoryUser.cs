using SWCE.Domain.Entities.Configuration.User_Perfil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Repository
{
    public interface IRepositoryUser : IRepositoryBase<User>
    {
        public List<User> GetById(int id);
        public List<User> GetByEmail(string email);
        
    }
}
