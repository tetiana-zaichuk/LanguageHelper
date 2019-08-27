using System;

namespace LanguageHelper.Shared.Dtos
{
    public class SheetDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SpreadsheetId { get; set; }
        public DateTime LastUsed { get; set; }
        public int UserId { get; set; }
        public UserDto User { get; set; }
    }
}
