using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Models.PostDtos;

namespace AutomotiveForumSystem.Helpers.Contracts
{
    public interface IPostModelMapper
    {
        Post Map(PostCreateDTO model);
        List<PostResponseDto> MapPostsToResponseDtos(IList<Post> postsToReturn);
        PostResponseDto MapPostToResponseDto(Post post);
    }
}
