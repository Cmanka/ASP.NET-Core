using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SS.Models
{
    public class Order
    {
        [BindNever]
        public int Id { get; set; }
        [Display(Name = "First Name")]
        [StringLength(30)]
        [Required(ErrorMessage = "First Name field is empty")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [StringLength(30)]
        [Required(ErrorMessage = "Last Name field is empty")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Country field is empty")]
        [StringLength(50)]
        public string Country { get; set; }
        [StringLength(20)]
        [Required(ErrorMessage = "State field is empty")]
        public string State { get; set; }
        [StringLength(30)]
        [Required(ErrorMessage = "Address field is empty")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Phone field is empty")]
        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone number")]
        public string Phone { get; set; }
        [Required]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
            ErrorMessage = "The email address is not entered in a correct format")]
        public string Email { get; set; }
        [BindNever]
        [ScaffoldColumn(false)]
        public DateTime OrderPlaced { get; set; }
    }
}
