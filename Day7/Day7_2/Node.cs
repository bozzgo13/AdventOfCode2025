using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7_2
{
    public abstract class Node
    {
        public Node? Prev { get; set; }
        public Position Pos { get; set; }
        public ParentPosition ParentPos { get; set; }

        public int Depth { get; set; }


        public Node(Node? prev, Position pos, ParentPosition parentPos)
        {
            Prev = prev;
            Pos = pos;
            ParentPos = parentPos;
        }
    }

    public class Position
    {

        public int row, col;

        public Position(int r, int c)
        {
            row = r; col = c;
        }
    }

    public class LinearNode : Node
    {
        public LinearNode(Node? prev, Position pos, ParentPosition parentPos) : base(prev, pos, parentPos)
        {
        }

        public Node? Next { get; set; }
    }

    public class ForkNode : Node
    {
        public ForkNode(Node? prev, Position pos, ParentPosition parentPos) : base(prev, pos, parentPos)
        {
        }

        public Node? NextLeft { get; set; }
        public Node? NextRight { get; set; }
    }

    public enum ParentPosition
    {
        Left,
        Right,
        Linear,
        None
    }
}
