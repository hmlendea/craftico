using System;
using System.Collections.Generic;

using NuciXNA.Primitives;

using Craftico.Models;
using Craftico.Settings;

namespace Craftico.GameLogic.Generators
{
    public class WorldGenerator
    {
        Random random;

        public int Seed { get; }

        public WorldGenerator(int seed)
        {
            Seed = seed;
            random = new Random(seed);
        }

        public WorldChunk GenerateChunk(int x, int y)
        {
            List<string> terrainIds = new List<string> { "grass", "dirt", "sand" };
            WorldChunk chunk = new WorldChunk
            {
                Location = new Point2D(x, y)
            };

            for (int j = 0; j < GameDefines.WorldChunkSize; j++)
            {
                for (int i = 0; i < GameDefines.WorldChunkSize; i++)
                {
                    WorldTile tile = new WorldTile
                    {
                        TerrainId = terrainIds[random.Next(0, terrainIds.Count)],
                        ChunkLocation = new Point2D(i, j),
                        WorldLocation = new Point2D(
                            x * GameDefines.WorldChunkSize + i,
                            y * GameDefines.WorldChunkSize + j)
                    };

                    chunk[i, j] = tile;
                }
            }

            return chunk;
        }
    }
}
