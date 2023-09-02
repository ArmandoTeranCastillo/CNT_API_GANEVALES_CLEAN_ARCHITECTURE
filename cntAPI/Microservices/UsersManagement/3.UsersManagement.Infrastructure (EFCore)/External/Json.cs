using Newtonsoft.Json;

namespace _3.UsersManagement.Infrastructure__EFCore_.External
{
    public class Json
    {
        public static T Deserialize<T>(object jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString.ToString(), JsonSettings.Insensitive);
        }

    }
}