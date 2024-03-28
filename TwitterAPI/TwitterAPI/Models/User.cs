using System.ComponentModel.DataAnnotations;

namespace TwitterAPI.Models
{
    public class User
    { 
        public string Name { get; set; }

        [Key]
        public string Username { get; set; }
                
        [EmailAddress]
        public string Email { get; set; }
                
        public string PhoneNo { get; set; }
                
        public string Password { get; set; }

        public string ProfileImage { get; set; }
 
        public string Gender { get; set; }
         
        public bool Status { get; set; } = true;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
