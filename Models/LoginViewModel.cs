using System.ComponentModel.DataAnnotations;

namespace GigBookin.Models
{
    public class LoginViewModel
    {
        
        [Required]
        public string UserName { get; set; } = null;

        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    
    }
}
