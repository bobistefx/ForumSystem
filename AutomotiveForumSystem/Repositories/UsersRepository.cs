using AutomotiveForumSystem.Data;
using AutomotiveForumSystem.Exceptions;
using AutomotiveForumSystem.Models.Contracts;
using AutomotiveForumSystem.Repositories.Contracts;

namespace AutomotiveForumSystem.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationContext context;

        public UsersRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public IUser Create(IUser user)
        {
            this.context.Add(user);
            this.context.SaveChanges();

            return user;
        }

        public IUser GetById(int id)
        {
            return this.context.Users.FirstOrDefault(u => u.Id == id)
                ?? throw new EntityNotFoundException($"User with id {id} does not exist.");
        }

        public IUser GetByUsername(string username)
        {
            return this.context.Users.FirstOrDefault(u => u.UserName == username)
                ?? throw new EntityNotFoundException($"User with username {username} does not exist.");
        }

        public IUser Update()
        {
            throw new NotImplementedException();
        }
    }
}
