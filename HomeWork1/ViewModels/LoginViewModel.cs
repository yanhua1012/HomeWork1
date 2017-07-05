using System.ComponentModel.DataAnnotations;

namespace HomeWork1.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "帳號")]
        public string Account { get; set; }

        [Required]
        [Display(Name = "密碼")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "記住我")]
        public bool RememberMe { get; set; }
    }
}