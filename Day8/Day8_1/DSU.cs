using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day8_1
{

    /// <summary>
    /// Disjoint Set Union (DSU) / Union-Find structure
    /// This is used to track which junction boxes belong to which circuit.
    /// </summary>
    class DSU
    {
        private int[] parent;
        private int[] size;

        public DSU(int n)
        {
            parent = new int[n];
            size = new int[n];
            for (int i = 0; i < n; i++)
            {
                parent[i] = i; // Initially, every node is its own parent
                size[i] = 1;   // Initially, every circuit has a size of 1
            }
        }

        // Find the "root" of the circuit with Path Compression
        public int Find(int i)
        {
            // If i itself is root
            if (parent[i] == i) return i;
            // else recursively finds root and flattens structure
            return parent[i] = Find(parent[i]);
        }

        // Connect two circuits together (Union by Size)
        public void Union(int i, int j)
        {
            int rootI = Find(i);
            int rootJ = Find(j);

            if (rootI != rootJ)
            {
                // Attach the smaller circuit to the larger one to keep the tree flat
                if (size[rootI] < size[rootJ])
                {
                    parent[rootI] = rootJ;
                    size[rootJ] += size[rootI];
                }
                else
                {
                    parent[rootJ] = rootI;
                    size[rootI] += size[rootJ];
                }
            }
        }

        // Returns all circuit sizes for analysis
        public List<int> GetAllSizes()
        {
            List<int> sizes = new List<int>();
            for (int i = 0; i < parent.Length; i++)
            {
                if (parent[i] == i) // Only roots represent a full circuit
                {
                    sizes.Add(size[i]);
                }
            }
            return sizes;
        }
    }
}
