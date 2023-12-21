using AutomotiveForumSystem.Exceptions;
using AutomotiveForumSystem.Helpers;
using AutomotiveForumSystem.Helpers.Contracts;
using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Models.DTOs;
using AutomotiveForumSystem.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AutomotiveForumSystem.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentsAPIController : ControllerBase
    {
        private readonly IPostService postService;
        private readonly ICommentsService commentsService;
        private readonly IAuthManager authManager;
        private readonly ICommentModelMapper commentModelMapper;

        public CommentsAPIController(IPostService postService,
            ICommentsService commentsService,
            IAuthManager authManager,
            ICommentModelMapper commentModelMapper)
        {
            this.postService = postService;
            this.commentsService = commentsService;
            this.authManager = authManager;
            this.commentModelMapper = commentModelMapper;
        }

        [HttpGet("")]
        public IActionResult GetAllComments([FromQuery] CommentQueryParameters commentQueryParameters)
        {
            try
            {
                var commentsToReturn = this.commentsService.GetAllComments(commentQueryParameters);
                return Ok(this.commentModelMapper.Map(commentsToReturn));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCommentById(int id)
        {
            try
            {
                var commentToReturn = this.commentsService.GetCommentById(id);
                return Ok(this.commentModelMapper.Map(commentToReturn));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("replies/{id}")]
        public IActionResult GetAllRepliesByCommentId(int id)
        {
            try
            {
                var replies = this.commentsService.GetAllRepliesByCommentId(id);

                // TODO : to map to DTO
                return Ok(replies);
            }
            catch (Exception ex)
            {
                throw new EntityNotFoundException(ex.Message);
            }
        }

        [HttpPost("")]
        public IActionResult CreateComment([FromHeader] string credentials, [FromBody] CommentCreateDTO comment)
        {
            try
            {
                var user = this.authManager.TryGetUser(credentials);
                var createdComment = this.commentModelMapper.Map(comment);

                var post = postService.GetPostById(comment.PostID);

                this.commentsService.CreateComment(user, post, createdComment);

                return Ok(this.commentModelMapper.Map(createdComment));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateComment([FromHeader] string credentials, int id, [FromBody] Comment comment)
        {
            try
            {
                User user = this.authManager.TryGetUser(credentials);

                this.commentsService.UpdateComment(user, id, comment);

                return Ok();
            }
            catch (AuthenticationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteComment([FromHeader] string credentials, int id)
        {
            try
            {
                var user = this.authManager.TryGetUser(credentials);
                var result = this.commentsService.DeleteComment(user, id);

                return Ok("Comment deleted.");
            }
            catch (AuthenticationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (AuthorizationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}