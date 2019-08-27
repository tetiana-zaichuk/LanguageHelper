using LanguageHelper.DataAccessLayer.Entities;
using LanguageHelper.DataAccessLayer.Interfaces;

namespace LanguageHelper.DataAccessLayer.Repositories
{
    public class UserRepository : Repository<User, int>, IUserRepository
    {
        public UserRepository(LanguageHelperDbContext context) : base(context) { }
    }
}
