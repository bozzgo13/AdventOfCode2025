using System.IO;
using System;


string filePath = "input.txt"; //correct password for this input is 6175
//string filePath = "test.txt"; //correct password for this input is 6
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

            int startingPosition = dialPosition;

            if (firstChar == 'L')
            {
                if (startingPosition == 0)
                {
                    // Case 1: L from 0. Hits 0 at clicks 100, 200, ...
                    password += parsedNumber / 100;
                }
                else if (parsedNumber >= startingPosition)
                {
                    // Case 2: L from S != 0. Hits 0 at clicks S, S+100, S+200, ...
                    // Total crossings = floor((D - S) / 100) + 1
                    password += (parsedNumber - startingPosition) / 100 + 1;
                }

                // Calculate the final position
                dialPosition = (dialPosition - parsedNumber) % 100;
                if (dialPosition < 0) { dialPosition += 100; }
            }
            else
            {
                // Right Rotation (R): Hits 0 at 100-S, 200-S, ...
                // Total crossings = floor((D + S) / 100)
                password += (startingPosition + parsedNumber) / 100;

                // Calculate the final position
                dialPosition = (dialPosition + parsedNumber) % 100;
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

