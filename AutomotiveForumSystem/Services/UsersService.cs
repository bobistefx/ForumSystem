using AutomotiveForumSystem.Exceptions;
using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Models.DTOs;
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
        
        public User Create(User user)
        {
            this.EnsureUsernameIsUnique(user.UserName);
            return this.users.Create(user);
        }

        public User GetById(int id)
        {
            return this.users.GetById(id);
        }

        public User GetByUsername(string username)
        {
            return this.users.GetByUsername(username);
        }

        public User Update(User user, UserUpdateDTO userDTO)
        {
            return this.users.Update(user, userDTO);
        }

        private void EnsureUsernameIsUnique(string username)
        {
            if (this.users.UsernameExists(username))
            {
                throw new DuplicateEntityException("Username already exists.");
            }
        }
    }
}