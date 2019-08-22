using System;
using System.Collections.Generic;

namespace LanguageHelper.Shared.Dtos
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Photo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastActive { get; set; }
        public IList<Sheet> Sheets { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}