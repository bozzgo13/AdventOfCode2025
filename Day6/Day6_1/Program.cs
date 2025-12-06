
using System.Numerics;

internal class Program
{
    private static void Main(string[] args)
    {

        string inputFileName = "input.txt"; // Grand total found = 4405895212738

        var data = File.ReadAllLines(inputFileName);

        var operations = data[data.Length-1].Split(" ", StringSplitOptions.RemoveEmptyEntries);

        List<List<int>> numbersList = new List<List<int>>();
        for (int i = 0; i < data.Length - 1; i++) //exclude last line with operations 
        {
            string? line = data[i];
            var numbers = line
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(x=> int.Parse(x))
                .ToArray();

            for (int j = 0; j < numbers.Length; j++)
            {
                if (i == 0)
                {
                    numbersList.Add(new List<int>());
                }

                numbersList[j].Add(numbers[j]);
            }
        }

        BigInteger result = BigInteger.Zero;
        BigInteger partialResult = BigInteger.Zero;

        for (int k = 0; k < numbersList.Count; k++)
        {
            var x = numbersList[k];

            for (int l = 0; l < x.Count; l++)
            {

                if (l > 0)
                {
                    Console.Write(" " + operations[k] + " ");
                }
                Console.Write(x[l]);

                if (l==0)
                {
                    partialResult = x[l];
                }
                else
                {
                    switch (operations[k])
                    {
                        case "*":
                            partialResult *= x[l];
                            break;
                        case "+":
                            partialResult += x[l];
                            break;
                        default:
                            break;
                    }
                }                    
            }

            Console.WriteLine(" = {0}", partialResult);
            result += partialResult;

        }
        Console.WriteLine("\r\nGrand total found = {0}", result);
    }

}