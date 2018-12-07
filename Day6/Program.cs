using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            var locations = new List<Location>();
            using(var sr = new StreamReader("input.txt"))
            {
                var lines = sr.ReadToEnd().Split("\n");
                int i = 1;
                foreach(var line in lines)
                {
                    var regex = new Regex(@"(?<x>\d+), (?<y>\d+)");
                    var match = regex.Match(line);
                    int coordinateX = int.Parse(match.Groups["x"].Value);
                    int coordinateY =  int.Parse(match.Groups["y"].Value);
                    locations.Add(new Location(){Nr  = i,CoordinateX = coordinateX, CoordinateY = coordinateY});

                    i++;
                }

                var orderedLocations = locations.OrderBy(x => x.CoordinateX);
                int maxX = orderedLocations.Last().CoordinateX;
                int minX = orderedLocations.First().CoordinateX;

                orderedLocations = locations.OrderBy(x => x.CoordinateY);
                int maxY = orderedLocations.Last().CoordinateY;
                int minY = orderedLocations.First().CoordinateY;

                var board = new List<Location>();

                for(i = minX; i <= maxX; i++)
                {
                    for(int j = minY; j <= maxY; j++)
                    {
                        var orderedPositions = locations.OrderBy(x => x.CalculateDistance(i,j));
                        var newLocation = new Location(){Nr = 0,CoordinateX = i, CoordinateY = j, SumOfTheDistances = 0};
                        foreach(var location in locations)
                        {
                            // for 2nd excercise
                            newLocation.SumOfTheDistances += newLocation.CalculateDistance(location.CoordinateX,location.CoordinateY);
                        }
                        
                        if(orderedPositions.ToArray()[0].CalculateDistance(i,j) == orderedPositions.ToArray()[1].CalculateDistance(i,j))
                            newLocation.Nr = 0;
                        else
                            newLocation.Nr = orderedPositions.First().Nr;

                        board.Add(newLocation);
                        
                    }
                }

                

                var infiniteAreas = new HashSet<int>();
                board.Where(x => x.CoordinateX == minX || x.CoordinateX == maxX || x.CoordinateY == minY || x.CoordinateY == maxY).ToList().ForEach(x=>
                {
                    infiniteAreas.Add(x.Nr);
                });

                var dict = board.GroupBy(x => x.Nr).ToDictionary(x => x.Key, y => y.Count());
                foreach(var entry in infiniteAreas)
                {
                    dict.Remove(entry);
                }
                var foundLocation = dict.OrderBy(x => x.Value).Last();
                Console.WriteLine($"Excercise 1 answer: {foundLocation.Value}");
                
                //Excercise 2
                Console.WriteLine($"Excercise 2 answer: {board.Where(x => x.SumOfTheDistances < 10000).Count()}");
                

            }
        }
    }
}
