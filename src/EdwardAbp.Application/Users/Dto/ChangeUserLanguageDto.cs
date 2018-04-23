using System.ComponentModel.DataAnnotations;

namespace EdwardAbp.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}