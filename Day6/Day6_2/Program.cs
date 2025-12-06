
using System.Numerics;

internal class Program
{
    private static void Main(string[] args)
    {

        string inputFileName = "input.txt"; // Grand total found = 7450962489289

        var data = File.ReadAllLines(inputFileName);

        var lastLine = data[data.Length - 1];
        var operations = lastLine.Split(" ", StringSplitOptions.RemoveEmptyEntries);

        List<List<int>> numbersList = new List<List<int>>();
        List<int>? lst =null;
        char operatorx = ' ';
        for (int index = 0; index < lastLine.Length; index++)
        {
            char c = lastLine[index];//character on current position in last line
            //it is always left aligned, so it always shows when new calcuation is in place

            if(c != ' ')
            {
                if (lst != null)
                {
                    operatorx = c;
                    numbersList.Add(lst);
                }
                lst = new List<int>();
            }

            string numberAsStr = "";
            for (int i = 0; i < data.Length - 1; i++) //exclude last line with operations 
            {
                numberAsStr += data[i][index];
            }

            string numberAsStrTrimed = numberAsStr.TrimEnd(' ').TrimStart(' ');

            if (string.IsNullOrEmpty(numberAsStrTrimed))
                continue;

           lst?.Add(int.Parse(numberAsStrTrimed));
        }

        numbersList.Add(lst);

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

                if (l == 0)
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