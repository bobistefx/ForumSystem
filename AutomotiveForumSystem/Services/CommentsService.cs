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
            return this.commentsRepository.CreateComment(comment);
        }

        public IList<Comment> GetAllComments(CommentQueryParameters commentQueryParameters)
        {
            return this.commentsRepository.GetAllComments(commentQueryParameters);
        }

        public Comment GetCommentById(int id)
        {
            return this.commentsRepository.GetCommentById(id);
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

            return this.commentsRepository.UpdateComment(id, comment);
        }

        public bool DeleteComment(User user, int id)
        {
            // TODO : check if we have to get the comment from the repo first
            // so we check if the user is the creator !?
            var commentToDelete = this.commentsRepository.GetCommentById(id);
            
            if (commentToDelete.IsDeleted)
            {
                throw new EntityNotFoundException($"Comment with id {id} not found");
            }

            if (commentToDelete.UserID != user.Id && user.IsAdmin)
            {
                throw new EntityNotFoundException($"Comment with id {id} not found");
            }

            this.commentsRepository.DeleteComment(commentToDelete);

            return true;
        }
    }
}
