using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var sr = new StreamReader("input.txt"))
            {
                var input = sr.ReadToEnd().Split("\n");
                var regex = new Regex(@"position=< ?(?<posX>-?\d+),  ?(?<posY>-?\d+)> velocity=< ?(?<velX>-?\d+),  ?(?<velY>-?\d+)>");
                var points = new List<Point>();
                foreach(var line in input)
                {
                    var match = regex.Match(line);
                    int posX = int.Parse(match.Groups["posX"].Value);
                    int posY = int.Parse(match.Groups["posY"].Value);
                    int velX = int.Parse(match.Groups["velX"].Value);
                    int velY = int.Parse(match.Groups["velY"].Value);
                    points.Add(new Point(posX,posY,velX,velY));
                }

                int seconds = 0;
                while(points.Max(x => x.PosY) - points.Min(x => x.PosY) > 15)
                {
                    points.ForEach(x => x.Move());
                    seconds++;
                }
                       
                Console.WriteLine("Excercise 1 solution:");
                
                for(int y = points.Min(point => point.PosY); y <= points.Max(point => point.PosY); y++)
                {                   
                    for(int x = points.Min(point => point.PosX); x <= points.Max(point => point.PosX); x++)
                    {
                        if(points.Any(point => point.PosX == x && point.PosY == y))
                            Console.Write("O");
                        else
                            Console.Write(" ");
                    }                    
                    Console.WriteLine();
                }

                Console.WriteLine($"Excercise 2 solution: {seconds} seconds");
            }
            
        }
    }
}
