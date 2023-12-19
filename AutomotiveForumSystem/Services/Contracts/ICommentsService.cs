using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Models.CommentDTOs;
using AutomotiveForumSystem.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AutomotiveForumSystem.Services.Contracts
{
    public interface ICommentsService
    {
        IList<Comment> GetAllComments(CommentQueryParameters commentQueryParameters);
        Comment GetCommentById(int id);
        IList<Comment> GetAllRepliesByCommentId(int id);
        Comment CreateComment(User user, Post post, Comment comment);
        Comment UpdateComment(User user, int id, Comment comment);
        bool DeleteComment(User user, int id);
    }
}