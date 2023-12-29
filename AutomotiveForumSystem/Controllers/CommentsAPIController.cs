using AutomotiveForumSystem.Exceptions;
using AutomotiveForumSystem.Helpers;
using AutomotiveForumSystem.Helpers.Contracts;
using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Models.CommentDTOs;
using AutomotiveForumSystem.Models.DTOs;
using AutomotiveForumSystem.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
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

                return Ok(this.commentModelMapper.Map(replies));
            }
            catch (Exception ex)
            {
                throw new EntityNotFoundException(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("")]
        public IActionResult CreateComment([FromHeader(Name = "Authorization")] string auth, [FromBody] CommentCreateDTO comment)
        {
            try
            {
                var token = auth.Replace("Bearer ", string.Empty);

                var user = this.authManager.TryGetUserFromToken(token);
                var createdComment = this.commentModelMapper.Map(comment);

                var post = this.postService.GetPostById(comment.PostID);

                this.commentsService.CreateComment(user, post, createdComment, comment.CommentID);

                return Ok(this.commentModelMapper.Map(createdComment));
            }                        
            catch (UserBlockedException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (DuplicateEntityException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdateComment([FromHeader(Name = "Authorization")] string auth, int id, [FromBody] CommentRequestDTO content)
        {
            try
            {
                Console.WriteLine(content);

                var token = auth.Replace("Bearer ", string.Empty);

                var user = this.authManager.TryGetUserFromToken(token);

                var updatedComment = this.commentsService.UpdateComment(user, id, content.Content);

                return Ok(commentModelMapper.Map(updatedComment));
            }
            catch (AuthenticationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteComment([FromHeader(Name = "Authorization")] string auth, int id)
        {
            try
            {
                var user = this.authManager.TryGetUserFromToken(auth);
                var result = this.commentsService.DeleteComment(user, id);

                return Ok("Comment deleted.");
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