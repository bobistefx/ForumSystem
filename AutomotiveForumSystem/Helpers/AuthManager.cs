using AutomotiveForumSystem.Exceptions;
using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Helpers.Contracts;
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

        public User TryGetUser(string credentials)
        {
            try
            {
                var credentialsArgs = credentials.Split(":");

                var username = credentialsArgs[0];
                var password = credentialsArgs[1];


                var user = this.usersService.GetByUsername(username);

                if (user.Password != password)
                {
                    throw new ApplicationException();
                }

                return user;
            }
            catch (ApplicationException)
            {
                throw new AuthenticationException("Invalid credentials");
            }
        }
    }
}
