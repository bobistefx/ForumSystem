using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Models.DTOs;

namespace AutomotiveForumSystem.Repositories.Contracts
{
    public interface ICommentsRepository
    {
        IList<Comment> GetAllComments(CommentQueryParameters commentQueryParameters);
        Comment GetCommentById(int id);
        IList<Comment> GetAllRepliesByCommentId(int id);
        Comment CreateComment(Comment comment);
        Comment UpdateComment(int id, Comment comment);
        bool DeleteComment(int id);
    }
}
