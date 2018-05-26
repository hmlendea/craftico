using System.Collections.Generic;

using Microsoft.Xna.Framework;
using NuciXNA.Primitives;

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
            Point2D chunkLocation = GetChunkLocationFromTileLocation(x, y);
            WorldChunk chunk = GetChunk(chunkLocation.X, chunkLocation.Y);

            // Gets the tile inside the chunk using its index (0, cts)
            int ctx = x % GameDefines.WorldChunkSize;
            int cty = y % GameDefines.WorldChunkSize;

            if (x < 0 && ctx != 0)
            {
                ctx = GameDefines.WorldChunkSize + ctx;
            }

            if (y < 0 && cty != 0)
            {
                cty = GameDefines.WorldChunkSize + cty;
            }

            return chunk[ctx, cty];
        }

        public IEnumerable<WorldTile> GetChunkTilesAroundLocation(int x, int y)
        {
            Point2D chunkLocation = GetChunkLocationFromTileLocation(x, y);

            List<WorldTile> tiles = new List<WorldTile>();

            for (int j = -1; j <= 1; j++)
            {
                for (int i = -1; i <= 1; i++)
                {
                    tiles.AddRange(GetChunkTiles(chunkLocation.X + i, chunkLocation.Y + j));
                }
            }

            return tiles;
        }

        IEnumerable<WorldTile> GetChunkTiles(int x, int y)
        {
            WorldChunk chunk = GetChunk(x, y);

            List<WorldTile> tiles = new List<WorldTile>();

            for (int j = 0; j < GameDefines.WorldChunkSize; j++)
            {
                for (int i = 0; i < GameDefines.WorldChunkSize; i++)
                {
                    tiles.Add(chunk[i, j]);
                }
            }

            return tiles;
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

        Point2D GetChunkLocationFromTileLocation(int tileX, int tileY)
        {
            int chunkX = tileX / GameDefines.WorldChunkSize;
            int chunkY = tileY / GameDefines.WorldChunkSize;

            if (tileX < 0 && tileX % GameDefines.WorldChunkSize > 0)
            {
                chunkX -= 1;
            }

            if (tileY < 0 && tileY % GameDefines.WorldChunkSize > 0)
            {
                chunkY -= 1;
            }

            return new Point2D(chunkX, chunkY);
        }
    }
}
