﻿using AutomotiveForumSystem.Data;
using AutomotiveForumSystem.Models;
using AutomotiveForumSystem.Exceptions;
using AutomotiveForumSystem.Repositories.Contracts;
using AutomotiveForumSystem.Models.DTOs;

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
            return this.GetAll().FirstOrDefault(u => u.Id == id)
                ?? throw new EntityNotFoundException($"User with id {id} does not exist.");
        }

        public User GetByUsername(string username)
        {
            return this.GetAll().FirstOrDefault(u => u.UserName == username)
                ?? throw new EntityNotFoundException($"User with username {username} does not exist.");
        }

        public User GetByEmail(string email)
        {
            return this.GetAll().FirstOrDefault(u => u.Email == email)
                ?? throw new EntityNotFoundException($"User with email {email} does not exist.");
        }

        public List<User> GetByFirstName(string firstName)
        {
            return this.GetAll().FindAll(u => u.FirstName == firstName)
                ?? throw new EntityNotFoundException($"No user with first name {firstName} exists.");
        }

        public User Block(User user)
        {
            user.IsBlocked = true;
            this.context.SaveChanges();
            return user;
        }

        public User Unblock(User user)
        {
            user.IsBlocked = false;
            this.context.SaveChanges();
            return user;
        }

        public User SetAdmin(User user)
        {
            user.IsAdmin = true;
            this.context.SaveChanges();
            return user;
        }

        public User UpdateProfileInformation(User user, UserUpdateProfileInformationDTO userDTO)
        {
            user.FirstName = userDTO.FirstName;
            user.LastName = userDTO.LastName;
            user.Email = userDTO.Email;
            user.PhoneNumber = userDTO.PhoneNumber;

            this.context.SaveChanges();

            return user;
        }


        public void Delete(User userToDelete)
        {
            userToDelete.IsDeleted = true;
            this.context.SaveChanges();
        }        
    }
}
