
using System;
using System.Numerics;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {

        string inputFileName = "input.txt"; // Beam will be split 1635 times!

        var data = File.ReadAllLines(inputFileName);

        var firstLine = data[0];

        Console.WriteLine(firstLine);
        int startingPoint = firstLine.IndexOf("S");
        int beamSplitCount = 0;

        SortedSet<int> positions = new SortedSet<int>();
        positions.Add(startingPoint);

        for (int index = 1; index < data.Length; index++)
        {
            var nextLine = data[index];

            if (index % 2==0) //spliting 
            {
                StringBuilder sb = new StringBuilder(nextLine);

                List<int> removeIndexes = new List<int>();
                List<int> addIndexes = new List<int>();


                foreach (var item in positions)
                {
                    if (nextLine[item].Equals('^'))
                    {
                        beamSplitCount++;
                        removeIndexes.Add(item);
                        sb[item-1] = '|';
                        sb[item+1] = '|';
                        addIndexes.Add(item-1);
                        addIndexes.Add(item+1);
                    }
                    else
                    {
                        sb[item] = '|';
                    }
                }

                foreach (var item in removeIndexes)
                {
                    positions.Remove(item);
                }
                foreach (var item in addIndexes)
                {
                    positions.Add(item);
                }

                nextLine = sb.ToString();

            }
            else 
            {
                StringBuilder sb = new StringBuilder(nextLine);
                foreach (var item in positions)
                {
                    sb[item] = '|';
                }

                nextLine = sb.ToString();

            }
            Console.WriteLine(nextLine);
           
        }
        Console.WriteLine($"Beam will be split {beamSplitCount} times!");

    }

}