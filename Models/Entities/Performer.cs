using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GigBookin.Models.Entities;

namespace GigBookin.Models.Entities
{
    public class Performer
    {
        public Performer()
        {
            Id= Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("Genre")]
        public Guid GenreId { get; set; }
        public Genre Genre { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Type { get; set; }
        
        [Required]
        public int Rating { get; set; }

        [Required]
        public string Experience { get; set; }
       
        [Required]
        public decimal Price { get; set; }

       public ICollection<EventPerformer> EventPerformers { get; set; }=new List<EventPerformer>(); 
       
    }
}
