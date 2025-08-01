﻿namespace UserManagement.Domain.Exceptions
{
    public class InvalidCredentialException : Exception
    {
        public InvalidCredentialException() : base() { }
        public InvalidCredentialException(string message) : base(message) { }
        public InvalidCredentialException(string message, Exception inner) : base(message, inner) { }
    }
}
