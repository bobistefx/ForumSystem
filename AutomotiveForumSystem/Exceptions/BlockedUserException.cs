namespace AutomotiveForumSystem.Exceptions
{
    public class BlockedUserException : ApplicationException
    {
        public BlockedUserException(string message) : base(message) { }
    }
}
