using System.Security.Cryptography;
using System.Text;

namespace Talent;

public class Cyber
{

    public static string HashPassowrd(string plainText)
    {
        string salt = "make things go right";
        byte[] saltByte = Encoding.UTF8.GetBytes(salt);
        Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(plainText, saltByte, 17, HashAlgorithmName.SHA512);
        byte[] hashBytes = rfc.GetBytes(64);
        string hashPassword= Convert.ToBase64String(hashBytes);
 
        return hashPassword;
    }


}
