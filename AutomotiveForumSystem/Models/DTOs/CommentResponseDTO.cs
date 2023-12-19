namespace AutomotiveForumSystem.Models.CommentDTOs
{
    public class CommentResponseDTO
    {
        public string Post { get; set; }
        public string Content { get; set; }
        public String CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public IList<CommentResponseDTO> Replies { get; set; }
    }
}