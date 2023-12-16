using AutomotiveForumSystem.Models.Contracts;
using System.ComponentModel.DataAnnotations;

namespace AutomotiveForumSystem.Models
{
    public class User : IUser
    {
        [Key]
        public int Id { get; set; }

        [Required, MinLength(4), MaxLength(16)]
        public string UserName { get; set; }

        [Required, MinLength(4), MaxLength(32)]
        public string FirstName { get; set; }

        [Required, MinLength(4), MaxLength(32)]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string? PhoneNumber { get; set; }

        [Required]
        public int RoleID { get; set; }

        public Role Role { get; set; }

        public IList<Post> Posts { get; set; } = new List<Post>();

        public IList<Comment> Comments { get; set; } = new List<Comment>();

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
