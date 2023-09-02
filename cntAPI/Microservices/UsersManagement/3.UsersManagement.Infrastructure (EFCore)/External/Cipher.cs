namespace _3.UsersManagement.Infrastructure__EFCore_.External
{
    public abstract class Cipher
    {
        public static string StringEncrypting(string str)
        {
            return StringCipher.Encrypt(str, GlobalVariables.SoftGuid);
        }
        public static string StringDecrypting(string str)
        {
            return  StringCipher.Decrypt(str, GlobalVariables.SoftGuid);
        }
    }
}