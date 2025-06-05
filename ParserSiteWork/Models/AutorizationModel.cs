using System.ComponentModel.DataAnnotations;

namespace ParserSiteWork.Models
{
    public class AutorizationModel
    {
        [Display(Name = "Логин")]
        [Required(ErrorMessage ="Не указан логин пользователя")]
        public string Login { get; set; }

        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Не введен пароль пользователя")]
        public string Password { get; set; }
    }
}
