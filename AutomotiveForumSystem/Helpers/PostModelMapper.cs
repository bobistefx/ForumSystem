using AutomotiveForumSystem.Data;
using AutomotiveForumSystem.Models;

namespace AutomotiveForumSystem.Helpers
{
    public class PostModelMapper
    {
        private readonly ApplicationContext applicationContext;

        public PostModelMapper(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public Post Map(CreatePostModel model)
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
