using System.ComponentModel.DataAnnotations;

namespace SS.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "User Name field is empty")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email field is empty")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password field is empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Different passwords")]
        public string ConfirmPassword { get; set; }
    }
}
