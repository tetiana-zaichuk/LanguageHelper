using LanguageHelper.DataAccessLayer.Interfaces;

namespace LanguageHelper.DataAccessLayer.Entities
{
    public class UserLanguage: IEntity<int>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Language { get; set; }
        public string Level { get; set; }
    }
}