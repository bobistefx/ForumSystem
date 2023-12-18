using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Models.DTOs;
using AutomotiveForumSystem.Models.DTOS;

namespace AutomotiveForumSystem.Helpers.Contracts
{
    public interface IUserMapper
    {
        User Map(UserCreateDTO user);

        UserResponseDTO Map(User user);
    }
}
