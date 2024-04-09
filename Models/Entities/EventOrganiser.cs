using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GigBookin.Models.Entities
{
    public class EventOrganiser : IdentityUser<Guid>
    {

        public EventOrganiser()
        {
            this.EventPerformers = new List<EventPerformer>();
        }
        
        [StringLength(50)]
        

        public string? Name { get; set; }

        [StringLength(300)]
        
        public string? Description { get; set; }


        [StringLength(100)]
        [AllowNull]

        public string Experience { get; set; }

        [StringLength(30)]
        [AllowNull]

        public string FirmName { get; set; }

   
        [StringLength(30)]
        [AllowNull]

        public string FirmLocation { get; set; }

        [AllowNull]

        public decimal Balance { get; set; }

        public ICollection<EventPerformer> EventPerformers { get; set; }
      

    }
}
