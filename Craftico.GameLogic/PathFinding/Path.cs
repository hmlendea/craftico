using System.Collections.Generic;
using System.Linq;

namespace Craftico.GameLogic.PathFinding
{
    public class Path
    {
        List<PathNode> nodes { get; set; }

        public Path()
        {
            nodes = new List<PathNode>();
        }

        public PathNode FirstNode => NodeAt(0);

        public PathNode NodeAt(int index)
        {
            return nodes[index];
        }

        public void Add(PathNode node)
        {
            nodes.Add(node);
        }

        public void RemoveAt(int index)
        {
            nodes.RemoveAt(index);
        }

        public int Length => nodes.Count();

        public int Cost => nodes.Sum(x => x.MoveCost);
    }
}
