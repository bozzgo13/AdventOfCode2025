using Day7_2;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq.Expressions;
using System.Numerics; 

class Program
{
    static void Main()
    {
        var lines = File.ReadAllLines("input.txt");

        // var lines = new string[] {
        //".......S.......",
        //"...............",
        //".......^.......",
        //"...............",
        //"......^.^......",
        //"...............",
        //".....^.^.^.....",
        //"...............",
        //"....^.^...^....",
        //"...............",
        //"...^.^...^.^...",
        //"...............",
        //"..^...^.....^..",
        //"...............",
        //".^.^.^.^.^...^.",
        //"...............",
        //    };
        int cols = lines[0].Length;
        int rows = lines.Length;
        char[,] grid = new char[rows, cols];
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                grid[r, c] = lines[r][c];
            }
        }
        
        int startinPos = lines[0].IndexOf("S");
        int rowPosition = 0;


        Node n = new LinearNode(null, new Position(rowPosition, startinPos), ParentPosition.None);
        n.Depth = 0;
        Node startingNode = n;
        BigInteger counter = BigInteger.Zero;
        bool isEnd = false;
        while (!isEnd)
        {

            if (n.Pos.row == rows - 1)
            {
                counter++;

                if (n.ParentPos == ParentPosition.Left)
                {
                    ForkNode p = n.Prev as ForkNode;
                    n = p.NextRight;
                    continue;
                }
                if (n.ParentPos == ParentPosition.Right || n.ParentPos == ParentPosition.Linear)
                {
                    ForkNode p = null;
                    while (n.ParentPos != ParentPosition.None && n.ParentPos != ParentPosition.Left)
                    {
                        n = n.Prev;

                    }
                    if (n.ParentPos == ParentPosition.Left)
                    {
                        p = n.Prev as ForkNode;
                        p.NextLeft = null;

                        n = p.NextRight;
                        continue;
                    }
                    else if (n.ParentPos == ParentPosition.None)
                    {
                        isEnd = true;
                    }

                    continue;
                }

            }

            if (n is LinearNode linear)
            {
                if (grid[n.Pos.row + 1, n.Pos.col].Equals('.'))
                {
                    Position pos = new Position(n.Pos.row + 1, n.Pos.col);
                    LinearNode ln = new LinearNode(n, pos, ParentPosition.Linear);
                    ln.Depth = n.Depth + 1;
                    linear.Next = ln;
                    n = ln;
                }
                else if (grid[n.Pos.row + 1, n.Pos.col].Equals('^'))
                {
                    Position pos = new Position(n.Pos.row + 1, n.Pos.col);
                    ForkNode fn = new ForkNode(linear, pos, ParentPosition.Linear);
                    fn.Depth = n.Depth + 1;
                    linear.Next = fn;
                    n = fn;

                }

            }

            else if (n is ForkNode fork)
            {
                //left
                if (grid[n.Pos.row + 1, n.Pos.col - 1].Equals('.'))
                {
                    Position pos = new Position(n.Pos.row + 1, n.Pos.col - 1);
                    LinearNode ln1 = new LinearNode(n, pos, ParentPosition.Left);
                    ln1.Depth = n.Depth + 1;
                    fork.NextLeft = ln1;
                }

                //right
                if (grid[n.Pos.row + 1, n.Pos.col + 1].Equals('.'))
                {
                    Position pos = new Position(n.Pos.row + 1, n.Pos.col + 1);
                    LinearNode ln2 = new LinearNode(n, pos, ParentPosition.Right);
                    ln2.Depth = n.Depth + 1;
                    fork.NextRight = ln2;
                }

                if (fork.NextLeft != null)
                {
                    n = fork.NextLeft;
                }
            }
        }
        Console.WriteLine("Counter: {0}", counter);
    }

}