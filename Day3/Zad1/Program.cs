using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Zad1
{
    class Program
    {
        
        private static (int column, int row, int width, int height) GetValues(string s)
        {
            var regex = new Regex(@"#(?<id>\d+) @ (?<column>\d+),(?<row>\d+): (?<width>\d+)x(?<height>\d+)");
            var result = regex.Match(s);
            return (int.Parse(result.Groups["column"].Value),int.Parse(result.Groups["row"].Value),int.Parse(result.Groups["width"].Value),int.Parse(result.Groups["height"].Value));
        }

        static void Main(string[] args)
        {
            var fabric = new int[1000,1000];
            using(StreamReader sr = new StreamReader("input.txt"))
            {
                var text = sr.ReadToEnd().Split("\n");
                text.Select(x => GetValues(x)).ToList().ForEach(
                    x => 
                    {
                        for(int i = x.row; i < x.row + x.height; i++)
                        {
                            for(int j = x.column; j < x.column + x.width; j++)
                            {
                                fabric[i,j]++;
                            }
                        }
                    }
                );
            }
            
            int count = 0;
            foreach(var square in fabric)
            {
                if(square > 1)
                    count++;
            }
             Console.WriteLine($"{count}");
        }
    }

}
