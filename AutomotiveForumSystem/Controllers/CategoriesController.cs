using AutomotiveForumSystem.Exceptions;
using AutomotiveForumSystem.Helpers.Contracts;
using AutomotiveForumSystem.Models.DTOs;
using AutomotiveForumSystem.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AutomotiveForumSystem.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService categoriesService;
        private readonly IAuthManager authManager;
        private readonly ICategoryModelMapper categoryModelMapper;

        public CategoriesController(ICategoriesService categoriesService,
            IAuthManager authManager,
            ICategoryModelMapper categoryModelMapper)
        {
            this.categoriesService = categoriesService;
            this.authManager = authManager;
            this.categoryModelMapper = categoryModelMapper;
        }

        [HttpGet("")]
        public IActionResult GetAll([FromQuery] CategoryQueryParameters categoryQueryParameters)
        {
            var categoriesToReturn = this.categoriesService.GetAll();

            return Ok(this.categoryModelMapper.Map(categoriesToReturn));
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            try
            {
                var categoryToReturn = this.categoriesService.GetCategoryById(id);
                return Ok(this.categoryModelMapper.Map(categoryToReturn));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("")]
        public IActionResult CreateCategory([FromHeader] string credentials, [FromBody] CategoryDTO category)
        {
            try
            {
                var user = this.authManager.TryGetUser(credentials);
                var newCategory = this.categoriesService.CreateCategory(user, category.Name);
                return Ok(this.categoryModelMapper.Map(newCategory));
            }
            catch (AuthenticationException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (AuthorizationException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (DuplicateEntityException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory([FromHeader] string credentials, int id, [FromBody] CategoryDTO category)
        {
            try
            {
                var user = this.authManager.TryGetUser(credentials);
                var newCategory = this.categoryModelMapper.Map(category);

                this.categoriesService.UpdateCategory(user, id, newCategory);

                return Ok(categoryModelMapper.Map(newCategory));
            }
            catch (AuthenticationException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (AuthorizationException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (DuplicateEntityException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory([FromHeader] string credentials, int id)
        {
            Console.WriteLine("Delete category end point...");

            try
            {
                var user = authManager.TryGetUser(credentials);
                var categoryDeleted = this.categoriesService.DeleteCategory(user, id);

                return Ok("Category deleted successfully.");
            }
            catch (AuthenticationException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (AuthorizationException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
