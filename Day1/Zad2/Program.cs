using System;
using System.Collections.Generic;
using System.IO;

namespace Zad2
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            var visitedNumbers = new HashSet<int>();
            while(true)
            {
                using(StreamReader sr = new StreamReader("input.txt"))
                {
                    while(!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();
                        int number;
                        if(Int32.TryParse(line,out number)){
                            count += number;
                            if(visitedNumbers.Contains(count))
                            {
                                Console.WriteLine($"The number visited twice is {count}");
                                return;
                            }
                            else
                                visitedNumbers.Add(count);
                        }
                        else{
                            Console.WriteLine("Parsing error");
                        }
                    }
                }
            }
        }
    }
}
