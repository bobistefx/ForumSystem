using AutomotiveForumSystem.Exceptions;
using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Repositories.Contracts;
using AutomotiveForumSystem.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomotiveForumSystem.Tests.CategoryServiceTests
{
    [TestClass]
    public class CategoryServiceTests
    {
        private Mock<ICategoriesRepository> categoriesRepositoryMock;
        private CategoriesService categoriesService;

        public CategoryServiceTests()
        {
            categoriesRepositoryMock = new Mock<ICategoriesRepository>();
            categoriesService = new CategoriesService(categoriesRepositoryMock.Object);
        }

        [TestMethod]
        public void GetAll_ReturnsCategoriesFromRepository()
        {
            // Arrange
            var expectedCategories = new List<Category>
        {
            new Category { Id = 1, Name = "Category 1" },
            new Category { Id = 2, Name = "Category 2" },
            // Add more categories as needed
        };

            // Mock repository behavior
            categoriesRepositoryMock.Setup(repo => repo.GetAll()).Returns(expectedCategories);

            // Act
            var result = categoriesService.GetAll().ToList();

            // Assert
            // Verify that the repository method was called with the correct parameters
            categoriesRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);

            // Ensure that the returned categories match the expected categories
            CollectionAssert.AreEqual(expectedCategories, result);
        }

        [TestMethod]
        public void GetCategoryById_ReturnsCategoryFromRepository()
        {
            // Arrange
            var categoryId = 1;
            var expectedCategory = new Category { Id = categoryId, Name = "SampleCategory" };

            // Mock repository behavior
            categoriesRepositoryMock.Setup(repo => repo.GetCategoryById(categoryId)).Returns(expectedCategory);

            // Act
            var result = categoriesService.GetCategoryById(categoryId);

            // Assert
            // Verify that the repository method was called with the correct parameters
            categoriesRepositoryMock.Verify(repo => repo.GetCategoryById(categoryId), Times.Once);

            // Ensure that the returned category matches the expected category
            Assert.AreEqual(expectedCategory, result);
        }
        [TestMethod]
        public void CreateCategory_WhenCategoryNameIsUnique_CreatesCategory()
        {
            // Arrange
            var categoryName = "NewCategory";
            var expectedCategory = new Category { Id = 1, Name = categoryName };

            // Mock repository behavior
            categoriesRepositoryMock.Setup(repo => repo.CreateCategory(categoryName)).Returns(expectedCategory);

            // Act
            var result = categoriesService.CreateCategory(categoryName);

            // Assert
            // Verify that the repository method was called with the correct parameters
            categoriesRepositoryMock.Verify(repo => repo.CreateCategory(categoryName), Times.Once);

            // Ensure that the returned category matches the expected category
            Assert.AreEqual(expectedCategory, result);
        }

        [TestMethod]
        public void CreateCategory_WhenCategoryNameIsNotUnique_ThrowsException()
        {
            // Arrange
            var categoryName = "ExistingCategory";

            // Mock EnsureCategoryUniqueName to throw an exception when called
            categoriesRepositoryMock.Setup(repo => repo.CreateCategory(It.IsAny<string>()))
                .Throws(new DuplicateEntityException("Category name is not unique."));

            // Act
            // Assert
            Assert.ThrowsException<DuplicateEntityException>(() => categoriesService.CreateCategory(categoryName));

            // Verify that the repository method was called with the correct parameters
            categoriesRepositoryMock.Verify(repo => repo.CreateCategory(categoryName), Times.Once);
        }

        [TestMethod]
        public void UpdateCategory_WhenCategoryNameIsUnique_UpdatesCategory()
        {
            // Arrange
            var categoryId = 1;
            var updatedCategory = new Category { Id = categoryId, Name = "UpdatedCategory" };

            // Mock repository behavior
            categoriesRepositoryMock.Setup(repo => repo.UpdateCategory(categoryId, updatedCategory)).Returns(updatedCategory);

            // Act
            var result = categoriesService.UpdateCategory(categoryId, updatedCategory);

            // Assert
            // Verify that the repository method was called with the correct parameters
            categoriesRepositoryMock.Verify(repo => repo.UpdateCategory(categoryId, updatedCategory), Times.Once);

            // Ensure that the returned category matches the expected category
            Assert.AreEqual(updatedCategory, result);
        }

        [TestMethod]
        public void UpdateCategory_WhenCategoryNameIsNotUnique_ThrowsException()
        {
            // Arrange
            var categoryId = 1;
            var updatedCategory = new Category { Id = categoryId, Name = "UpdatedCategory" };

            // Mock EnsureCategoryUniqueName to throw an exception when called
            categoriesRepositoryMock.Setup(repo => repo.UpdateCategory(It.IsAny<int>(), It.IsAny<Category>()))
                .Throws(new DuplicateEntityException("Category name is not unique."));

            // Act
            // Assert
            Assert.ThrowsException<DuplicateEntityException>(() => categoriesService.UpdateCategory(categoryId, updatedCategory));

            // Verify that the repository method was called with the correct parameters
            categoriesRepositoryMock.Verify(repo => repo.UpdateCategory(categoryId, updatedCategory), Times.Once);
        }

        [TestMethod]
        public void DeleteCategory_ReturnsTrue()
        {
            // Arrange
            var categoryId = 1;

            // Mock repository behavior
            categoriesRepositoryMock.Setup(repo => repo.DeleteCategory(categoryId));

            // Act
            var result = categoriesService.DeleteCategory(categoryId);

            // Assert
            // Verify that the repository method was called with the correct parameters
            categoriesRepositoryMock.Verify(repo => repo.DeleteCategory(categoryId), Times.Once);

            // Ensure that the method returns true
            Assert.IsTrue(result);
        }
    }
}
