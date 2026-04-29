using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class Program
{
    public static void Main()
    {
        string[] fileConten = File.ReadAllLines("input.txt");

        long totalPresses = 0;

        foreach (var line in fileConten)
        {
            // Example input lines:
            // [.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}
            // [...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}
            // [.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}
            // The manual describes one machine per line
            // Each line contains:
            // - a single indicator light diagram in [square brackets]
            // - one or more button wiring schematics in (parentheses)
            // - joltage requirements in {curly braces}.


            // Parsing light states
            var lightMatch = Regex.Match(line, @"\[([.#]+)\]");
            int targetState = 0;
            string lights = lightMatch.Groups[1].Value;
            for (int i = 0; i < lights.Length; i++)
            {
                if (lights[i] == '#') targetState |= (1 << i);
            }

            // Parsing buttons
            var buttonMatches = Regex.Matches(line, @"\(([\d,]+)\)");
            List<int> buttons = new List<int>();
            foreach (Match m in buttonMatches)
            {
                int buttonMask = 0;
                var indices = m.Groups[1].Value.Split(',').Select(int.Parse);
                foreach (int idx in indices) buttonMask |= (1 << idx);
                buttons.Add(buttonMask);
            }

            // BFS for finding minimum presses
            totalPresses += FindMinPresses(targetState, buttons);
        }

        Console.WriteLine($"Total fewest presses: {totalPresses}");
    }

    /// <summary>
    /// Finds the minimum number of button presses required to reach the target configuration 
    /// using a Breadth-First Search (BFS) algorithm.
    /// </summary>
    /// <param name="target">The bitmask representing the desired state of indicator lights.</param>
    /// <param name="buttons">A list of bitmasks, where each mask represents the lights toggled by a specific button.</param>
    /// <returns>The fewest total presses required to reach the target state.</returns>
    static int FindMinPresses(int target, List<int> buttons)
    {
        // Queue stores pairs of (current_light_state, total_presses_made)
        Queue<(int state, int count)> queue = new Queue<(int, int)>();

        // HashSet keeps track of already visited states to avoid redundant calculations and infinite loops
        HashSet<int> visited = new HashSet<int>();

        // Start the search from the initial state (all lights off = 0) with 0 presses
        queue.Enqueue((0, 0));
        visited.Add(0);

        while (queue.Count > 0)
        {
            // Dequeue the next state to explore (BFS ensures we explore by depth: 1 press, then 2, etc.)
            var (currentState, count) = queue.Dequeue();

            // If the current configuration matches the target, we've found the shortest path
            if (currentState == target)
            {
                return count;
            }

            // Try pressing every available button from the current state
            foreach (var button in buttons)
            {
                // Use the XOR operator (^) to toggle the lights according to the button's wiring
                int nextState = currentState ^ button;

                // If this new light configuration hasn't been seen before, add it to the queue
                if (!visited.Contains(nextState))
                {
                    visited.Add(nextState);
                    queue.Enqueue((nextState, count + 1));
                }
            }
        }

        // Return 0 if the target configuration is unreachable with the given buttons
        return 0;
    }
}