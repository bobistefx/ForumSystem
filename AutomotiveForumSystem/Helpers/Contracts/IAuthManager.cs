using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Models.Contracts;

namespace AutomotiveForumSystem.Helpers.Contracts
{
    public interface IAuthManager
    {
        User TryGetUser(string credentials);
    }
}
