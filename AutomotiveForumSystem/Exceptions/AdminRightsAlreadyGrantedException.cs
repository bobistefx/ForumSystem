namespace AutomotiveForumSystem.Exceptions
{
    public class AdminRightsAlreadyGrantedException : ApplicationException
    {
        public AdminRightsAlreadyGrantedException(string? message) : base(message) { }
    }
}
