namespace LanguageHelper.Shared.Dtos
{
    public class UserLanguageDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Language { get; set; }
        public string Level { get; set; }
    }
}