using AutomotiveForumSystem.Models.Contracts;
using System.ComponentModel.DataAnnotations;

namespace AutomotiveForumSystem.Models
{
    public class Category : ICategory
    {
        [Key]
        public int Id { get; set; }

        [Required, MinLength(4), MaxLength(32)]
        public string Name { get; set; }

        public IList<Post> Posts { get; set;} = new List<Post>();

        [Required]
        public bool IsDeleted { get; set; }
    }
}