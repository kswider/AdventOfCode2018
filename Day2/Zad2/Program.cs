using System;
using System.IO;
using System.Linq;

namespace Zad2
{
    class Program
    {
        static void Main(string[] args)
        {
            using(StreamReader sr = new StreamReader("input.txt"))
            {
                var input = sr.ReadToEnd();
                var ids = input.Split("\n");
                for (int i =0; i < ids[0].Length; i++)
                {
                    var pair = ids.Select(x => x.Remove(i,1)).GroupBy(x => x).FirstOrDefault(x=> x.Count() > 1);
                    if(pair != null)
                        Console.WriteLine(pair.First());
                }
                
                
            }
        }
    }


}
