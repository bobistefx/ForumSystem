using AutomotiveForumSystem.Data;
using AutomotiveForumSystem.Models.DTOs;
using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Repositories.Contracts;
using AutomotiveForumSystem.Exceptions;

namespace AutomotiveForumSystem.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly ApplicationContext applicationContext;

        public CategoriesRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public IList<Category> GetAll()
        {
            return this.applicationContext.Categories.Where(x => x.IsDeleted == false).ToList();
        }

        public Category GetCategoryById(int id)
        {
            var categoryToReturn = this.applicationContext.Categories.FirstOrDefault(x => x.Id == id)
                ?? throw new EntityNotFoundException($"Category with id {id} not found.");

            return categoryToReturn;
        }

        public Category CreateCategory(string categoryName)
        {
            var newCategory = new Category()
            {
                Name = categoryName,
            };

            this.applicationContext.Categories.Add(newCategory);
            this.applicationContext.SaveChanges();

            return newCategory;
        }

        public Category UpdateCategory(int id, Category category)
        {
            var beerToUpdate = GetCategoryById(id);

            beerToUpdate.Name = category.Name;

            this.applicationContext.Update(beerToUpdate);
            this.applicationContext.SaveChanges();

            return beerToUpdate;
        }

        public bool DeleteCategory(int id)
        {
            var categoryToDelete = this.applicationContext.Categories.FirstOrDefault(c => c.Id == id)
                ?? throw new EntityNotFoundException($"Category with id{id} not found.");

            categoryToDelete.IsDeleted = true;

            this.applicationContext.Update(categoryToDelete);
            this.applicationContext.SaveChanges();

            return true;
        }

        public bool DoesCategoryExist(string name)
        {
            return this.applicationContext.Categories.Any(c => c.Name == name);
        }
    }
}
