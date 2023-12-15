using System.ComponentModel.DataAnnotations;

namespace AutomotiveForumSystem.Models.Contracts
{
    public interface ICategory
    {
        int Id { get; set; }
        string Name { get; set; }
        IList<Post> Posts { get; set; }
        bool IsDeleted { get; set; }
    }
}