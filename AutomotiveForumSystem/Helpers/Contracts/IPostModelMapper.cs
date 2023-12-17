using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Models.PostDtos;

namespace AutomotiveForumSystem.Helpers.Contracts
{
    public interface IPostModelMapper
    {
        Post Map(PostModelCreate model);
        List<PostResponseDto> MapPostsToResponseDtos(IList<Post> postsToReturn);
        PostResponseDto MapPostToResponseDto(Post post);
    }
}
