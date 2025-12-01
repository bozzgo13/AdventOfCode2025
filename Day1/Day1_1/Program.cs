using System.IO;
using System;


string filePath = "input.txt"; //correct password for this input is 1102
//string filePath = "test.txt"; //correct password for this input is 3
int password = 0;
int dialPosition = 50; //starting position is 50

if (File.Exists(filePath))
{
    try
    {
        foreach (string line in File.ReadLines(filePath))
        {
            char firstChar = line[0];
            if (firstChar != 'L' && firstChar != 'R')
            {
                throw new Exception("Error, Unexpected rotation sign input");
            }

            string numberString = line.Substring(1);
            int parsedNumber;
            bool parsingOk = int.TryParse(numberString, out parsedNumber);

            if (!parsingOk)
            {
                throw new Exception("Error, Unexpected number input");
            }

            while (parsedNumber >= 100)
            {
                parsedNumber -= 100;
            }

            if (firstChar == 'L')
            {
                dialPosition -= parsedNumber;
                if (dialPosition < 0) { dialPosition += 100; }
            }
            else
            {
                dialPosition += parsedNumber;
                if (dialPosition > 99) { dialPosition -= 100; }
            }

            if (dialPosition == 0)
            {
                password++;
            }
        }
    }
    catch (IOException ex)
    {
        Console.WriteLine($"IOException: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"General exception: {ex.Message}");
    }
}
else
{
    Console.WriteLine($"File dosn't exists: {filePath}");

}

Console.WriteLine("Pasword is {0}", password);

