using AutomotiveForumSystem.Exceptions;
using AutomotiveForumSystem.Helpers;
using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Models.PostDtos;
using AutomotiveForumSystem.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace AutomotiveForumSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService postService;
        private readonly PostModelMapper postModelMapper;
        private readonly AuthManager authManager;

        public PostsController(IPostService postService, PostModelMapper postModelMapper, AuthManager authManager)
        {
            this.postService = postService;
            this.postModelMapper = postModelMapper;
            this.authManager = authManager;
        }

        // GET: api/posts
        [HttpGet]
        public ActionResult<IEnumerable<Post>> Get()
        {
            return Ok(postService.GetAll());
        }

        // GET: api/posts/categoryName
        [HttpGet]
        public ActionResult<Post> Get([FromQuery] string categoryName)
        {
            try
            {
                var posts = postService.GetByCategory(categoryName);
                return Ok(posts);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: api/posts/userId
        [HttpGet]
        public ActionResult<IEnumerable<Post>> Get([FromQuery] int userId, [FromQuery] PostQueryParameters postQueryParameters)
        {
            try
            {
                var posts = postService.GetByUser(userId, postQueryParameters);
                return Ok(posts);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/posts
        [HttpPost]
        public IActionResult CreatePost([FromHeader]string credentials, [FromBody] PostModelCreate model)
        {
            var currentUser = this.authManager.TryGetUser(credentials);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdPost = postService.Create(postModelMapper.Map(model), currentUser);
            return StatusCode(StatusCodes.Status201Created, createdPost);
        }

        // PUT: api/posts/{id}
        [HttpPut("{id}")]
        public IActionResult UpdatePost([FromHeader] string credentials, int id, [FromBody] Post post)
        {
            try
            {
                var currentUser = this.authManager.TryGetUser(credentials);
                var postToUpdate = this.postService.Update(id, post, currentUser);
                return Ok(postToUpdate);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (AuthorizationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/posts/id
        [HttpDelete("{id}")]
        public IActionResult DeletePost([FromHeader] string credentials, int id)
        {
            try
            {
                var currentUser = this.authManager.TryGetUser(credentials);
                this.postService.Delete(id, currentUser);
                return Ok();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (AuthorizationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
