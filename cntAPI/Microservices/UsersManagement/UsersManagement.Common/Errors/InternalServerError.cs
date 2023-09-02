using System;

namespace UsersManagement.Common.Errors
{
    public class InternalServerError : Exception
    {
        public InternalServerError(string message) : base(message)
        { }
    }
}
