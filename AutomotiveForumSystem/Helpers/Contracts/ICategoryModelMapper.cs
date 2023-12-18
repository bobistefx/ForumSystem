using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Models.DTOs;

namespace AutomotiveForumSystem.Helpers.Contracts
{
    public interface ICategoryModelMapper
    {
        Category Map(CategoryDTO category);
        CategoryDTO Map(Category category);
        IList<CategoryDTO> Map(IList<Category> category);
    }
}
