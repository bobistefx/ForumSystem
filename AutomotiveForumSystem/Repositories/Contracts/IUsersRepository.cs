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

        User GetByEmail(string email);

        List<User> GetByFirstName(string firstName);

        User Block(User user);

        User Unblock(User user);

        User SetAdmin(User user);

        User UpdateProfileInformation(User user, UserUpdateProfileInformationDTO userDTO);

        void Delete(User userToDelete);
    }
}
