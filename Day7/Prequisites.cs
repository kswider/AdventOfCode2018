using System.Collections.Generic;

namespace Day7
{
    public class Prequisites
    {
        public List<string> ListOfPrequisites { get; set; } = new List<string>();
        public bool CanBeExecuted { get; set; }

        public Prequisites()
        {
            CanBeExecuted = false;
        }

        public void SwitchStatus()
        {
            if(ListOfPrequisites.Count == 0)
                CanBeExecuted = true;
        }
    }
}