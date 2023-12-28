namespace AutomotiveForumSystem.Models.DTOs
{
    public class CommentQueryParameters
    {
        public string? Content { get; set; }
        public string? User { get; set; }
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; }
    }
}
