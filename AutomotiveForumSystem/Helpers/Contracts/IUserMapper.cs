using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Models.DTOs;

namespace AutomotiveForumSystem.Helpers.Contracts
{
    public interface IUserMapper
    {
        User Map(UserCreateDTO user);

        UserResponseDTO Map(User user);

        List<UserResponseDTO> Map(List<User> users);
    }
}
