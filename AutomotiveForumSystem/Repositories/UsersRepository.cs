using AutomotiveForumSystem.Data;
using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Exceptions;
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
            this.context.Users.Add(user);
            this.context.SaveChanges();

            return user;
        }

        public List<User> GetAll()
        {
            return this.context.Users.ToList();
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

        public User UpdateProfileInformation(User user, UserUpdateProfileInformationDTO userDTO)
        {
            user.FirstName = userDTO.FirstName;
            user.LastName = userDTO.LastName;
            user.Email = userDTO.Email;
            user.PhoneNumber = userDTO.PhoneNumber;

            return user;
        }

        public User UpdateAccountSettings(User user, UserUpdateAccountStatusDTO userDTO)
        {
            user.IsBlocked = userDTO.IsBlocked;
            user.IsAdmin = userDTO.IsAdmin;

            return user;
        }
    }
}
