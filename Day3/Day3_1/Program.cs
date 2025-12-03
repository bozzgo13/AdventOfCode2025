
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        string filePath = "input.txt"; //The total output joltage is 17427
        //string filePath = "test.txt"; //The total output joltage is 357

        int joltageSum = 0;
        if (File.Exists(filePath))
        {
            try
            {
                foreach (string line in File.ReadLines(filePath))
                {
                    string firstSubstring = line.Substring(0, line.Length - 1);
                    string firstDigitStr = FindMaxDigit(firstSubstring);

                    int firstDigit = int.Parse(firstDigitStr);

                    int index = line.IndexOf(firstDigitStr);
                    string secondSubstring = line.Substring(index + 1);
                    string secondDigitStr = FindMaxDigit(secondSubstring);

                    int secondDigit = int.Parse(secondDigitStr);

                    int joltage = firstDigit * 10 + secondDigit;
                    joltageSum += joltage;

                    //Console.Write(firstDigit);
                    //Console.WriteLine(secondDigit);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("The total output joltage is {0}", joltageSum);
        }
    }

    private static string FindMaxDigit(string v)
    {
        for (int i = 9; i>0;i--)
        {
            if (v.IndexOf(i.ToString())>-1)
            {
                return i.ToString();
            }
        }

        return "0";
    }
}