using ConsoleTables;

namespace DiceGame
{
    internal class HelperTable
    {
        public static void Print(List<Dice> diceList)
        {
            Console.WriteLine("\nDice Win Probability Table:");

            var headers = new List<string> { "vs " };
            headers.AddRange(Enumerable.Range(1, diceList.Count).Select(i => $"D{i}"));

            var table = new ConsoleTable(headers.ToArray());

            for (int i = 0; i < diceList.Count; i++)
            {
                var row = new List<string> { $"D{i + 1}" };
                for (int j = 0; j < diceList.Count; j++)
                {
                    if (i == j) row.Add(" - ");
                    else
                    {
                        double prob = WinCalculator.WinProbability(diceList[i], diceList[j]);
                        row.Add(prob.ToString("0.00"));
                    }
                }
                table.AddRow(row.ToArray());
            }

            table.Write(Format.Alternative);
            Console.WriteLine();
        }
    }
}
