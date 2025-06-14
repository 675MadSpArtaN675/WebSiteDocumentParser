using System.ComponentModel.DataAnnotations;

class RegistrationModel
{
    [Required(ErrorMessage = "Требуется ввод логина")]
    [Display(Name = "Логин")]
    public string? Login { get; set; }

    [Display(Name = "Пароль")]
    public string? Password { get; set; }

    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    [Display(Name = "Подтверждение пароля")]
    public string? PasswordConfrim { get; set; }
}