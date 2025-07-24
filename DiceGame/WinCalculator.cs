namespace DiceGame
{
    internal class WinCalculator
    {
        public static double WinProbability(Dice a, Dice b)
        {
            int win = 0, total = a.Faces.Length * b.Faces.Length;

            foreach (var x in a.Faces)
                foreach (var y in b.Faces)
                    if (x > y) win++;

            return (double)win / total;
        }
    }
}
