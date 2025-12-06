
using System.Numerics;

internal class Program
{
    private static void Main(string[] args)
    {

        //string inputFileName = "input.txt"; // 840 ingredient IDs are fresh
        string inputFileName = "test.txt"; // 3 ingredient IDs are fresh
        var data= File.ReadAllText(inputFileName);
        var parts = data.Split(new string[] { "\n\n", "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        var freshIdRanges = 
            parts[0]
            .Split("\n")
            .Select(x => x.Split("-"))
            .Select(x => new Tuple<BigInteger, BigInteger>(BigInteger.Parse(x[0]), BigInteger.Parse(x[1])))
            .ToList();
        
        var availableIngredientIds = 
            parts[1]
            .Split("\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => BigInteger.Parse(x));

        int numberOfFreshIds = 0;

        foreach (var ingredientId in availableIngredientIds)
        {
            foreach (var range in freshIdRanges)
            {
                if(ingredientId >= range.Item1 && ingredientId <= range.Item2) 
                {
                    numberOfFreshIds++;
                    break;
                }
            }
        }


        Console.WriteLine("{0} ingredient IDs are fresh", numberOfFreshIds);
    }

}