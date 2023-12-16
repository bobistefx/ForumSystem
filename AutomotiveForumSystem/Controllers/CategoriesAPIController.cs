using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AutomotiveForumSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesAPIController : ControllerBase
    {
        private readonly ICommentsService commentsService;

        public CategoriesAPIController(ICommentsService commentsService)
        {
            this.commentsService = commentsService;
        }

        [HttpGet("")]
        public IActionResult GetAllComments()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public IActionResult GetCommentById(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("replies/{id}")]
        public IActionResult GetAllRepliesByCommentId(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost("")]
        public IActionResult CreateComment([FromBody] Comment comment)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateComment([FromHeader] string credentials, int id, [FromBody] Comment comment)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteComment([FromHeader] string credentials, int id)
        {
            throw new NotImplementedException();
        }
    }
}