namespace AutomotiveForumSystem.Models.Contracts
{
    public interface IRole
    {
        int Id { get; set; }
        string Name { get; set; }
        IList<User> Users { get; set; }
        bool IsDeleted { get; set; }
    }
}
