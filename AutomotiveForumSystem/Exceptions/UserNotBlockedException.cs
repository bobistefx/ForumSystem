namespace AutomotiveForumSystem.Exceptions
{
    public class UserNotBlockedException : ApplicationException
    {
        public UserNotBlockedException(string? message) : base(message) { }
    }
}
