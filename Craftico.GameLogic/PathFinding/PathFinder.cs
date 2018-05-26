using System;
using System.Collections.Generic;
using System.Linq;

using NuciXNA.Primitives;

using Craftico.GameLogic.GameManagers;
using Craftico.Models;
using Craftico.Settings;

namespace Craftico.GameLogic.PathFinding
{
    /// <summary>
    /// A-Star path finder.
    /// </summary>
    public static class PathFinder
    {
        public static Path GetShortestPath(Point2D startLocation, Point2D endLocation, IWorldManager worldManager)
        {
            int size = Math.Max(
                Math.Abs(startLocation.X - endLocation.X),
                Math.Abs(startLocation.Y - endLocation.Y)) + GameDefines.WorldChunkSize;

            if (size > GameDefines.WorldChunkSize * 2)
            {
                return null;
            }

            List<PathNode> nodes = worldManager
                .GetChunkTilesAroundLocation(startLocation.X, startLocation.Y)
                .Select(x => new PathNode(x.WorldLocation.X, x.WorldLocation.Y, 1))
                .ToList();

            PathNode start = nodes.Single(x => x.Location.X == startLocation.X && x.Location.Y == startLocation.Y);
            PathNode end = nodes.Single(x => x.Location.X == endLocation.X && x.Location.Y == endLocation.Y);

            return GetShortestPath(start, end, nodes);
        }

        static Path GetShortestPath(PathNode start, PathNode end, List<PathNode> map)
        {
            Path path = new Path();

            List<PathNode> open = new List<PathNode>();
            List<PathNode> closed = new List<PathNode>();

            foreach (PathNode node in map)
            {
                node.Heuristic = Math.Abs(node.Location.X - end.Location.X) + Math.Abs(node.Location.Y - end.Location.Y);
            }

            open.Add(start);
            end.IsImpassable = false;

            while (open.Count > 0)
            {
                PathNode current = GetNextNode(open);
                open.Remove(current);
                closed.Add(current);

                foreach (PathNode neighbour in GetNeighbours(current, map))
                {
                    if (neighbour.IsImpassable || closed.Contains(neighbour))
                    {
                        continue;
                    }

                    if (!open.Contains(neighbour))
                    {
                        neighbour.G = neighbour.MoveCost + current.G;
                        neighbour.Parent = current;
                        open.Add(neighbour);
                    }
                    else
                    {
                        if (neighbour.G > current.G + neighbour.MoveCost)
                        {
                            neighbour.G = current.G + neighbour.MoveCost;
                            neighbour.Parent = current;
                        }
                    }
                }

                if (open.Contains(end))
                {
                    path.Add(end.AddParents(path));
                    path.RemoveAt(0);
                    return path;
                }
            }

            return null;
        }

        static PathNode GetNextNode(List<PathNode> open)
        {
            PathNode node = open.First();

            foreach (PathNode otherNode in open)
            {
                if (node.f() > otherNode.f())
                {
                    node = otherNode;
                }
            }

            return node;
        }

        static IEnumerable<PathNode> TilesToNodes(IEnumerable<WorldTile> tiles)
        {
            List<PathNode> nodes = new List<PathNode>();

            foreach (WorldTile tile in tiles)
            {
                PathNode node = new PathNode(tile.WorldLocation.X, tile.WorldLocation.Y, 1);
                nodes.Add(node);
            }

            return nodes;
        }

        static IEnumerable<PathNode> GetNeighbours(PathNode node, List<PathNode> map)
        {
            List<PathNode> neighbours = new List<PathNode>();

            neighbours.Add(map.FirstOrDefault(x => x.Location.X == node.Location.X && x.Location.Y == node.Location.Y - 1));
            neighbours.Add(map.FirstOrDefault(x => x.Location.X == node.Location.X - 1 && x.Location.Y == node.Location.Y));
            neighbours.Add(map.FirstOrDefault(x => x.Location.X == node.Location.X && x.Location.Y == node.Location.Y + 1));
            neighbours.Add(map.FirstOrDefault(x => x.Location.X == node.Location.X + 1 && x.Location.Y == node.Location.Y));

            return neighbours;
        }
    }
}
