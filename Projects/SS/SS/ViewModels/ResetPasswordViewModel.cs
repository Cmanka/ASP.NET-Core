using System.ComponentModel.DataAnnotations;

namespace SS.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Email field is empty")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password field is empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Different passwords")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
