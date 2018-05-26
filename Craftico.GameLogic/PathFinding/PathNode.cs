using System.Collections.Generic;

using NuciXNA.Primitives;

namespace Craftico.GameLogic.PathFinding
{
    public class PathNode
    {
        int _heuristic;
        bool _impassable;

        public PathNode Parent { get; set; }

        public List<PathNode> Neighbours { get; set; }

        public Point2D Location { get; set; }

        public int MoveCost { get; }

        public int G { get; set; }

        public bool IsImpassable
        {
            get { return MoveCost <= 0 || _impassable; }
            set
            {
                _impassable = value;
            }
        }

        public int Heuristic
        {
            get { return _heuristic; }
            set
            {
                _heuristic = value;
                G = MoveCost;
                Parent = null;
            }
        }

        public PathNode(int x, int y, int moveCost)
        {
            Location = new Point2D(x, y);

            Neighbours = new List<PathNode>();
            MoveCost = moveCost;
            G = moveCost;
            IsImpassable = false;
        }

        public int f() => Heuristic + MoveCost;

        public PathNode AddParents(Path path)
        {
            if (Parent != null)
            {
                path.Add(Parent.AddParents(path));
            }

            return this;
        }
    }
}
