using AutomotiveForumSystem.Exceptions;
using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Repositories.Contracts;
using AutomotiveForumSystem.Services.Contracts;

namespace AutomotiveForumSystem.Services
{
    public class CategoriesService : ICategoriesService
    {
        ICategoriesRepository categoriesRepository;

        public CategoriesService(ICategoriesRepository categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public IList<Category> GetAll()
        {
            return this.categoriesRepository.GetAll();
        }

        public Category GetCategoryById(int id)
        {
            return this.categoriesRepository.GetCategoryById(id);
        }

        public Category CreateCategory(User user, string name)
        {
            EnsureUserIsAuthorized(user);
            EnsureCategoryUniqueName(name);
            
            return this.categoriesRepository.CreateCategory(name);
        }

        public Category UpdateCategory(User user, int id, Category category)
        {
            EnsureUserIsAuthorized(user);
            EnsureCategoryUniqueName(category.Name);

            return this.categoriesRepository.UpdateCategory(id, category);
        }

        public bool DeleteCategory(User user, int id)
        {
            EnsureUserIsAuthorized(user);
            this.categoriesRepository.DeleteCategory(id);

            return true;
        }

        private void EnsureUserIsAuthorized(User user)
        {
            if (!user.IsAdmin)
            {
                throw new AuthorizationException("Not authorized.");
            }
        }

        private void EnsureCategoryUniqueName(string categoryName)
        {
            if (this.categoriesRepository.DoesCategoryExist(categoryName))
            {
                throw new DuplicateEntityException($"Category with name {categoryName} already exists.");
            }
        }
    }
}
