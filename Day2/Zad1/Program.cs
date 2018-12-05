using System;
using System.IO;
using System.Linq;

namespace Zad1
{
    class Program
    {
        static void Main(string[] args)
        {
            int twoLetters = 0, threeLetters = 0;
            
            using(StreamReader sr = new StreamReader("input.txt"))
                {
                    var lineNumber = 0;
                    while(!sr.EndOfStream)
                    {
                        lineNumber++;
                        var line = sr.ReadLine();
                        var occurences = line.GroupBy(x => x).OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Count());
                        if(occurences.ContainsValue(2))
                            twoLetters++;
                        if(occurences.ContainsValue(3))
                            threeLetters++;
                    }
                }
            Console.WriteLine($"{twoLetters} x {threeLetters} = {twoLetters * threeLetters}");
        }
    }
}
