using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var input = new StreamReader("input.txt"))
            {
                 var text = input.ReadToEnd();
                Console.WriteLine($"Length at the beginning: {text.Length}");
                var reducedText = React(text);
                Console.WriteLine("Excercise 1");
                Console.WriteLine($"Length after reactioin: {reducedText.Length}");
                
                //Excercise 2
                string alphabet = "abcdefghijklmnoprstquvwxyz";
                var dict = new Dictionary<char,int>();
                foreach(var letter in alphabet)
                {
                    var polymerWithoutLetter = text.Where(x => !x.Equals(letter) && !x.Equals(char.ToUpper(letter)));
                    var sb = new StringBuilder();
                    polymerWithoutLetter.ToList().ForEach(x => sb.Append(x));
                    var afterReaction = React(sb.ToString());
                    dict.Add(letter,afterReaction.Length);
                }
                var entryWithLowestSize = dict.OrderBy(x => x.Value).First();
                Console.WriteLine("Excercise 2");
                Console.WriteLine($"Letter {entryWithLowestSize.Key}, polymer length {entryWithLowestSize.Value}");
            }
        }

        static string React(string s)
        {
            var stack = new Stack<char>();
            foreach(var letter in s)
            {
                if(stack.Count == 0)
                    stack.Push(letter);
                else if(char.IsLower(letter))
                {
                    if(!char.ToUpper(letter).Equals(stack.Peek()))
                        stack.Push(letter);
                    else
                    {
                        stack.Pop();
                    }
                }
                else
                {
                    if(!char.ToLower(letter).Equals(stack.Peek()))
                        stack.Push(letter);
                    else
                    {
                        stack.Pop();
                    }
                        
                }
            }
            var sb = new StringBuilder();
            stack.ToList().ForEach(x => sb.Append(x));
            return sb.ToString().Trim();
        }
    }
}
