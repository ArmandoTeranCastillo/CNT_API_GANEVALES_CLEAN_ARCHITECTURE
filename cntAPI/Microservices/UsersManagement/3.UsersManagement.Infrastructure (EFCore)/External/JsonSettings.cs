using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace _3.UsersManagement.Infrastructure__EFCore_.External
{
    public class JsonSettings
    {
        public static JsonSerializerSettings Insensitive { get; } = new()
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new DefaultNamingStrategy
                {
                    ProcessDictionaryKeys = false,
                    OverrideSpecifiedNames = false
                }
            }
        };

    }
}