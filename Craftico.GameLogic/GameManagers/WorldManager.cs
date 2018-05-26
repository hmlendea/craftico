using Microsoft.Xna.Framework;

using Craftico.GameLogic.Generators;
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
            WorldChunk chunk = GetChunk(x / GameDefines.WorldChunkSize, y / GameDefines.WorldChunkSize);

            // Gets the tile inside the chunk using its index (0, cts)
            int ctx = x % GameDefines.WorldChunkSize;
            int cty = y % GameDefines.WorldChunkSize;

            if (x < 0)
            {
                ctx = GameDefines.WorldChunkSize + ctx;
            }

            if (y < 0)
            {
                cty = GameDefines.WorldChunkSize + cty;
            }

            return chunk[ctx, cty];
        }

        WorldChunk GetChunk(int x, int y)
        {
            int chunkX = GameDefines.WorldSize / 2 + x;
            int chunkY = GameDefines.WorldSize / 2 + y;

            WorldChunk chunk;

            if (chunks[chunkX, chunkY] == null)
            {
                WorldGenerator worldGenerator = new WorldGenerator(873);

                chunk = worldGenerator.GenerateChunk(x, y);
                chunks[chunkX, chunkY] = chunk;
            }
            else
            {
                chunk = chunks[chunkX, chunkY];
            }

            return chunk;
        }
    }
}
