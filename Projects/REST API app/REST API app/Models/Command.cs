using System.ComponentModel.DataAnnotations;

namespace REST_API_app.Models
{
    public class Command
    {
        public int Id { get; set; }
        [Required]
        public string HowTo { get; set; }
        [Required]
        public string Line { get; set; }
        [Required]
        public string Platform { get; set; }
    }
}
