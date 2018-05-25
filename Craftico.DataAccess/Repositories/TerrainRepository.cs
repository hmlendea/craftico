using NuciXNA.DataAccess.Exceptions;
using NuciXNA.DataAccess.Repositories;

using Craftico.DataAccess.DataObjects;

namespace Craftico.DataAccess.Repositories
{
    /// <summary>
    /// Terrain repository implementation.
    /// </summary>
    public class TerrainRepository : XmlRepository<TerrainEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainRepository"/> class.
        /// </summary>
        /// <param name="fileName">File name.</param>
        public TerrainRepository(string fileName) : base(fileName)
        {

        }

        /// <summary>
        /// Updates the specified terrain.
        /// </summary>
        /// <param name="terrainEntity">Terrain.</param>
        public override void Update(TerrainEntity terrainEntity)
        {
            LoadEntitiesIfNeeded();

            TerrainEntity terrainEntityToUpdate = Get(terrainEntity.Id);

            if (terrainEntityToUpdate == null)
            {
                throw new EntityNotFoundException(terrainEntity.Id, nameof(TerrainEntity));
            }

            terrainEntityToUpdate.Name = terrainEntity.Name;
            terrainEntityToUpdate.Description = terrainEntity.Description;
            terrainEntityToUpdate.SpriteSheet = terrainEntity.SpriteSheet;
            terrainEntityToUpdate.Order = terrainEntity.Order;

            XmlFile.SaveEntities(Entities.Values);
        }
    }
}
