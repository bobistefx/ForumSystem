using AutomotiveForumSystem.Models;

namespace AutomotiveForumSystem.Services.Contracts
{
    public interface IPostService
    {
        IList<PostResponseDto> GetAll();
        IList<PostResponseDto> GetByCategory(string categoryName);
        IList<PostResponseDto> GetByUser(int userId, PostQueryParameters postQueryParameters);
        Post GetById(int id);
        Post Create(Post post, User currentUser);
        Post Update(int id, Post post, User currentUser);
        void Delete(int id, User currentUser);
    }
}
