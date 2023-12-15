﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AutomotiveForumSystem.Models.Contracts
{
    public interface IUser
    {
        int Id { get; set; }
        string UserName { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        string? PhoneNumber { get; set; }
        int RoleID { get; set; }
        Role Role { get; set; }
        IList<Post> Posts { get; set; }
        IList<Comment> Comments { get; set; }
        bool IsBlocked { get; set; }
        bool IsDeleted { get; set; }
    }
}