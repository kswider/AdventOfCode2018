using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day8
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var sr = new StreamReader("input.txt"))
            {
                var numbers = new Queue<int>(sr.ReadToEnd().Split(" ").Select(x => int.Parse(x)));

                var root = CreateNode(numbers);
                Console.WriteLine($"Excercise 1 solution: {SumMetadata(root)}");
                // Excercise 2
                Console.WriteLine($"Excercise 2 solution: {CalculateNodeValue(root)}");
            }
        }
        static Node CreateNode(Queue<int> numbers)
        {
            var node = new Node(numbers.Dequeue(), numbers.Dequeue());
            for(int i = 0; i < node.NumberOfChildren; i++)
            {
                node.Children.Add(CreateNode(numbers));
            }
            for(int i = 0; i < node.NumberOfMetadata; i++)
            {
                node.Metadata.Add(numbers.Dequeue());
            }
            return node;
        }

        static int SumMetadata(Node root) => root.Metadata.Sum() + root.Children.Select(x => SumMetadata(x)).Sum();

        static int CalculateNodeValue(Node root)
        {
            if(root == null)
                return 0;
            if(root.Children.Count() == 0)
                return root.Metadata.Sum();
            
            int childrenSum = 0;
            foreach(var metadata in root.Metadata)
            {
                childrenSum += CalculateNodeValue(root.Children.ElementAtOrDefault(metadata - 1));
            }
            return childrenSum;
        }
    }
}
