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
                var commentsToReturn = commentsService.GetAllComments(commentQueryParameters);
                return Ok(commentModelMapper.Map(commentsToReturn));
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
                var commentToReturn = commentsService.GetCommentById(id);
                return Ok(commentModelMapper.Map(commentToReturn));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("replies/{id}")]
        public IActionResult GetAllRepliesByCommentId(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost("")]
        public IActionResult CreateComment([FromHeader] string credentials, [FromBody] CommentCreateDTO comment)
        {
            try
            {
                var user = authManager.TryGetUser(credentials);
                var createdComment = commentModelMapper.Map(comment);

                var post = postService.GetPostById(comment.PostID);

                commentsService.CreateComment(user, post, createdComment);

                return Ok(commentModelMapper.Map(createdComment));
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
                User user = authManager.TryGetUser(credentials);

                commentsService.UpdateComment(user, id, comment);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteComment([FromHeader] string credentials, int id)
        {
            throw new NotImplementedException();
        }
    }
}