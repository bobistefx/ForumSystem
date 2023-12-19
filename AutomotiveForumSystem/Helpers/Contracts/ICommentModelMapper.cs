using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Models.CommentDTOs;
using AutomotiveForumSystem.Models.DTOs;

namespace AutomotiveForumSystem.Helpers.Contracts
{
    public interface ICommentModelMapper
    {
        CommentResponseDTO Map(Comment comment);
        IList<CommentResponseDTO> Map(IList<Comment> comments);
        Comment Map(CommentCreateDTO comment);
    }
}