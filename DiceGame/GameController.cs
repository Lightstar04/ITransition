namespace DiceGame
{
    internal class GameController
    {
        private readonly List<Dice> diceList;

        public GameController(List<Dice> diceList)
        {
            this.diceList = diceList;
        }

        public void Start()
        {
            Console.WriteLine("Let's determine who makes the first move.");
            FairRollProtocol fair = new FairRollProtocol(2);
            int result = fair.Execute("Starting");

            bool userMovesFirst = result == 0;

            Console.WriteLine(userMovesFirst ? "You move first." : "Computer moves first.");

            int userIndex, computerIndex;

            if (userMovesFirst)
            {
                userIndex = PromptDiceSelection("your", -1);
                computerIndex = ComputerSelectDice(userIndex);
            }
            else
            {
                computerIndex = ComputerSelectDice(-1);
                userIndex = PromptDiceSelection("your", computerIndex);
            }

            Console.WriteLine($"\nYou chose:     Dice {userIndex + 1} [{diceList[userIndex]}]");
            Console.WriteLine($"Computer chose: Dice {computerIndex + 1} [{diceList[computerIndex]}]");

            int userRoll = PerformFairRoll(diceList[userIndex], "Your");
            int computerRoll = PerformFairRoll(diceList[computerIndex], "Computer");

            Console.WriteLine($"\nYou rolled: {userRoll} | Computer rolled: {computerRoll}");

            if (userRoll > computerRoll) Console.WriteLine("You win!");
            else if (userRoll < computerRoll) Console.WriteLine("Computer wins!");
            else Console.WriteLine("It's a draw!");
        }

        private int PromptDiceSelection(string participant, int excludeIndex)
        {
            while (true)
            {
                Console.WriteLine("\nAvailable Dice:");

                for (int i = 0; i < diceList.Count; i++)
                {
                    if (i == excludeIndex) continue;
                    Console.WriteLine($"{i + 1}. [{diceList[i]}]");
                }

                Console.WriteLine("H. Help table");
                Console.WriteLine("X. Exit");

                Console.Write($"\n{participant} choice: ");
                string input = Console.ReadLine().ToLower();

                if (input == "x") Environment.Exit(0);
                if (input == "h") HelperTable.Print(diceList);

                if (int.TryParse(input, out int choice) &&
                    choice > 0 && choice <= diceList.Count &&
                    choice - 1 != excludeIndex)
                    return choice - 1;

                Console.WriteLine("Invalid input.");
            }
        }

        private int ComputerSelectDice(int excludeIndex)
        {
            var rand = new Random();
            int index;
            do
            {
                index = rand.Next(diceList.Count);
            } 
            while (index == excludeIndex);

            return index;
        }

        private int PerformFairRoll(Dice dice, string owner)
        {
            FairRollProtocol protocol = new FairRollProtocol(dice.Faces.Length);
            int result = protocol.Execute(owner);
            return dice.Roll(result);
        }
    }
}
