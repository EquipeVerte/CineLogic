using System.Linq;
using System.Security.Cryptography;

namespace CineLogic.Models.Hashing
{
    public class Hasher
    {
        public Hash GenerateHash(string stringToHash, int iterations = 1000)
        {
            //  Generate salt.
            var salt = new byte[24];
            new RNGCryptoServiceProvider().GetBytes(salt);

            //  Generate hash.
            var pbkdf2 = new Rfc2898DeriveBytes(stringToHash, salt, iterations);
            byte[] hash = pbkdf2.GetBytes(24);

            //  Return concatenation : salt|iterations|hash.
            return new Hash(salt, iterations,hash);
        }

        private byte[] Generate(string testString, byte[] salt, int iterations)
        {
            //  Generate hash.
            var pbkdf2 = new Rfc2898DeriveBytes(testString, salt, iterations);
            byte[] hash = pbkdf2.GetBytes(24);

            return hash;
        }

        public bool IsMatched(string testString, Hash hash)
        {
            byte[] hashedTestPassword = Generate(testString, hash.Salt, hash.Iterations);

            return hashedTestPassword.SequenceEqual(hash.HashedString);
        }
    }
}