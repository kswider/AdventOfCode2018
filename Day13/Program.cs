using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day13
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var sr = new StreamReader("input.txt"))
            {
                var map = new char[150,150];
                int column = 0, row = 0;
                var carts = new List<Cart>();
                foreach(var line in sr.ReadToEnd().Split("\n"))
                {
                    column = 0;
                    foreach(var c in line)
                    {
                        switch(c)
                        {
                            case '<':
                                carts.Add(new Cart(column,row,Direction.LEFT));
                                map[row,column] = '-';
                                break;
                            case '^':
                                carts.Add(new Cart(column,row,Direction.UP));
                                map[row,column] = '|';
                                break;
                            case '>':
                                carts.Add(new Cart(column,row,Direction.RIGHT));
                                map[row,column] = '-';
                                break;
                            case 'v':
                                carts.Add(new Cart(column,row,Direction.DOWN));
                                map[row,column] = '|';
                                break;
                            default:
                                map[row,column] = c;
                                break;
                        }
                        column++;
                        
                    }
                    row++;
                }

                int iterations = 0;
                bool solutionOneFound = false;
                while(carts.Count() > 1)
                {
                    carts = carts.OrderBy(x => x.PositionY).ThenBy(x => x.PositionX).ToList();
                    iterations++;
                    var cartsToDelete = new List<Cart>();
                    carts.ForEach( x =>
                    {
                        x.Move(map);
                        var foundCarts = carts.Where(cart => cart.PositionX == x.PositionX && cart.PositionY == x.PositionY && cart != x);
                        if(foundCarts.FirstOrDefault() != null)
                        {
                            if(!solutionOneFound)
                            {
                                Console.WriteLine($"Excercise 1 solution: x = {x.PositionX}, y = {x.PositionY} iterations = {iterations}");
                                solutionOneFound = true;
                            }
                            cartsToDelete.Add(foundCarts.First());
                            cartsToDelete.Add(x);
                        }
                    });

                    foreach(var cart in cartsToDelete)
                    {
                        carts.Remove(cart);
                    }
                }
                var foundCart = carts.First();
                Console.WriteLine($"Excercise 2 solution: x = {foundCart.PositionX}, y = {foundCart.PositionY} iterations = {iterations}");
                
            }
        }
    }
}
