namespace UserManagement.Domain.Exceptions
{
    public class EntityExistsException : Exception
    {
        public EntityExistsException() : base() { }
        public EntityExistsException(string message) : base(message) { }
        public EntityExistsException(string message, Exception inner) : base(message, inner) { }
    }
}
