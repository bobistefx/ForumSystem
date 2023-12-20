namespace AutomotiveForumSystem.Models.DTOs
{
    public class UserUpdateAccountStatusDTO
    {
        public bool IsBlocked { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsAdmin { get; set; }
    }
}
