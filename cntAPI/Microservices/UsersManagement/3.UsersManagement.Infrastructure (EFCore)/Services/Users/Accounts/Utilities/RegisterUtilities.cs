using System;
using System.Linq;
using _2.UsersManagement.Application.DTOs.Users.Accounts.in_Controllers;
using _3.UsersManagement.Infrastructure__EFCore_.External;

namespace _3.UsersManagement.Infrastructure__EFCore_.Services.Users.Accounts.Utilities
{
    public abstract class RegisterUtilities
    {
        public static string CreateUsername(CreateUsernameDto request)
        {
            var username = request.FirstName.Trim();
            var secondName = !string.IsNullOrEmpty(request.SecondName) ? request.SecondName.Trim()[..1].ToUpper() : "";
            var fLastName = request.FLastName.Trim()[..1].ToUpper();
            var sLastName = !string.IsNullOrEmpty(request.SLastName) ? request.SLastName.Trim().ToUpper()[..1].ToUpper() : "";

            username += secondName;
            username += fLastName;
            username += sLastName;

            if (string.IsNullOrEmpty(request.SecondName) && string.IsNullOrEmpty(request.SLastName))
            {
                username += request.FirstName[^2..].ToUpper();
            }
            else if (string.IsNullOrEmpty(request.SecondName) || string.IsNullOrEmpty(request.SLastName))
            {
                username += request.FirstName[^1].ToString().ToUpper();
            }

            return username;
        }
        
        public static string CreateRandomPassword()
        {
            var rnd = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var randomString = new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[rnd.Next(s.Length)]).ToArray());
            return StringCipher.Encrypt(randomString, GlobalVariables.SoftGuid);
        }
    }
}