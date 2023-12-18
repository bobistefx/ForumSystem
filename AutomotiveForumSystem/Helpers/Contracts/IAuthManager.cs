using AutomotiveForumSystem.Models;

namespace AutomotiveForumSystem.Helpers.Contracts
{
    public interface IAuthManager
    {
        User TryGetUser(string credentials);
    }
}
