using AutomotiveForumSystem.Exceptions;
using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Models.DTOs;
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

        public IList<Category> GetAll(CategoryQueryParameters categoryQueryParameters)
        {
            return this.categoriesRepository.GetAll(categoryQueryParameters);
        }

        public Category GetCategoryById(int id)
        {
            return this.categoriesRepository.GetCategoryById(id);
        }

        public Category CreateCategory(string name)
        {
            EnsureCategoryUniqueName(name);
            
            return this.categoriesRepository.CreateCategory(name);
        }

        public Category UpdateCategory(int id, Category category)
        {
            EnsureCategoryUniqueName(category.Name);

            return this.categoriesRepository.UpdateCategory(id, category);
        }

        public bool DeleteCategory(int id)
        {
            this.categoriesRepository.DeleteCategory(id);

            return true;
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
