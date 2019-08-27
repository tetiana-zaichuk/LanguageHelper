using LanguageHelper.DataAccessLayer.Entities;
using LanguageHelper.DataAccessLayer.Interfaces;

namespace LanguageHelper.DataAccessLayer.Repositories
{
    public class SheetRepository : Repository<Sheet, int>, ISheetRepository
    {
        public SheetRepository(LanguageHelperDbContext context) : base(context) { }
    }
}
