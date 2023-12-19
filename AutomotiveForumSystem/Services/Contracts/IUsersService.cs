using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Models.DTOs;

namespace AutomotiveForumSystem.Services.Contracts
{
    public interface IUsersService
    {
        User Create(User user);

        User GetById(int id);

        User GetByUsername(string username);

        User UpdateProfileInformation(User user, UserUpdateProfileInformationDTO userDTO);

        User UpdateAccountStatus(User requestingUser, User userToUpdate, UserUpdateAccountStatusDTO userDTO);
    }
}
