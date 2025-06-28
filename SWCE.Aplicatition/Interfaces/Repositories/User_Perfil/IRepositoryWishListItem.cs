using SWCE.Aplicatition.Base;
using SWCE.Domain.Base;
using SWCE.Domain.Entities.Configuration.User_Perfil;


namespace SWCE.Aplicatition.Interfaces.Repositories.User_Perfil
{
    public interface IRepositoryWishListItem : IRepositoryBase<WishListItem>
    {
        public Task<OperationResult> GetbyUserid(int userId);
        Task<OperationResult> DisableAsync(WishListItem entity);
    }
}
