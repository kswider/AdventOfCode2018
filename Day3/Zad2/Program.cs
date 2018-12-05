using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Zad2
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

            int rowNumber, columnNumber;
            
            using(StreamReader sr = new StreamReader("input.txt"))
            {
                var text = sr.ReadToEnd().Split("\n");     
                (columnNumber, rowNumber, _, _) = text.Select(x => GetValues(x)).ToList().FirstOrDefault(
                    x => 
                    {
                        bool overlapped = false;
                        for(int i = x.row; i < x.row + x.height; i++)
                        {
                            for(int j = x.column; j < x.column + x.width; j++)
                            {
                                if(fabric[i,j] != 1)
                                    overlapped = true; 
                            }
                        }
                        return !overlapped;
                    }
                );                     
            }

            using(StreamReader sr = new StreamReader("input.txt"))
            {
                var text = sr.ReadToEnd().Split("\n");
                var foundText = text.FirstOrDefault(
                    x => 
                    {
                        int column, row;
                        (column,row,_,_) = GetValues(x);
                        if(row == rowNumber && column == columnNumber)
                            return true;
                        return false;
                    }
                );    
                Console.WriteLine(foundText);
            }         
        }
    }
}
