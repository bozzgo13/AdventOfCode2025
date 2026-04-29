using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day9_2
{
    /// <summary>
    /// Tile position on the grid as struct
    /// </summary>
    public struct Tile
    {
        public int X, Y;
        public Tile(int x, int y) { X = x; Y = y; }
    }
}
