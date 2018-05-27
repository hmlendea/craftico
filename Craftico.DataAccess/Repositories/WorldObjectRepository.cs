using NuciXNA.DataAccess.Exceptions;
using NuciXNA.DataAccess.Repositories;

using Craftico.DataAccess.DataObjects;

namespace Craftico.DataAccess.Repositories
{
    /// <summary>
    /// WorldObject repository implementation.
    /// </summary>
    public class WorldObjectRepository : XmlRepository<WorldObjectEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorldObjectRepository"/> class.
        /// </summary>
        /// <param name="fileName">File name.</param>
        public WorldObjectRepository(string fileName) : base(fileName)
        {

        }

        /// <summary>
        /// Updates the specified worldObject.
        /// </summary>
        /// <param name="worldObjectEntity">WorldObject.</param>
        public override void Update(WorldObjectEntity worldObjectEntity)
        {
            LoadEntitiesIfNeeded();

            WorldObjectEntity worldObjectEntityToUpdate = Get(worldObjectEntity.Id);

            if (worldObjectEntityToUpdate == null)
            {
                throw new EntityNotFoundException(worldObjectEntity.Id, nameof(WorldObjectEntity));
            }

            worldObjectEntityToUpdate.Name = worldObjectEntity.Name;
            worldObjectEntityToUpdate.Description = worldObjectEntity.Description;
            worldObjectEntityToUpdate.Colour = worldObjectEntity.Colour;
            worldObjectEntityToUpdate.Layers = worldObjectEntity.Layers;

            XmlFile.SaveEntities(Entities.Values);
        }
    }
}
