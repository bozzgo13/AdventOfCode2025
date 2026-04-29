namespace Day9_2
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

            // Parse the Input
            // Red tiles are given as X,Y. These define the "vertices" of our loop.
            // According to instructions, they are connected in the order they appear.
            var redTiles = File.ReadAllLines(filePath)
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(line =>
                {
                    var p = line.Split(',');
                    return new Tile(int.Parse(p[0]), int.Parse(p[1]));
                })
                .ToList();

            long maxArea = 0;
            int n = redTiles.Count;

            Console.WriteLine($"Processing {n} red tiles...");

            // Iterate Through All Pairs of Red Tiles
            // We pick two red tiles to act as opposite corners of a potential rectangle.
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    Tile t1 = redTiles[i];
                    Tile t2 = redTiles[j];

                    // Calculate the boundaries of the rectangle formed by these two tiles
                    int minX = Math.Min(t1.X, t2.X);
                    int maxX = Math.Max(t1.X, t2.X);
                    int minY = Math.Min(t1.Y, t2.Y);
                    int maxY = Math.Max(t1.Y, t2.Y);

                    // Calculate current rectangle area: (width * height)
                    // We add +1 because coordinates represent tiles, not just infinitesimal points.
                    long width = maxX - minX + 1;
                    long height = maxY - minY + 1;
                    long currentArea = width * height;

                    // Optimization: If this rectangle is already smaller than our best find, skip the expensive check.
                    if (currentArea <= maxArea) continue;

                    // Validate the Rectangle
                    // We must ensure that *every* tile inside this rectangle is either Red or Green.
                    // "Red or Green" means any tile that is on the boundary OR inside the loop.
                    if (IsRectangleValid(minX, maxX, minY, maxY, redTiles))
                    {
                        maxArea = currentArea;
                    }
                }
            }

            Console.WriteLine($"Result: {maxArea}");

        }
        /// <summary>
        /// Checks if a rectangle is fully contained within the area defined by the red tile loop.
        /// </summary>
        static bool IsRectangleValid(int minX, int maxX, int minY, int maxY, List<Tile> polygon)
        {
            // Check all 4 corners using the "center of tile" logic
            // Stepping 0.5 into the tile ensures we're clearly inside, not stuck on the fence.
            if (!IsPointInOrOnPolygon(minX + 0.5, minY + 0.5, polygon)) return false;
            if (!IsPointInOrOnPolygon(maxX - 0.5, minY + 0.5, polygon)) return false;
            if (!IsPointInOrOnPolygon(minX + 0.5, maxY - 0.5, polygon)) return false;
            if (!IsPointInOrOnPolygon(maxX - 0.5, maxY - 0.5, polygon)) return false;

            // Ensure no polygon edge passes through the rectangle.
            // If an edge of the red/green loop is inside our rectangle, 
            // it means the rectangle is crossing over a hole.
            for (int i = 0, j = polygon.Count - 1; i < polygon.Count; j = i++)
            {
                Tile p1 = polygon[i];
                Tile p2 = polygon[j];

                // Find the bounding box of this polygon edge
                int edgeMinX = Math.Min(p1.X, p2.X);
                int edgeMaxX = Math.Max(p1.X, p2.X);
                int edgeMinY = Math.Min(p1.Y, p2.Y);
                int edgeMaxY = Math.Max(p1.Y, p2.Y);

                // Does this edge enter the inside of our rectangle?
                // We check if the edge is partially or fully inside the rectangle's bounds,
                // but it's okay if the edge IS the boundary of our rectangle.
                bool isInsideX = edgeMinX < maxX && edgeMaxX > minX;
                bool isInsideY = edgeMinY < maxY && edgeMaxY > minY;

                if (isInsideX && isInsideY)
                {
                    // If the edge is vertical and inside the X-range
                    if (p1.X == p2.X && p1.X > minX && p1.X < maxX) return false;
                    // If the edge is horizontal and inside the Y-range
                    if (p1.Y == p2.Y && p1.Y > minY && p1.Y < maxY) return false;
                }
            }

            // One last check for the very middle
            if (!IsPointInOrOnPolygon(minX + (maxX - minX) / 2.0, minY + (maxY - minY) / 2.0, polygon)) return false;

            return true;
        }

        /// <summary>
        /// Implements the Ray Casting algorithm to determine if a point (x, y) is inside the polygon.
        /// Also handles points exactly on the boundary (Edges/Vertices).
        /// </summary>
        static bool IsPointInOrOnPolygon(double x, double y, List<Tile> poly)
        {
            bool isInside = false;
            int n = poly.Count;

            for (int i = 0, j = n - 1; i < n; j = i++)
            {
                Tile pointI = poly[i];
                Tile pointJ = poly[j];

                // Boundary Check: Check if the point lies exactly on an orthogonal edge
                // Vertical edges
                if (
                    pointI.X == pointJ.X
                    && pointI.X == x
                    && y >= Math.Min(pointI.Y, pointJ.Y)
                    && y <= Math.Max(pointI.Y, pointJ.Y))
                    return true;
                // Horizontal edges
                if (
                    pointI.Y == pointJ.Y
                    && pointI.Y == y
                    && x >= Math.Min(pointI.X, pointJ.X)
                    && x <= Math.Max(pointI.X, pointJ.X))
                    return true;

                // Ray Casting Logic:
                // If we cast a ray to the right, how many polygon edges does it cross?
                // If the number of crossings is odd, the point is inside.
                if (
                    (pointI.Y > y) != (pointJ.Y > y) // to prevent division by zero in case of horizontal edges
                    &&
                    (x < (double)(pointJ.X - pointI.X) * (y - pointI.Y) / (pointJ.Y - pointI.Y) + pointI.X)
                )
                {
                    isInside = !isInside;
                }
            }
            return isInside;
        }
    }
}