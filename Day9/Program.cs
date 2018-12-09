using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day9
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var sr = new StreamReader("input.txt"))
            {
                var input = sr.ReadToEnd();
                var regex = new Regex(@"(?<numberOfPlayers>\d+) .* worth (?<lastMarbleValue>\d+)");
                var match = regex.Match(input);
                int numberOfPlayers = int.Parse(match.Groups["numberOfPlayers"].Value);
                int lastMarbleValue = int.Parse(match.Groups["lastMarbleValue"].Value);

                var players = new List<Player>();
                for(int i = 1; i <= numberOfPlayers; i++)
                {
                    players.Add(new Player(i));
                }
                
                var marbles = new LinkedList<Marble>();
                var firstMarble = new Marble(0);
                var currentMarbleNode = marbles.AddFirst(firstMarble);

                for(int currentMarbleNumber = 1; currentMarbleNumber <= lastMarbleValue; currentMarbleNumber++)
                {
                    
                    if(currentMarbleNumber % 23 == 0)
                    {
                        var playerGettingPoints = players.ElementAt(currentMarbleNumber % numberOfPlayers);
                        var marbleToDelete = GoBack7(marbles,currentMarbleNode);

                        playerGettingPoints.NumberOfPoints += currentMarbleNumber + marbleToDelete.Value.Value;
                        currentMarbleNode = marbleToDelete.Next ?? marbles.First; 
                        marbles.Remove(marbleToDelete);
                    }
                    else
                    {
                        var marbleToInsertAfter = currentMarbleNode.Next ?? marbles.First;
                        currentMarbleNode = marbles.AddAfter(marbleToInsertAfter,new Marble(currentMarbleNumber));                       
                    }
                }

                //Excercise 1
                Console.WriteLine($"Excercise 1 - max score: {players.Max(x => x.NumberOfPoints)}");

                for(int currentMarbleNumber = lastMarbleValue + 1; currentMarbleNumber <= lastMarbleValue * 100; currentMarbleNumber++)
                {
                    
                    if(currentMarbleNumber % 23 == 0)
                    {
                        var playerGettingPoints = players.ElementAt(currentMarbleNumber % numberOfPlayers);
                        var marbleToDelete = GoBack7(marbles,currentMarbleNode);

                        playerGettingPoints.NumberOfPoints += currentMarbleNumber + marbleToDelete.Value.Value;
                        currentMarbleNode = marbleToDelete.Next ?? marbles.First; 
                        marbles.Remove(marbleToDelete);
                    }
                    else
                    {
                        var marbleToInsertAfter = currentMarbleNode.Next ?? marbles.First;
                        currentMarbleNode = marbles.AddAfter(marbleToInsertAfter,new Marble(currentMarbleNumber));                       
                    }
                }

                //Excercise 2
                Console.WriteLine($"Excercise 2 - max score: {players.Max(x => x.NumberOfPoints)}");
            }
        }

        static LinkedListNode<Marble> GoBack7(LinkedList<Marble> list, LinkedListNode<Marble> currentMarble)
        {
            var current = currentMarble;
            for(int i = 0; i < 7; i++)
            {
                current = current.Previous ?? list.Last;
            }
            return current;
        }

    }
}
