namespace DiceGame
{
    internal class Dice
    {
        public int[] Faces {  get; set; }
        
        public Dice(string defination)
        {
            var parts = defination.Split(',');
            if(parts.Length < 2)
            {
                throw new ArgumentException("Each die must have at least 2 faces");
            }
            Faces = parts.Select(p =>
            {
                if(!int.TryParse(p.Trim(), out var face))
                {
                    throw new ArgumentException("All dice must be integer");
                }
                return face;
            }).ToArray();
        }

        public int Roll (int num)
        {
            return Faces[num%Faces.Length];
        }

        public override string ToString()
        {
            return string.Join(",", Faces);
        }
    }
}
