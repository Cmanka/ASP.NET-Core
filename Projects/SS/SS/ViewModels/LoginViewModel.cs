using System.ComponentModel.DataAnnotations;

namespace SS.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "User Name field is empty")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password field is empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
