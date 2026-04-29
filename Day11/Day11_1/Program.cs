
namespace Day11_1
{
    public class Program
    {
        // Dictionary where keys are nodes and their values are lists of neighboring nodes.
        private static Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>();

        public static void Main(string[] args)
        {
            var fileConten = File.ReadAllLines("input.txt");

            foreach (var line in fileConten)
            {
                var parts = line.Split(": ");
                var key = parts[0];
                var values = parts[1].Split(' ').ToList();
                graph[key] = values;
            }

            string startNode = "you";
            string targetNode = "out";

            // Set to keep track of nodes in the current path to prevent infinite loops
            // (e.g., A → B → A).
            HashSet<string> visited = new HashSet<string>();
            int totalPaths = CountAllPaths(startNode, targetNode, visited);
            Console.WriteLine($"Number of distinct paths from {startNode} to {targetNode}: {totalPaths}");

        }


        /// <summary>
        /// Recursive method to find the total number of paths using Depth-First Search.
        /// </summary>
        private static int CountAllPaths(string current, string target, HashSet<string> visited)
        {
            // Base case: if we reached the target, we found 1 valid path
            if (current == target)
            {
                return 1;
            }

            int pathCount = 0;

            // Add current node to visited set before exploring neighbors
            visited.Add(current);

            if (graph.ContainsKey(current))
            {
                foreach (var neighbor in graph[current])
                {
                    // Only visit the neighbor if it's not already in the current path
                    if (!visited.Contains(neighbor))
                    {
                        pathCount += CountAllPaths(neighbor, target, visited);
                    }
                }
            }

            // Remove the node from visited so it can be used in other paths
            visited.Remove(current);

            return pathCount;
        }
    }
}