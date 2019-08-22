using System;
using LanguageHelper.DataAccessLayer.Interfaces;

namespace LanguageHelper.DataAccessLayer.Entities
{
    public class Sheet: IEntity<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SpreadsheetId { get; set; }
        public DateTime LastUsed { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
