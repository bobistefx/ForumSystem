using AutomotiveForumSystem.Models.Contracts;
using System.ComponentModel.DataAnnotations;

namespace AutomotiveForumSystem.Models
{
    public class Role : IRole
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public IList<User> Users { get; set; } = new List<User>();

        [Required]
        public bool IsDeleted { get; set; }
    }
}