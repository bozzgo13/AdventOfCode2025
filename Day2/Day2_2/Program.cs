
string inputFileName = "input.txt"; //result for my input is  4174379265
string inputRanges = File.ReadAllText(inputFileName);

// to store the cumulative sum of all invalid IDs.
// use long, as numbers can exceed 32 bits
long sumOfInvalidIDs = 0;

// Expected input "11-22,95-115,998-1012,..."
// Split the input string by comma (',') to get individual ranges.
var ranges = inputRanges.Split(',');


foreach (var range in ranges)
{
    // Split the range string by the dash ('-') to separate the start Id and end ID.
    var parts = range.Split('-');

    // Check if valid numbers
    if (long.TryParse(parts[0], out long startId) && long.TryParse(parts[1], out long endId))
    {
        // Go through every single ID from the start ID to the end ID
        for (long id = startId; id <= endId; id++)
        {
            string idStr = id.ToString();
            // Get the total length of the ID string. 
            int totalLength = idStr.Length;

            bool isInvalidId = false;

            // ID must have at least 2 digits to be a repeated pattern (e.g., 11).
            if (totalLength >= 2)
            {
                // Patern length can be from 1 to half the total length (pattern must repeat at least twice) 
                for (int patternLength = 1; patternLength <= totalLength / 2; patternLength++)
                {
                    // the length of the pattern must be a multiple of the length of all digits
                    // for example: if we are checking pattern length 2, but number of digits is 5, we can't repeat patern
                    if (totalLength % patternLength == 0)
                    {
                        // base patern
                        string pattern = idStr.Substring(0, patternLength);

                        // Number of times the pattern must repeat.
                        int repeatCount = totalLength / patternLength;

                        // Construct expectedId using the pattern and expected length
                        string expectedId = string.Empty;
                        for (int k = 0; k < repeatCount; k++)
                        {
                            expectedId += pattern;
                        }

                        // Compare the constructed ID with the original string ID.
                        if (idStr.Equals(expectedId))
                        {
                            // If they match, given ID is invalid
                            // and we don't need to check other paterns for this ID
                            isInvalidId = true;
                            break;
                        }
                    }
                }
            }

            // If an invalid ID is found, sum its value with the cumulative total
            if (isInvalidId)
            {
                sumOfInvalidIDs += id;
            }
        }
    }
}

Console.WriteLine($"The sum of invalid IDs is: {sumOfInvalidIDs}");
