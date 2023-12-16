using AutomotiveForumSystem.Models.Contracts;

namespace AutomotiveForumSystem.Repositories.Contracts
{
    public interface IUsersRepository
    {
        IUser Create(IUser user);

        IUser GetById(int id);

        IUser GetByUsername(string username);

        IUser Update();
    }
}
