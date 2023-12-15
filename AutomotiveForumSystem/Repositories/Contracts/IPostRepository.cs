using AutomotiveForumSystem.Models;

namespace AutomotiveForumSystem.Repositories.Contracts
{
    public interface IPostRepository
    {
        IList<PostResponseDto> GetAll();
        IList<PostResponseDto> GetByCategory(string categoryName);
        IList<PostResponseDto> GetByUser(int userId, PostQueryParameters postQueryParameters);
        bool PostExist(string title);
        Post GetById(int id);
        Post Create(Post post, User currentUser);
        Post Update(int id, Post post);
        void Delete(Post post, User currentUser);
    }
}
