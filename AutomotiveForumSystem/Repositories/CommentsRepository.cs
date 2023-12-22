using AutomotiveForumSystem.Data;
using AutomotiveForumSystem.Exceptions;
using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Models.DTOs;
using AutomotiveForumSystem.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AutomotiveForumSystem.Repositories
{
    public class CommentsRepository : ICommentsRepository
    {
        ApplicationContext applicationContext;

        public CommentsRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public Comment CreateComment(Comment comment)
        {
            this.applicationContext.Comments.Add(comment);
            this.applicationContext.SaveChanges();
            return comment;
        }

        public IList<Comment> GetAllComments(CommentQueryParameters commentQueryParameters)
        {
            // TODO : check how to filter deleted users, comments etc.
            return this.applicationContext.Comments
                .Where(c => c.IsDeleted == false)
                .Include(c => c.Post).Where(c => c.IsDeleted == false )
                .Include(c => c.Replies).Where(r => r.IsDeleted == false)
                .Include(c => c.User)
                .ToList();
        }

        public IList<Comment> GetAllRepliesByCommentId(int id)
        {
            var targetComment = this.applicationContext.Comments.FirstOrDefault(c => c.Id == id)
                ?? throw new EntityNotFoundException($"Comment with id {id} not found.");

            return targetComment.Replies;
        }

        public Comment GetCommentById(int id)
        {
            var targetComment = this.applicationContext.Comments
                .Include(c => c.User)
                .Include(c => c.Post)
                .Include(c => c.Replies)
                .FirstOrDefault(c => c.Id == id && !c.IsDeleted)
                ?? throw new EntityNotFoundException($"Comment with id {id} not found.");

            if (targetComment.User.IsDeleted)
            {

            }

            return targetComment;
        }

        public Comment UpdateComment(int id, Comment comment)
        {
            var commentToUpdate = this.applicationContext.Comments.FirstOrDefault(p => p.Id == id)
                ?? throw new EntityNotFoundException($"Comment with id {id} not found.");

            commentToUpdate.Content = comment.Content;
            this.applicationContext.SaveChanges();

            return commentToUpdate;
        }

        public bool DeleteComment(Comment comment)
        {
            comment.IsDeleted = true;

            foreach (var item in comment.Replies)
            {
                item.IsDeleted = true;
            }

            this.applicationContext.SaveChanges();
            return true;
        }
    }
}
