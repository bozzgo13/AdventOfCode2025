using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;
namespace Day8_2
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

            // 1. Parsing input
            var lines = File.ReadAllLines(filePath);
            Point3D[] points = lines
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(line => line.Split(','))
                .Select(p => new Point3D(long.Parse(p[0]), long.Parse(p[1]), long.Parse(p[2])))
                .ToArray();

            int n = points.Length;
            Console.WriteLine($"Analyzing {n} junction boxes...");

            // 2. Pre-calculate all possible connections (Edges)
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

            // 3. Sort connections by distance (Kruskal's approach)
            var sortedConnections = connections.OrderBy(c => c.distSq).ToList();

            // 4. Connect until only one circuit remains
            DSU dsu = new DSU(n);
            long part2Result = 0;
            bool completed = false;

            foreach (var conn in sortedConnections)
            {
                // Try to connect the two boxes
                // Union returns true only if they were in different circuits
                if (dsu.Union(conn.p1, conn.p2))
                {
                    // Check if this was the final connection needed
                    if (dsu.NumComponents == 1)
                    {
                        // Calculate product of X coordinates of these two specific boxes
                        part2Result = points[conn.p1].X * points[conn.p2].X;

                        Console.WriteLine("\n--- Part Two Completed ---");
                        Console.WriteLine($"Last connection found between:");
                        Console.WriteLine($"Box A: Index {conn.p1}, X={points[conn.p1].X}");
                        Console.WriteLine($"Box B: Index {conn.p2}, X={points[conn.p2].X}");
                        Console.WriteLine($"Final Answer (X1 * X2): {part2Result}");

                        completed = true;
                        break;
                    }
                }
            }

            sw.Stop();
            if (!completed)
            {
                Console.WriteLine("Could not connect all boxes into a single circuit.");
            }

            Console.WriteLine($"\nProcessing time: {sw.ElapsedMilliseconds} ms");
        }
    }
}