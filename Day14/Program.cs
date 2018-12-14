using System;
using System.Collections.Generic;
using System.Linq;

namespace Day14
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfRecipes = 306281;
            var recipes = new LinkedList<int>();
            recipes.AddLast(3);
            recipes.AddLast(7);
            var elf1 = recipes.First;
            var elf2 = recipes.Last;
            /* 
            Excercise 1
            while(recipes.Count < numberOfRecipes + 10)
            {
                MakeNewRecipes(ref elf1, ref elf2,recipes);
            }
            
            *
            Console.WriteLine($"Excercise 1 solution: ");

            for(int i = numberOfRecipes; i < numberOfRecipes + 10; i++)
            {
                Console.Write(recipes.ElementAt(i));
            }
            */

            // Excercise 2
            while(!MakeNewRecipesWithCheck(ref elf1, ref elf2, recipes)){ }
            Console.WriteLine($"Excercise 2 solution = {recipes.Count - 6}"); //sequence to find has 6 digits
        }

        static void MakeNewRecipes(ref LinkedListNode<int> first,ref LinkedListNode<int> second, LinkedList<int> recipes)
        {
            int sum = first.Value + second.Value;
            if(sum >= 10)
            {
                recipes.AddLast(1);
            }
                
            recipes.AddLast(sum % 10);
            GoToNextNode(ref first,recipes);
            GoToNextNode(ref second,recipes);
        }

        static bool MakeNewRecipesWithCheck(ref LinkedListNode<int> first,ref LinkedListNode<int> second, LinkedList<int> recipes)
        {
            int sum = first.Value + second.Value;
            if(sum >= 10)
            {
                recipes.AddLast(1);
                if(CheckSequence(recipes.Last))
                    return true;
            }
                
            recipes.AddLast(sum % 10);
            if(CheckSequence(recipes.Last))
                return true;
            GoToNextNode(ref first,recipes);
            GoToNextNode(ref second,recipes);
            return false;
        }

        static void GoToNextNode(ref LinkedListNode<int> node, LinkedList<int> nodes)
        {
            int valueOfChange = node.Value + 1;
            for(int i = 0; i < valueOfChange; i++)
            {
                node = node.Next ?? nodes.First;
            }
        }

        static bool CheckSequence(LinkedListNode<int> node)
        {
            // written sequence 306281 backwards to allow easier check after each addition to list
            var sequence = new List<int>() {1, 8, 2, 6, 0, 3};
            foreach(var element in sequence)
            {
                if(element != node.Value)
                    return false;
                node = node.Previous;
            }
            return true;

        }
    }
}
