using AutomotiveForumSystem.Exceptions;
using AutomotiveForumSystem.Helpers.Contracts;
using AutomotiveForumSystem.Models.DTOs;
using AutomotiveForumSystem.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutomotiveForumSystem.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService categoriesService;
        private readonly ICategoryModelMapper categoryModelMapper;

        public CategoriesController(ICategoriesService categoriesService,
            ICategoryModelMapper categoryModelMapper)
        {
            this.categoriesService = categoriesService;
            this.categoryModelMapper = categoryModelMapper;
        }

        [HttpGet("")]
        public IActionResult GetAll([FromQuery] CategoryQueryParameters categoryQueryParameters)
        {
            var categoriesToReturn = this.categoriesService.GetAll(categoryQueryParameters);

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
            catch (AuthenticationException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (AuthorizationException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (UserBlockedException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("")]
        public IActionResult CreateCategory([FromBody] CategoryDTO category)
        {
            try
            {
                var newCategory = this.categoriesService.CreateCategory(category.Name);

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
                return NotFound(ex.Message);
            }
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] CategoryDTO category)
        {
            try
            {
                var newCategory = this.categoryModelMapper.Map(category);

                this.categoriesService.UpdateCategory(id, newCategory);

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

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            Console.WriteLine("Delete category end point...");

            try
            {
                var categoryDeleted = this.categoriesService.DeleteCategory(id);

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
