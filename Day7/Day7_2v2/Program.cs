using System;
using System.Collections.Generic;
using System.Numerics;
using System.IO;

class Program
{
    static void Main()
    {
        string[] grid = File.ReadAllLines("input.txt");// Counter: 58097428661390

        int height = grid.Length;
        int width = grid[0].Length;

        // number of paths per column
        BigInteger[] numberOfRays = new BigInteger[width];

        int startRow = 0;
        int startCol = grid[startRow].IndexOf('S');
        
        numberOfRays[startCol] = 1;
        BigInteger totalFinishedTimelines = 0;

        for (int r = startRow; r < height; r++)
        {
            BigInteger[] nextRowRays = new BigInteger[width];

            for (int c = 0; c < width; c++)
            {

                if(r == height-1)
                {
                    // when in last line, sum up all rays
                    totalFinishedTimelines += numberOfRays[c];
                }
                else
                {
                    if (numberOfRays[c] == 0) continue;

                    // check if the current cell is a splitter
                    // sum rays to left and right row
                    if (grid[r][c] == '^')
                    {
                        nextRowRays[c - 1] += numberOfRays[c];
                        nextRowRays[c + 1] += numberOfRays[c];
                    }
                    else // if empty space
                    {
                        // just add number of rays from the same col 
                        nextRowRays[c] += numberOfRays[c];
                    }
                }
            }
            numberOfRays = nextRowRays;
        }

        Console.WriteLine($"Counter: {totalFinishedTimelines}");
    }
}