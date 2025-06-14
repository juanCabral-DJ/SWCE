using SWCE.Domain.Entities.Configuration.User_Perfil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Repository
{
    public interface IRepositoryAddress : IRepositoryBase<Address>
    {
        public List<Address> GetbyId(int id);
        public List<Address> GetbyPredeterminada(string predeterminada);
        public List<Address> GetbyUserId(int userId);
    }
}
