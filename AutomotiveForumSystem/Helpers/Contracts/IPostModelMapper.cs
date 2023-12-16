using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Models.PostDtos;

namespace AutomotiveForumSystem.Helpers.Contracts
{
    public interface IPostModelMapper
    {
        public Post Map(PostModelCreate model);
    }
}
