using Microsoft.AspNetCore.Http;
using SS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SS.ViewModels
{
    public class SneakerAddViewModel
    {
        public int SneakerId { get; set; }
        [Required(ErrorMessage = "Name field is empty")]
        public string Name { get; set; }
        public virtual Brand Brand { get; set; }
        public int? BrandId { get; set; }
        [Required(ErrorMessage = "Color field is empty")]
        public string Color { get; set; }
        [Required(ErrorMessage = "Price field is empty")]
        [Range(0, double.MaxValue, ErrorMessage = "Price cannot be less than 0")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Count field is empty")]
        [Range(0, 100, ErrorMessage = "Quantity sneakers on the storage cannot be more than 100 or less 0")]
        public int Count { get; set; }
        [Required(ErrorMessage = "Picture path  is empty")]
        public IFormFile Picture { get; set; }

        public string PicturePath { get; set; }
        public List<Brand> BrandsList { get; set; }
        public IEnumerable<Sneaker> Sneakers { get; set; }
    }
}
