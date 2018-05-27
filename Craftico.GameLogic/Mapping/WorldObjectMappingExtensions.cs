using System.Collections.Generic;
using System.Linq;

using NuciXNA.Primitives.Mapping;

using Craftico.DataAccess.DataObjects;
using Craftico.Models;

namespace Craftico.GameLogic.Mapping
{
    /// <summary>
    /// WorldObject mapping extensions for converting between entities and domain models.
    /// </summary>
    static class WorldObjectMappingExtensions
    {
        /// <summary>
        /// Converts the entity into a domain model.
        /// </summary>
        /// <returns>The domain model.</returns>
        /// <param name="worldObjectEntity">WorldObject entity.</param>
        internal static WorldObject ToDomainModel(this WorldObjectEntity worldObjectEntity)
        {
            WorldObject worldObject = new WorldObject
            {
                Id = worldObjectEntity.Id,
                Name = worldObjectEntity.Name,
                Description = worldObjectEntity.Description,
                Colour = ColourTranslator.FromHexadecimal(worldObjectEntity.Colour),
                Layers = worldObjectEntity.Layers.ToDomainModels().ToList()
            };

            return worldObject;
        }

        /// <summary>
        /// Converts the domain model into an entity.
        /// </summary>
        /// <returns>The entity.</returns>
        /// <param name="worldObject">WorldObject.</param>
        internal static WorldObjectEntity ToEntity(this WorldObject worldObject)
        {
            WorldObjectEntity worldObjectEntity = new WorldObjectEntity
            {
                Id = worldObject.Id,
                Name = worldObject.Name,
                Description = worldObject.Description,
                Colour = worldObject.Colour.ToHexadecimal(),
                Layers = worldObject.Layers.ToEntities().ToList()
            };

            return worldObjectEntity;
        }

        /// <summary>
        /// Converts the entities into domain models.
        /// </summary>
        /// <returns>The domain models.</returns>
        /// <param name="worldObjectEntities">WorldObject entities.</param>
        internal static IEnumerable<WorldObject> ToDomainModels(this IEnumerable<WorldObjectEntity> worldObjectEntities)
        {
            IEnumerable<WorldObject> worldObjects = worldObjectEntities.Select(worldObjectEntity => worldObjectEntity.ToDomainModel());

            return worldObjects;
        }

        /// <summary>
        /// Converts the domain models into entities.
        /// </summary>
        /// <returns>The entities.</returns>
        /// <param name="worldObjects">WorldObjects.</param>
        internal static IEnumerable<WorldObjectEntity> ToEntities(this IEnumerable<WorldObject> worldObjects)
        {
            IEnumerable<WorldObjectEntity> worldObjectEntities = worldObjects.Select(worldObject => worldObject.ToEntity());

            return worldObjectEntities;
        }
    }
}
