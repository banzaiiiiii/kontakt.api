using System.ComponentModel.DataAnnotations;

namespace Kontakt.API.Models
{
    public class Contact
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string ContactNumber { get; set; }
        [Required]
        [MaxLength(500)]
        public string Note { get; set; }
    }
}
