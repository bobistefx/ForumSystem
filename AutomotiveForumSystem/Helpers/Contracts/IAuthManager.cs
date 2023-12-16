using AutomotiveForumSystem.Models.Contracts;

namespace AutomotiveForumSystem.Helpers.Contracts
{
    public interface IAuthManager
    {
        IUser TryGetUser(string credentials);
    }
}
