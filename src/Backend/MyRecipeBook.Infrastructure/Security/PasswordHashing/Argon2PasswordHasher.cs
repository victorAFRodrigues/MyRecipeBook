using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;
using MyRecipeBook.Domain.Security.PasswordHashing;

namespace MyRecipeBook.Infrastructure.Security.PasswordHashing;

internal sealed class Argon2PasswordHasher : IPasswordHasher
{
    private const int DEGREE_OF_PARALLELLISM = 1;
    private const int ITERATIONS = 2;
    private const int MEMORY_SIZE = 20 * 1024;
    private const int SALT_SIZE = 16;
    private const int HASH_SIZE = 32;
    
    public string HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SALT_SIZE);

        var hash = HashPassword(password, salt);
        
        var combinedHash = new byte[hash.Length + salt.Length];
        
        salt.CopyTo(combinedHash);
        hash.CopyTo(combinedHash, index: salt.Length);
        
        return Convert.ToBase64String(combinedHash);
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        var combinedHash = Convert.FromBase64String(passwordHash);

        var salt = new byte[SALT_SIZE];
        var hash = new byte[HASH_SIZE];
        
        Array.Copy(combinedHash, salt, SALT_SIZE);
        
        Array.Copy(combinedHash, SALT_SIZE, hash, 0, HASH_SIZE);
        
        var newHash = HashPassword(password, salt);
        
        return CryptographicOperations.FixedTimeEquals(newHash,hash);
    }

    private byte[] HashPassword(string password, byte[] salt)
    {
        var bytePassword = Encoding.UTF8.GetBytes(password);
        
        var hashAlgorithm = new Argon2id(bytePassword)
        {
            DegreeOfParallelism = DEGREE_OF_PARALLELLISM,
            Iterations =  ITERATIONS,
            MemorySize = MEMORY_SIZE,
            Salt = salt
        };

        return hashAlgorithm.GetBytes(HASH_SIZE);
    }
}