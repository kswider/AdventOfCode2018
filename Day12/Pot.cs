namespace Day12
{
    public class Pot
    {
        public int Number { get; set; }

        public string Symbol { get; set; }
        public Pot(int number, char symbol)
        {
            Number = number;
            Symbol = symbol.ToString();
        }
    }
}