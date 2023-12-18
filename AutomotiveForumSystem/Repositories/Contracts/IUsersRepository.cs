using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Models.DTOs;

namespace AutomotiveForumSystem.Repositories.Contracts
{
    public interface IUsersRepository
    {
        User Create(User user);

        User GetById(int id);

        User GetByUsername(string username);

        User Update(User user, UserUpdateDTO userDTO);

        bool UsernameExists(string username);
    }
}
