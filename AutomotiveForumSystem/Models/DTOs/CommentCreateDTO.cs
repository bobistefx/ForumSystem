namespace AutomotiveForumSystem.Models.DTOs
{
    public class CommentCreateDTO
    {
        public int PostID { get; set; }
        public int? CommentID { get; set; }
        public string Content { get; set; }
    }
}
