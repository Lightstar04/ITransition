namespace DiceGame
{
    internal class DiceParser
    {
        private readonly string[] args;

        public DiceParser(string[] args)
        {
            this.args = args;
        }

        public List<Dice> Parse()
        {
            if (args.Length < 3)
            {
                throw new ArgumentException("You must provide at least 3 dice.");
            }
            
            return args.Select(arg => new Dice(arg)).ToList();
        }
    }
}
