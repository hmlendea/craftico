using NuciXNA.Primitives;

using Craftico.Settings;

namespace Craftico.Models
{
    public sealed class WorldChunk
    {
        public const int ChunkSize = 32;

        WorldTile[,] tiles;

        public Point2D Location { get; set; }

        public WorldChunk()
        {
            tiles = new WorldTile[GameDefines.WorldChunkSize, GameDefines.WorldChunkSize];
        }

        public WorldTile this[int x, int y]
        {
            get { return tiles[x, y]; }
            set { tiles[x, y] = value; }
        }
    }
}
