using System;

namespace Day11
{
    class Program
    {
        static void Main(string[] args)
        {
            int gridSerialNumber = 6303;
            var grid = new int[300,300];
            for(int row = 1; row <= 300; row++)
            {
                for(int col = 1; col <= 300; col++)
                {
                    int rackID = col + 10;
                    int powerLevel = rackID * row;
                    powerLevel += gridSerialNumber;
                    powerLevel *= rackID;
                    powerLevel = powerLevel % 1000 / 100 - 5;
                    grid[row-1,col-1] = powerLevel;
                }
            }         

            int currentMax = 0;
            int curRow = 0, curCol = 0, curSquareSize = 1;
            for(int squareSize = 1; squareSize <= 300; squareSize++)
            {
                for(int row = 0; row < 300; row++)
                {
                    for(int col = 0; col < 300; col++)
                    {
                        int currentSum = CalculateSquare(grid,col,row,squareSize);
                        if(currentSum > currentMax)
                        {
                            curRow = row;
                            curCol = col;
                            curSquareSize = squareSize;
                            currentMax = currentSum;
                        }
                    }
                }
                if(squareSize ==3)
                    Console.WriteLine($"Excercise 1 solution: x = {curCol + 1} y = {curRow + 1} sum = {currentMax} square: {curSquareSize}");
            }
            Console.WriteLine($"Excercise 2 solution: x = {curCol + 1} y = {curRow + 1} sum = {currentMax} square: {curSquareSize}");

        }

        static int CalculateSquare(int[,] grid, int x, int y, int squareSize)
        {
            if(x + squareSize > 300 || y + squareSize > 300)
                return 0;

            int sum = 0;
            
            for(int row = y; row < y + squareSize; row++ )
            {
                for(int col = x; col < x + squareSize; col++)
                {
                    sum += grid[row,col];
                }
            }
            return sum;
        }
    }
}
