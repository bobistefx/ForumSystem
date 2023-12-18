using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutomotiveForumSystem.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public IActionResult GetAll(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost()]
        public IActionResult CreateCategory([FromBody] Category categry)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}")]
        public IActionResult CreateCategory(int id, [FromBody] Category categry)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory([FromHeader] string credentials, int id)
        {
            throw new NotImplementedException();
        }
    }
}
