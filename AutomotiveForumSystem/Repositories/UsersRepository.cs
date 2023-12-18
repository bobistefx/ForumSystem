using AutomotiveForumSystem.Data;
using AutomotiveForumSystem.Exceptions;
using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Models.DTOs;
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

        public User Create(User user)
        {
            this.context.Add(user);
            this.context.SaveChanges();

            return user;
        }

        public User GetById(int id)
        {
            return this.context.Users.FirstOrDefault(u => u.Id == id)
                ?? throw new EntityNotFoundException($"User with id {id} does not exist.");
        }

        public User GetByUsername(string username)
        {
            return this.context.Users.FirstOrDefault(u => u.UserName == username)
                ?? throw new EntityNotFoundException($"User with username {username} does not exist.");
        }

        public User Update(User user, UserUpdateDTO userDTO)
        {
            user.FirstName = userDTO.FirstName;
            user.LastName = userDTO.LastName;
            user.Email = userDTO.Email;
            user.PhoneNumber = userDTO.PhoneNumber;

            return user;
        }

        public bool UsernameExists(string username)
        {
            return this.context.Users.FirstOrDefault(u => u.UserName == username) != null;                
        }
    }
}
