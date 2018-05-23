using Craftico.Models;
using Craftico.Settings;

namespace Craftico.GameLogic.GameManagers
{
    public sealed class WorldManager : IWorldManager
    {
        WorldChunk[,] chunks;

        public WorldManager()
        {
            chunks = new WorldChunk[GameDefines.WorldSize, GameDefines.WorldSize];
        }

        public WorldTile GetTile(int x, int y)
        {
            // Transforms world coordinates (-wts/2, +wts/2) to chunk index (0, wcs)
            int chunkX = GameDefines.WorldSize / 2 + x / GameDefines.WorldChunkSize;
            int chunkY = GameDefines.WorldSize / 2 + y / GameDefines.WorldChunkSize;

            WorldChunk chunk = chunks[chunkX, chunkY];

            // Gets the tile inside the chunk using its index (0, cts) 
            return chunk[x % GameDefines.WorldChunkSize, y % GameDefines.WorldChunkSize];
        }
    }
}
