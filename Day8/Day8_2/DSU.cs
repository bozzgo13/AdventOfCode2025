using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day8_2
{

    /// <summary>
    /// Disjoint Set Union (DSU) / Union-Find structure
    /// This is used to track which junction boxes belong to which circuit.
    /// </summary>
    class DSU
    {
        private int[] parent;
        private int[] size;
        public int NumComponents { get; private set; }

        public DSU(int n)
        {
            parent = new int[n];
            size = new int[n];
            NumComponents = n; // Initially, every node is its own separate circuit
            for (int i = 0; i < n; i++)
            {
                parent[i] = i;
                size[i] = 1;
            }
        }

        // Find root with path compression
        public int Find(int i)
        {
            if (parent[i] == i) return i;
            return parent[i] = Find(parent[i]);
        }

        // Union of two sets, returns true if a new connection was actually made
        public bool Union(int i, int j)
        {
            int rootI = Find(i);
            int rootJ = Find(j);

            if (rootI != rootJ)
            {
                // Join the smaller group to the larger one
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
                NumComponents--; // One less separate circuit
                return true;
            }
            return false; // Already in the same circuit
        }
    }
}
