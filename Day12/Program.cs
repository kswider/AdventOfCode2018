using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var sr = new StreamReader("input.txt"))
            {
                var initialRegex = new Regex(@": (?<initialState>.*)");
                var initialState = initialRegex.Match(sr.ReadLine()).Groups["initialState"].Value;
                var pots = new LinkedList<Pot>();
                int counter = 0;
                foreach(var symbol in initialState)
                {
                    pots.AddLast(new Pot(counter, symbol));
                    counter++;
                }            

                sr.ReadLine();
                var rules = new Dictionary<string,char>();
                var ruleRegex = new Regex(@"(?<before>.*) => (?<after>.)");
                while(!sr.EndOfStream)
                {
                    var match = ruleRegex.Match(sr.ReadLine());
                    rules.Add(match.Groups["before"].Value,match.Groups["after"].Value[0]);
                }
                
                var sums = new HashSet<int>();
                for(int i = 0; i < 20; i++)
                {
                    pots = Generate(pots,rules);
                           
                }
                Console.WriteLine($"Excercies 1 solution: {pots.Where(x => x.Symbol.Equals("#")).Sum(x => x.Number)}");     

                //Part 2 - needed to find a pattern - after 98 iterations each sum was 25 higher than previous
                long totalIterations = 50000000000;
                int stabilizationPoint = 98;
                int stabilizationPointValue = 3441;
                int increaseValue = 25;

                Console.WriteLine($"Excercies 2 solutions: {stabilizationPointValue + (increaseValue * (totalIterations - stabilizationPoint))}");
            }
        }

        static LinkedList<Pot> Generate(LinkedList<Pot> oldGeneration, Dictionary<string,char> rules)
        {

            var newGeneration = new LinkedList<Pot>();
            //add some pots before
            int firstValue = oldGeneration.First.Value.Number;
            for(int i = firstValue - 1; i > firstValue - 3; i--)
            {
                oldGeneration.AddFirst(new Pot(i,'.'));
            }
            
            //add some pots after
            int lastValue = oldGeneration.Last.Value.Number;
            for(int i = lastValue + 1; i < lastValue + 3; i++)
            {
                oldGeneration.AddLast(new Pot(i,'.'));
            }

            LinkedListNode<Pot> currentPot = oldGeneration.First;
            while(currentPot != null)
            {
                newGeneration.AddLast(ApplyRules(currentPot,rules));
                currentPot = currentPot.Next;
            }
            
            return newGeneration;
        }

        static Pot ApplyRules(LinkedListNode<Pot> currentPot, Dictionary<string,char> rules)
        {
            string beforeString = PrepareString(currentPot);
            if(rules.ContainsKey(beforeString))
            {
                return  new Pot(currentPot.Value.Number, rules[beforeString]);
            }
            return currentPot.Value;
        }

        static string PrepareString(LinkedListNode<Pot> pot)
        {
            var sb = new StringBuilder();
            sb.Append(pot.Previous?.Previous?.Value?.Symbol ?? ".");
            sb.Append(pot.Previous?.Value?.Symbol ?? ".");
            sb.Append(pot.Value.Symbol);
            sb.Append(pot.Next?.Value.Symbol ?? ".");
            sb.Append(pot.Next?.Next?.Value?.Symbol ?? ".");
            return sb.ToString();
        }
    }
}
