using System.Security.Cryptography;
using System.Text;

namespace DiceGame
{
    internal class FairRandomGenerator
    {
        public byte[] SecretKey { get; private set; }
        public int SecretNumber { get; private set; }
        public string HMAC { get; private set; }

        public FairRandomGenerator(int range)
        {
            SecretKey = new byte[32];
            RandomNumberGenerator.Fill(SecretKey);

            SecretNumber = RandomNumberGenerator.GetInt32(0, range);

            using var hmacsha = new HMACSHA256(SecretKey);
            var hash = hmacsha.ComputeHash(Encoding.UTF8.GetBytes(SecretNumber.ToString()));
            HMAC = BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        public int Resolve(int userNumber, int mod) => (userNumber + SecretNumber) % mod;
    }
}
