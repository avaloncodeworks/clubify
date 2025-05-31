using System.ComponentModel.DataAnnotations;

namespace Clubify.Data.Models
{
    public class Club
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
