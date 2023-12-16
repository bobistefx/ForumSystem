using AutomotiveForumSystem.Models.Contracts;
using AutomotiveForumSystem.Repositories.Contracts;
using AutomotiveForumSystem.Services.Contracts;

namespace AutomotiveForumSystem.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository users;

        public UsersService(IUsersRepository users)
        {
            this.users = users;
        }
        
        public IUser GetById(int id)
        {
            return this.users.GetById(id);
        }

        public IUser GetByUsername(string username)
        {
            return this.users.GetByUsername(username);
        }

        public IUser Update()
        {

        }
    }
}