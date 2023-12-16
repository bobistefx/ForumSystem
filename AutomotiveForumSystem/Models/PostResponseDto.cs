using System.ComponentModel.DataAnnotations;

namespace AutomotiveForumSystem.Models
{
    public class PostResponseDto
    {
        public string Category { get; set; }

        public string Title { get; set; } = "";

        public string Content { get; set; } = "";

        public DateTime CreateDate { get; set; }

        public IList<Comment> Comments { get; set; }

        public int Likes { get; set; }
    }
}
