using System.Threading.Tasks;

namespace LanguageHelper.DataAccessLayer.Interfaces
{
    public interface IUnitOfWork
    {
        IRoleRepository RoleRepository { get; }
        ISheetRepository SheetRepository { get; }
        IUserRepository UserRepository { get; }
        IUserLanguageRepository UserLanguageRepository { get; }
        int SaveChages();
        Task<bool> SaveAsync();
    }
}
