using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Models.DTOs;

namespace AutomotiveForumSystem.Repositories.Contracts
{
    public interface IUsersRepository
    {
        User Create(User user);

        User GetById(int id);

        List<User> GetAll();

        User GetByUsername(string username);

        User UpdateProfileInformation(User user, UserUpdateProfileInformationDTO userDTO);

        User UpdateAccountStatus(User user, UserUpdateAccountStatusDTO userDTO);
    }
}
