using LanguageHelper.DataAccessLayer.Entities;
using LanguageHelper.DataAccessLayer.Interfaces;

namespace LanguageHelper.DataAccessLayer.Repositories
{
    public class UserLanguageRepository : Repository<UserLanguage, int>, IUserLanguageRepository
    {
        public UserLanguageRepository(LanguageHelperDbContext context) : base(context) { }
    }
}
