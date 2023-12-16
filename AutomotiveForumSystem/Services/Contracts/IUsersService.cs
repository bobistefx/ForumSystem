using AutomotiveForumSystem.Models.Contracts;

namespace AutomotiveForumSystem.Services.Contracts
{
    public interface IUsersService
    {
        IUser Create(IUser user);

        IUser GetById(int id);

        IUser GetByUsername(string username);

        IUser Update();
    }
}
