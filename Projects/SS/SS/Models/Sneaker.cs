using System.ComponentModel.DataAnnotations;

namespace SS.Models
{
    public class Sneaker
    {
        public int Id { get; set; }
        [Required (ErrorMessage = "Name field is empty")]
        public string Name { get; set; }
        public Brand Brand { get; set; }
        public int? BrandId { get; set; }
        [Required(ErrorMessage = "Color field is empty")]
        public string Color { get; set; }
        [Required(ErrorMessage = "Price field is empty")]
        [Range(0,double.MaxValue,ErrorMessage ="Price cannot be less than 0")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Count field is empty")]
        [Range (0,100,ErrorMessage="Quantity sneakers on the storage cannot be more than 100 or less 0")]
        public int Count { get; set; }
        [Required(ErrorMessage = "Picture path  is empty")]
        public string PicturePath { get; set; }
    }
}
