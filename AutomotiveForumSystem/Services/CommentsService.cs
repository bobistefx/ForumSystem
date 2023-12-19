using AutomotiveForumSystem.Exceptions;
using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Models.DTOs;
using AutomotiveForumSystem.Repositories.Contracts;
using AutomotiveForumSystem.Services.Contracts;

namespace AutomotiveForumSystem.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly ICommentsRepository commentsRepository;
        private readonly IPostRepository postRepository;

        public CommentsService(ICommentsRepository commentsRepository, IPostRepository postRepository)
        {
            this.commentsRepository = commentsRepository;
            this.postRepository = postRepository;
        }

        public Comment CreateComment(User user, Post post, Comment comment)
        {
            comment.UserID = user.Id;
            comment.User = user;
            comment.PostID = post.Id;
            comment.Post = post;
            comment.CreateDate = DateTime.Now;
            return commentsRepository.CreateComment(comment);
        }

        public IList<Comment> GetAllComments(CommentQueryParameters commentQueryParameters)
        {
            return commentsRepository.GetAllComments(commentQueryParameters);
        }

        public Comment GetCommentById(int id)
        {
            return commentsRepository.GetCommentById(id);
        }

        public IList<Comment> GetAllRepliesByCommentId(int id)
        {
            throw new NotImplementedException();
        }

        public Comment UpdateComment(User user, int id, Comment comment)
        {
            if (user.Id != comment.UserID)
            {
                throw new AuthorizationException("Unauthorized");
            }

            return commentsRepository.UpdateComment(id, comment);
        }

        public bool DeleteComment(User user, int id)
        {
            throw new NotImplementedException();
        }
    }
}
