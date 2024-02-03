
using System.Security.Cryptography;
namespace LearnMS.API.Security.PasswordHasher;

public class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 128 / 8;
    private const int KeySize = 256 / 8;
    private const int Iterations = 10000;
    private readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA256;
    private const string Delimiter = ";";

    public string Hash(string password)
    {
        var sault = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, sault, Iterations, _hashAlgorithm, KeySize);

        return string.Join(Delimiter, Convert.ToBase64String(sault), Convert.ToBase64String(hash));
    }

    public bool Verify(string hashedPassword, string providedPassword)
    {
        var elements = hashedPassword.Split(Delimiter);
        var sault = Convert.FromBase64String(elements[0]);
        var hash = Convert.FromBase64String(elements[1]);

        var hashInput = Rfc2898DeriveBytes.Pbkdf2(providedPassword, sault, Iterations, _hashAlgorithm, KeySize);

        return CryptographicOperations.FixedTimeEquals(hash, hashInput);
    }
}