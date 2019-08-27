using LanguageHelper.DataAccessLayer.Entities;
using LanguageHelper.DataAccessLayer.Interfaces;

namespace LanguageHelper.DataAccessLayer.Repositories
{
    public class RoleRepository: Repository<Role, int>, IRoleRepository
    {
        public RoleRepository(LanguageHelperDbContext context) : base(context) { }
    }
}
