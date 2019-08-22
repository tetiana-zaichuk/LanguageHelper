using System;

namespace LanguageHelper.Shared.Dtos
{
    public class Sheet
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SpreadsheetId { get; set; }
        public DateTime LastUsed { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
