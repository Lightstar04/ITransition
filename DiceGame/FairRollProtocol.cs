namespace DiceGame
{
    internal class FairRollProtocol
    {
        private readonly int range;
        private readonly FairRandomGenerator gen;

        public FairRollProtocol(int range)
        {
            this.range = range;
            gen = new FairRandomGenerator(range);
        }

        public int Execute(string participant)
        {
            Console.WriteLine($"{participant} HMAC: {gen.HMAC}");
            Console.Write($"Choose your number between 0 and {range - 1}: ");

            if (!int.TryParse(Console.ReadLine(), out int userInput) || userInput < 0 || userInput >= range)
            {
                throw new ArgumentException("Invalid input.");
            }

            int result = gen.Resolve(userInput, range);
            Thread.Sleep(1000);

            Console.WriteLine($"Secret Key: {BitConverter.ToString(gen.SecretKey).Replace("-", "").ToLower()}");
            Console.WriteLine($"Computer number: {gen.SecretNumber}");
            Console.WriteLine($"{participant}'s result = (you + secret) mod {range} = {result}");

            return result;
        }
    }
}
