using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Models.DTOs;
using AutomotiveForumSystem.Models.PostDtos;

namespace AutomotiveForumSystem.Services.Contracts
{
    public interface ICategoriesService
    {
        IList<Category> GetAll();
        Category GetCategoryById(int id);
        Category CreateCategory(User user, string name);
        Category UpdateCategory(User user, int id, Category category);
        bool DeleteCategory(User user, int id);
    }
}