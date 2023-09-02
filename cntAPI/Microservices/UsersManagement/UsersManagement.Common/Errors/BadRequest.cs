using System;

namespace UsersManagement.Common.Errors
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        { }
    }
}
