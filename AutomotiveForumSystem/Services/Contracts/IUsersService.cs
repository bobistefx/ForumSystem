using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Models.DTOs;

namespace AutomotiveForumSystem.Services.Contracts
{
    public interface IUsersService
    {
        User Create(User user);

        User GetById(int id);

        User GetByUsername(string username);

        User Update(User user, UserUpdateDTO userDTO);
    }
}
