namespace AutomotiveForumSystem.Exceptions
{
    public class AdminRightsNotCurrentlyGrantedException : ApplicationException
    {
        public AdminRightsNotCurrentlyGrantedException(string? message) : base(message) { }
    }
}
