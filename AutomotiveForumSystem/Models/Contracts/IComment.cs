using System.ComponentModel.DataAnnotations;

namespace AutomotiveForumSystem.Models.Contracts
{
    public interface IComment
    {
        int Id { get; set; }
        string Content { get; set; }
        bool IsDeleted { get; set; }
    }
}
