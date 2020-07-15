using System.ComponentModel.DataAnnotations;

namespace SS.Models
{
    public class Brand
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name field is empty")]
        public string Name { get; set; }
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
