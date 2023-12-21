using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Exceptions;
using AutomotiveForumSystem.Models.DTOs;
using AutomotiveForumSystem.Services.Contracts;
using AutomotiveForumSystem.Repositories.Contracts;
using AutomotiveForumSystem.Models.Contracts;

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
            this.EnsureEmailIsUnique(user.Email);
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

        public User UpdateProfileInformation(User user, UserUpdateProfileInformationDTO userDTO)
        {
            if (user.IsBlocked) throw new UserBlockedException("Your account is blocked.");
            if (user.IsDeleted) throw new EntityNotFoundException("Account does not exists.");

            this.EnsureEmailIsUnique(userDTO.Email);
            return this.users.UpdateProfileInformation(user, userDTO);
        }

        public User UpdateAccountSettings(User requestingUser, User userToUpdate, UserUpdateAccountStatusDTO userDTO)
        {
            if (!requestingUser.IsAdmin) throw new AuthorizationException("You do not have admin permissions.");
            if (userToUpdate.IsDeleted) throw new EntityNotFoundException("Account does not exists.");

            if (userDTO.IsAdmin && userToUpdate.IsAdmin)
            {
                throw new AdminRightsAlreadyGrantedException("User has already been granted with admin rights.");
            }
            else if (!userDTO.IsAdmin && !userToUpdate.IsAdmin)
            {
                throw new AdminRightsNotCurrentlyGrantedException("Admin rights have not been currently granted to the user.");
            }
            else if (userDTO.IsBlocked && userToUpdate.IsBlocked)
            {
                throw new UserBlockedException("User is already blocked.");
            }
            else if (!userDTO.IsBlocked && !userToUpdate.IsBlocked)
            {
                throw new UserNotBlockedException("User is not blocked.");
            }

            return this.users.UpdateAccountSettings(userToUpdate, userDTO);
        }

        // isBlocked =
        
        private void EnsureUsernameIsUnique(string username)
        {
            var user = this.users.GetAll().FirstOrDefault(u => u.UserName == username);

            if (user != null)
            {
                throw new DuplicateEntityException($"Username {username} is already used.");
            }
        }

        private void EnsureEmailIsUnique(string email)
        {
            var user = this.users.GetAll().FirstOrDefault(u => u.Email == email);

            if (user != null)
            {
                throw new DuplicateEntityException($"Email {email} is already used.");
            }
        }
    }
}