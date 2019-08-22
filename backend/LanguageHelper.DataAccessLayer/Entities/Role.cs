using LanguageHelper.DataAccessLayer.Interfaces;

namespace LanguageHelper.DataAccessLayer.Entities
{
    public class Role: IEntity<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
