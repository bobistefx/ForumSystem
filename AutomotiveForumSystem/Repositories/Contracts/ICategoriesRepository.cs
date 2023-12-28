using AutomotiveForumSystem.Models;

namespace AutomotiveForumSystem.Repositories.Contracts
{
    public interface ICategoriesRepository
    {
        IList<Category> GetAll();
        Category GetCategoryById(int id);
        Category CreateCategory(string name);
        Category UpdateCategory(int id, Category category);
        bool DeleteCategory(int id);
        bool DoesCategoryExist(string name);
    }
}
