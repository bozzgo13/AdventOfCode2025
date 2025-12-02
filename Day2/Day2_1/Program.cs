

string inputFileName = "input.txt"; //result for my input is 31210613313
string input = File.ReadAllText(inputFileName);

// use long, as numbers can exceed 32 bits
long sumOfInvalidIDs = 0;

// ranges are separated by a comma
var ranges = input.Split(',');

foreach (var range in ranges)
{
    // range start and range end are separated with -
    var parts = range.Split('-');
    if (parts.Length != 2)
    {
        // this would theoretically only happen if the input was corrupt.
        continue;
    }
    // parsing to long
    if (long.TryParse(parts[0], out long startId) && long.TryParse(parts[1], out long endId))
    {
        // Check every Id if it's invalid
        for (long id = startId; id <= endId; id++)
        {
            string idAsString = id.ToString();
            int length = idAsString.Length;

            // Invalid ID must have an even number of digits 
            if (length % 2 != 0)
            {
                continue; // this can't be invalid number. 
            }

            int halfLength = length / 2;

            // first half of string
            string firstHalf = idAsString.Substring(0, halfLength);

            // second half of string
            string secondHalf = idAsString.Substring(halfLength);

            // Id is invalid if halfs are equal examples 6464, 11, 164164
            if(firstHalf.Equals(secondHalf))
            {
                sumOfInvalidIDs += id;
            }

        }
    }
}

Console.WriteLine($"Sum of invalid Ids: {sumOfInvalidIDs}");
