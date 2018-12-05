using System;
using System.IO;

namespace Zad1
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            using(StreamReader sr = new StreamReader("input.txt"))
            {
                while(!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    int number;
                    if(Int32.TryParse(line,out number)){
                        Console.WriteLine($"Read number is {number}");
                        count += number;
                    }
                    else{
                        Console.WriteLine("Parsing error");
                    }
                }
            }
            Console.WriteLine(count);
        }
    }
}
