using System.ComponentModel.DataAnnotations;

namespace AutomotiveForumSystem.Models.PostDtos
{
    public class PostResponseDto
    {
        public string CategoryName { get; set; }

        public string Title { get; set; } = "";

        public string Content { get; set; } = "";

        public string CreateDate { get; set; }

        public IList<Comment> Comments { get; set; }

        public int Likes { get; set; }
    }
}
