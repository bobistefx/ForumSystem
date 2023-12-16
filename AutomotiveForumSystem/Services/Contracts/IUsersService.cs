using AutomotiveForumSystem.Models.Contracts;

namespace AutomotiveForumSystem.Services.Contracts
{
    public interface IUsersService
    {
        IUser GetById(int id);

        IUser GetByUsername(string username);

        IUser Update();
    }
}
