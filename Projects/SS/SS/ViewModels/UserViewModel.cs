using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SS.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "User Name field is empty")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password field is empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Role { get; set; }

        public List<IdentityRole> RolesList { get; set; }

    }
}
