using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TwitterAPI.Models;

namespace TwitterAPI.DTOS
{
    public class TweetDTO
    {
        public int Id { get; set; }
        public string AuthorId { get; set; } // Assuming AuthorId is the foreign key referencing the User
        public string Body { get; set; }

        public int Likes { get; set; } = 0;

        public string ImageUrl { get; set; }
        public bool Status { get; set; } = true;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
