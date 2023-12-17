using AutomotiveForumSystem.Models.PostDtos;

namespace AutomotiveForumSystem.Services.Contracts
{
    public interface ICategoriesService
    {
        IList<PostResponseDto> GetAll();
    }
}