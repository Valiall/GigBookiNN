using System.ComponentModel.DataAnnotations;

namespace GigBookin.Models
{
    public class RemoveGenreViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

    }
}
