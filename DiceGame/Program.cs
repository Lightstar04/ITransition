using System.Security.Cryptography;

namespace DiceGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DiceParser parser = new DiceParser(["2,2,4,4,9,9", "1,1,6,6,8,8", "3,3,5,5,7,7"]);
                List<Dice> diceList = parser.Parse();

                GameController game = new GameController(diceList);
                game.Start();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
            }
        }
    }
}
