using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;
namespace Day8_1
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

            // 1. Load and parse the data
            var lines = File.ReadAllLines(filePath);
            Point3D[] points = lines
                .Select(line => line.Split(','))
                .Select(p => new Point3D(long.Parse(p[0]), long.Parse(p[1]), long.Parse(p[2])))
                .ToArray();

            int n = points.Length;
            Console.WriteLine($"Loaded {n} junction boxes.");

            // 2. Calculate distances for all possible pairs
            // We use Distance Squared to avoid expensive Square Root (Math.Sqrt) operations.
            var connections = new List<(long distSq, int p1, int p2)>();
            Stopwatch sw = Stopwatch.StartNew();

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    long dx = points[i].X - points[j].X;
                    long dy = points[i].Y - points[j].Y;
                    long dz = points[i].Z - points[j].Z;
                    long dSq = dx * dx + dy * dy + dz * dz;

                    connections.Add((dSq, i, j));
                }
            }

            // 3. Sort connections by distance (shortest first)
            var sortedConnections = connections.OrderBy(c => c.distSq).ToList();

            // 4. Process the 1000 shortest connections
            DSU dsu = new DSU(n);
            int limit = Math.Min(1000, sortedConnections.Count);

            for (int i = 0; i < limit; i++)
            {
                dsu.Union(sortedConnections[i].p1, sortedConnections[i].p2);
            }

            // 5. Get circuit sizes and find the three largest
            var finalSizes = dsu.GetAllSizes().OrderByDescending(s => s).ToList();

            sw.Stop();

            // Calculate final result (product of top 3)
            if (finalSizes.Count >= 3)
            {
                long result = (long)finalSizes[0] * finalSizes[1] * finalSizes[2];

                Console.WriteLine("\n--- Results ---");
                Console.WriteLine($"Top 3 circuit sizes: {finalSizes[0]}, {finalSizes[1]}, {finalSizes[2]}");
                Console.WriteLine($"Final Product: {result}");
            }
            else
            {
                Console.WriteLine("Not enough circuits found to multiply the top 3.");
            }

            Console.WriteLine($"\nExecution time: {sw.ElapsedMilliseconds} ms");
        }
    }
}