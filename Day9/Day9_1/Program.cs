namespace Day9_1
{
    class Program
    {
        static void Main()
        {
            const string filePath = "input.txt";
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Error: input.txt not found.");
                return;
            }

            // Read and parse the input data
            // Each line contains X,Y coordinates of a red tile
            var redTiles = File.ReadAllLines(filePath)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line =>
                {
                    var parts = line.Split(',');
                    return new Tile(int.Parse(parts[0]), int.Parse(parts[1]));
                })
                .ToList();

            long maxArea = 0;
            int tileCount = redTiles.Count;

            Console.WriteLine($"Found {tileCount} red tiles.");

            // Compare every pair of red tiles as opposite corners
            // Using a double loop to check all possible combinations (O(n^2))
            for (int i = 0; i < tileCount; i++)
            {
                for (int j = i + 1; j < tileCount; j++)
                {
                    Tile t1 = redTiles[i];
                    Tile t2 = redTiles[j];

                    // Calculate width and height
                    // We take the absolute difference and add 1 to include both boundary tiles
                    long width = Math.Abs(t1.X - t2.X) + 1;
                    long height = Math.Abs(t1.Y - t2.Y) + 1;

                    long area = width * height;

                    // Keep track of the largest area found
                    maxArea = Math.Max(maxArea, area);
                }
            }

            // Result
            Console.WriteLine($"The largest rectangle area is: {maxArea}");

        }
    }
}