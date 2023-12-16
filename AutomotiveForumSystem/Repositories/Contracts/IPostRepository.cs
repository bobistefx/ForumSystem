using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Models.PostDtos;

namespace AutomotiveForumSystem.Repositories.Contracts
{
    public interface IPostRepository
    {
        IList<PostResponseDto> GetAllPosts();
        IList<PostResponseDto> GetPostByCategory(string categoryName);
        IList<PostResponseDto> GetPostsByUser(int userId, PostQueryParameters postQueryParameters);
        bool PostExist(string title);
        Post GetPostById(int id);
        Post Create(Post post, User currentUser);
        Post Update(int id, Post post);
        void Delete(Post post, User currentUser);
    }
}
