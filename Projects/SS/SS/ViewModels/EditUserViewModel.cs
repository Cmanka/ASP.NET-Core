using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SS.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "User Name field is empty")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public string Role { get; set; }
        public List<IdentityRole> RolesList { get; set; }
    }
}
