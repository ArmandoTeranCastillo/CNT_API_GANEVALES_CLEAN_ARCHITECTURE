using System.Text.RegularExpressions;
using UsersManagement.Common.Errors;

namespace UsersManagement.Common.Utilities
{
    public abstract class Functions
    {
        public static void IsNumeric(string str)
        {
            if(!Regex.IsMatch(str, @"^\d+$")) throw new BadRequestException(Codes.HasCharacters);
        }
        
        public static void IsNullOrEmpty(string str)
        {
            if (string.IsNullOrEmpty(str)) throw new BadRequestException(Codes.EmptyField);
        }
    }
}
