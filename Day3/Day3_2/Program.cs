using System;
using System.IO;
using System.Text;
using System.Numerics;

public class AoC2025Day3Task2
{
    // Required output length for the joltage number (12 digits)
    private const int RequiredLength = 12;

    private static void Main(string[] args)
    {
        // Use BigInteger to store the total sum, as the result exceeds the capacity of a standard 'long'
        BigInteger joltageSum = 0;

        // Input file
        string filePath = "input.txt";

        // Check if the file exists
        if (File.Exists(filePath))
        {
            try
            {
                // Read the file line by line. Each battery bank is stored in its own line
                foreach (string line in File.ReadLines(filePath))
                {
                    // Call the greedy function to find the largest 12-digit number for the current bank
                    string maxJoltageStr = FindMaxTwelveDigitJoltage(line);

                    // Convert the resulting string to BigInteger. Using long would cause an overflow
                    // Add the maximum joltage from the current bank to the total sum
                    joltageSum += BigInteger.Parse(maxJoltageStr);
                }
            }
            catch (Exception ex)
            {
                // Handle potential file reading or processing exceptions
                Console.WriteLine("Error reading file or processing: " + ex.Message);
            }

            // Output the total joltage sum
            Console.WriteLine("The total output joltage is {0}", joltageSum);
        }
        else
        {
            // Inform the user if the input file was not found
            Console.WriteLine($"File not found: {filePath}");
        }
    }

    /// <summary>
    /// Implements an iterative greedy algorithm to find the largest 12-digit number 
    /// that can be formed by picking digits in their original order.
    /// 
    /// The goal is to maximize the final 12-digit number. Since the value of a digit is 
    /// determined by its positional weight, the most significant digits (the ones 
    /// furthest to the left) have the largest impact on the total magnitude. 
    /// 
    /// The greedy strategy ensures optimality by:
    /// 1. Maximizing the most significant digit first.
    /// 2. Restricting the search window to only those digits that leave exactly 
    /// enough remaining digits to complete the required 12-digit length.
    ///
    /// This guarantees that the locally optimal choice at each step leads to the 
    /// globally maximum number.
    /// </summary>
    /// <param name="bank">The input string of digits (battery bank).</param>
    /// <returns>A string representing the maximum 12-digit number.</returns>
    private static string FindMaxTwelveDigitJoltage(string bank)
    {
        // Safety check: if the bank is shorter than 12 digits, return 0 
        // (this really shouldn't happen)
        if (bank.Length < RequiredLength)
        {
            return "0";
        }

        // StringBuilder holds the resulting 12-digit number as we build it greedily
        StringBuilder result = new StringBuilder();
        // Index in the bank where we start searching for the current digit
        int currentStartIndex = 0;

        // Loop 12 times to determine each of the 12 digits of the final number
        for (int digitIndex = 0; digitIndex < RequiredLength; digitIndex++)
        {
            // Calculate how many digits we still need to select
            int digitsNeeded = RequiredLength - digitIndex;

            // Determine the maximum index we can search up to in the 'bank' string.
            // This ensures we leave enough remaining digits after the chosen digit
            // to complete the RequiredLength sequence.
            int maxSearchLength = bank.Length - digitsNeeded;

            // Safety check (should rarely be needed if bank.Length >= RequiredLength)
            if (currentStartIndex > maxSearchLength)
            {
                break;
            }

            // Initialize variables to track the best (largest) digit and its position
            char maxDigit = '0';
            int bestDigitIndex = -1;

            // Iterates through the valid search window (from currentStartIndex to maxSearchLength)
            for (int i = currentStartIndex; i <= maxSearchLength; i++)
            {
                // Greedy choice: If the current digit is larger than the largest found so far...
                if (bank[i] > maxDigit)
                {
                    // ...update the maximum digit and its index (position)
                    maxDigit = bank[i];
                    bestDigitIndex = i;
                }

                // Optimization: '9' is the largest possible digit. If found, we can immediately 
                // stop searching this window, as no better choice exists.
                if (maxDigit == '9')
                {
                    break;
                }
            }

            // Safety break if a digit wasn't found
            // (should not happen in valid input)
            if (bestDigitIndex == -1) break;

            // Append the selected largest digit to the result string
            result.Append(maxDigit);

            // Update the starting index for the next iteration: the search for the next digit 
            currentStartIndex = bestDigitIndex + 1;
        }

        return result.ToString();
    }
}