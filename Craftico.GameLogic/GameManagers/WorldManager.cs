using Microsoft.Xna.Framework;

using Craftico.Models;
using Craftico.Settings;

namespace Craftico.GameLogic.GameManagers
{
    public sealed class WorldManager : IWorldManager
    {
        WorldChunk[,] chunks;

        /// <summary>
        /// Loads the content.
        /// </summary>
        public void LoadContent()
        {
            chunks = new WorldChunk[GameDefines.WorldSize, GameDefines.WorldSize];
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public void UnloadContent()
        {
            chunks = null;
        }

        /// <summary>
        /// Update the content.
        /// </summary>
        /// <param name="gameTime">Game time.</param>
        public void Update(GameTime gameTime)
        {

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
