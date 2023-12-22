using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Models.DTOs;
using AutomotiveForumSystem.Models.PostDtos;

namespace AutomotiveForumSystem.Services.Contracts
{
    public interface ICategoriesService
    {
        IList<Category> GetAll(CategoryQueryParameters categoryQueryParameters);
        Category GetCategoryById(int id);
        Category CreateCategory(string name);
        Category UpdateCategory(int id, Category category);
        bool DeleteCategory(int id);
    }
}