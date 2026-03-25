using System.Text;

namespace Parabank.Utils;

public static class EncryptionUtil
{
    // Simple base64 "encryption" for demo purposes
    // In production, use proper encryption
    public static string Encrypt(string plainText)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
    }

    public static string Decrypt(string cipherText)
    {
        return Encoding.UTF8.GetString(Convert.FromBase64String(cipherText));
    }
}