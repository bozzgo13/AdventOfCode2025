
using System.Collections.Generic;
using System.Numerics;

internal class Program
{
    private static void Main(string[] args)
    {

        string inputFileName = "input.txt"; // total of 359913027576322 ingredient IDs to be fresh
        //string inputFileName = "test.txt"; // total of 14 ingredient IDs to be fresh
        var data = File.ReadAllText(inputFileName);
        var parts = data.Split(new string[] { "\n\n", "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        var freshIdRanges =
            parts[0]
            .Split("\n")
            .Select(x => x.Split("-"))
            .Select(x => new Tuple<BigInteger, BigInteger>(BigInteger.Parse(x[0]), BigInteger.Parse(x[1])))
            .ToList();

        BigInteger ingredientIDsConsideredFreshCount = BigInteger.Zero;
        List<Tuple<BigInteger, BigInteger>> megedIdRanges = MergeOverlappingIntervals(freshIdRanges);

        foreach (var range in megedIdRanges)
        {
            ingredientIDsConsideredFreshCount += range.Item2 - range.Item1+1; //+1 because of inclusion
        }

        Console.WriteLine("According to the fresh ingredient ID ranges there are {0} ingrediants considered to be fresh.", ingredientIDsConsideredFreshCount);
    }



    /// <summary>
    /// Merges all overlapping or adjacent intervals in the given list.
    /// Intervals are inclusive (start and end are included)
    /// </summary>
    /// <param name="intervals">The list of intervals as Tuple<BigInteger, BigInteger> 
    /// where Item1 is inclusive start and Item2 is inclusive end.</param>
    /// <returns>A new list of merged, non-overlapping intervals.</returns>
    public static List<Tuple<BigInteger, BigInteger>> MergeOverlappingIntervals(
        List<Tuple<BigInteger, BigInteger>> intervals)
    {
        
        if (intervals == null || intervals.Count == 0)
        {
            //this should not happen unless we have corrupt input data
            return new List<Tuple<BigInteger, BigInteger>>();
        }

        // Sorting the intervals
        // Sort by the start point
        var sortedIntervals = intervals
            .OrderBy(i => i.Item1)
            .ThenBy(i => i.Item2)
            .ToList();

        // Initialization and iterative merging
        var mergedIntervals = new List<Tuple<BigInteger, BigInteger>>();

        // Start merging with the first interval.
        BigInteger currentStart = sortedIntervals[0].Item1;
        BigInteger currentEnd = sortedIntervals[0].Item2;

        for (int i = 1; i < sortedIntervals.Count; i++)
        {
            BigInteger nextStart = sortedIntervals[i].Item1;
            BigInteger nextEnd = sortedIntervals[i].Item2;

            // Checking for overlap or adjacency:
            // Two inclusive intervals [a, b] and [c, d] are adjacent/overlapping if c <= b+1.
            if (nextStart <= currentEnd + 1)
            {
                // Merge: Update the end of the current merged interval to the maximum end value
                currentEnd = BigInteger.Max(currentEnd, nextEnd);
            }
            else
            {
                // No overlap/adjacency. We can complete current interval (because we sorted intervals on begining)
                mergedIntervals.Add(new Tuple<BigInteger, BigInteger>(currentStart, currentEnd));

                // Start a new merged interval
                currentStart = nextStart;
                currentEnd = nextEnd;
            }
        }

        // Adding the last merged interval
        mergedIntervals.Add(new Tuple<BigInteger, BigInteger>(currentStart, currentEnd));

        return mergedIntervals;
    }

}