using AutomotiveForumSystem.Data;
using AutomotiveForumSystem.Helpers.Contracts;
using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Models.PostDtos;

namespace AutomotiveForumSystem.Helpers
{
    public class PostModelMapper : IPostModelMapper
    {
        private readonly ApplicationContext applicationContext;

        public PostModelMapper(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public Post Map(PostModelCreate model)
        {
            var post = new Post
            {
                Title = model.Title,
                Content = model.Content,
                Category = applicationContext.Categories.FirstOrDefault(c => c.Name == model.CategoryName)
            };
            return post;
        }
    }
}
