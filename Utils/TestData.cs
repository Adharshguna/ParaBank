using DotNetEnv;

namespace Parabank.Utils;

public static class TestData
{
    static TestData()
    {
        Env.Load();
    }

    public static string BaseUrl => Env.GetString("BASE_URL", "https://parabank.parasoft.com/");
    public static string ValidUsername => Env.GetString("VALID_USERNAME", "testuser");
    public static string ValidPassword => EncryptionUtil.Decrypt(Env.GetString("VALID_PASSWORD", "dGVzdHVzZXI=")); // Default encrypted "testuser"
    public static string InvalidUsername => Env.GetString("INVALID_USERNAME", "invaliduser");
    public static string InvalidPassword => EncryptionUtil.Decrypt(Env.GetString("INVALID_PASSWORD", "aW52YWxpZHBhc3M=")); // "invalidpass"

    // Registration data
    public static string ExistingUsername => Env.GetString("EXISTING_USERNAME", "testuser");
    public static string FirstName => Env.GetString("FIRST_NAME", "John");
    public static string LastName => Env.GetString("LAST_NAME", "Doe");
    public static string Address => Env.GetString("ADDRESS", "123 Main St");
    public static string City => Env.GetString("CITY", "Anytown");
    public static string State => Env.GetString("STATE", "CA");
    public static string ZipCode => Env.GetString("ZIP_CODE", "12345");
    public static string Phone => Env.GetString("PHONE", "555-1234");
    public static string Ssn => EncryptionUtil.Decrypt(Env.GetString("SSN", "MTIzLTQ1LTY3ODk=")); // "123-45-6789"
    public static string NewUsername => "newuser" + Guid.NewGuid().ToString().Substring(0, 5);
    public static string NewPassword => EncryptionUtil.Decrypt(Env.GetString("NEW_PASSWORD", "cGFzc3dvcmQxMjM=")); // "password123"
}