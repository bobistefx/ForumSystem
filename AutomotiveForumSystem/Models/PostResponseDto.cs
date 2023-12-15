using System.ComponentModel.DataAnnotations;

namespace AutomotiveForumSystem.Models
{
    public class PostResponseDto
    {
        public string Category { get; set; }

        public string Title { get; set; } = "";

        public string Content { get; set; } = "";

        public DateTime CreateDate { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();

        public int Likes { get; set; }
    }
}
