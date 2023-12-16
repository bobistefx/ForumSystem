using AutomotiveForumSystem.Helpers.Contracts;
using AutomotiveForumSystem.Models.Contracts;
using AutomotiveForumSystem.Services.Contracts;

namespace AutomotiveForumSystem.Helpers
{
    public class AuthManager : IAuthManager
    {
        private readonly IUsersService usersService;

        public AuthManager(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IUser TryGetUser(string credentials)
        {
            var credentialsArgs = credentials.Split(":");

            var username = credentialsArgs[0];
            var password = credentialsArgs[1];

            var user = this.usersService.GetByUsername(username);

            if (user.Password != password)
            {
                throw new 
            }
        }
    }
}
