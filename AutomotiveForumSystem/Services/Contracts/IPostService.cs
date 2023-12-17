using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Models.PostDtos;

namespace AutomotiveForumSystem.Services.Contracts
{
    public interface IPostService
    {
        IList<PostResponseDto> GetAllPosts();
        IList<PostResponseDto> GetAll(PostQueryParameters postQueryParameters);
        IList<PostResponseDto> GetPostsByUser(int userId, PostQueryParameters postQueryParameters);
        Post GetPostById(int id);
        Post Create(Post post, User currentUser);
        Post Update(int id, Post post, User currentUser);
        void Delete(int id, User currentUser);
    }
}
