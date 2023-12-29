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
        Comment UpdateComment(Comment comment, string content);
        bool DeleteComment(Comment comment, bool b_SaveChanges = true);
        bool DeleteComments(List<Comment> comments);
    }
}
