using DotNetEnv;

namespace Parabank.Utils;

public static class TestData
{
    public static string BaseUrl => Env.GetString("BASE_URL");
    public static string ValidUsername => Env.GetString("USERNAME");
    public static string ValidPassword => EncryptionUtil.Decrypt(Env.GetString("ENCRYPTED_PASSWORD"));
    public static string InvalidUsername => "invaliduser";
    public static string InvalidPassword => "invalidpass";

    // Registration data
    public static string FirstName => "John";
    public static string LastName => "Doe";
    public static string Address => "123 Main St";
    public static string City => "Anytown";
    public static string State => "CA";
    public static string ZipCode => "12345";
    public static string Phone => "555-1234";
    public static string Ssn => "123-45-6789";
    public static string NewUsername => "newuser" + Guid.NewGuid().ToString().Substring(0, 5);
    public static string NewPassword => "password123";
}