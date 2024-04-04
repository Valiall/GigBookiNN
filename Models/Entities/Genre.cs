using System.ComponentModel.DataAnnotations;

namespace GigBookin.Models.Entities
{
    public class Genre
    {
        public Genre()
        {
            Id = Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }
       
        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        public ICollection<Performer> Performers { get; set; }

    }
}
