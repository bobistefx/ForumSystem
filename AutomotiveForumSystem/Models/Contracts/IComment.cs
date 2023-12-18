using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutomotiveForumSystem.Models.Contracts
{
    public interface IComment
    {
        int Id { get; set; }
        string Content { get; set; }
        int UserID { get; set; }
        User User { get; set; }
        DateTime CreateDate { get; set; }
        IList<Comment> Replies { get; set; }
        bool IsDeleted { get; set; }
    }
}