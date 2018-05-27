using System.Collections.Generic;
using System.Linq;

using NuciXNA.Primitives;

using Craftico.DataAccess.DataObjects;
using Craftico.Models;

namespace Craftico.GameLogic.Mapping
{
    /// <summary>
    /// WorldObjectLayer mapping extensions for converting between entities and domain models.
    /// </summary>
    static class WorldObjectLayerMappingExtensions
    {
        /// <summary>
        /// Converts the entity into a domain model.
        /// </summary>
        /// <returns>The domain model.</returns>
        /// <param name="worldObjectLayerEntity">WorldObjectLayer entity.</param>
        internal static WorldObjectLayer ToDomainModel(this WorldObjectLayerEntity worldObjectLayerEntity)
        {
            WorldObjectLayer worldObjectLayer = new WorldObjectLayer
            {
                SpriteSheet = worldObjectLayerEntity.SpriteSheet,
                SpriteIndex = worldObjectLayerEntity.SpriteIndex,
                Size = new Size2D(worldObjectLayerEntity.Width, worldObjectLayerEntity.Height),
                Anchor = new Point2D(worldObjectLayerEntity.AnchorX, worldObjectLayerEntity.AnchorY),
                Location = new Point2D(worldObjectLayerEntity.X, worldObjectLayerEntity.Y)
            };

            return worldObjectLayer;
        }

        /// <summary>
        /// Converts the domain model into an entity.
        /// </summary>
        /// <returns>The entity.</returns>
        /// <param name="worldObjectLayer">WorldObjectLayer.</param>
        internal static WorldObjectLayerEntity ToEntity(this WorldObjectLayer worldObjectLayer)
        {
            WorldObjectLayerEntity worldObjectLayerEntity = new WorldObjectLayerEntity
            {
                SpriteSheet = worldObjectLayer.SpriteSheet,
                SpriteIndex = worldObjectLayer.SpriteIndex,
                Width = worldObjectLayer.Size.Width,
                Height = worldObjectLayer.Size.Height,
                AnchorX = worldObjectLayer.Anchor.X,
                AnchorY = worldObjectLayer.Anchor.Y,
                X = worldObjectLayer.Location.X,
                Y = worldObjectLayer.Location.Y
            };

            return worldObjectLayerEntity;
        }

        /// <summary>
        /// Converts the entities into domain models.
        /// </summary>
        /// <returns>The domain models.</returns>
        /// <param name="worldObjectLayerEntities">WorldObjectLayer entities.</param>
        internal static IEnumerable<WorldObjectLayer> ToDomainModels(this IEnumerable<WorldObjectLayerEntity> worldObjectLayerEntities)
        {
            IEnumerable<WorldObjectLayer> worldObjectLayers = worldObjectLayerEntities.Select(worldObjectLayerEntity => worldObjectLayerEntity.ToDomainModel());

            return worldObjectLayers;
        }

        /// <summary>
        /// Converts the domain models into entities.
        /// </summary>
        /// <returns>The entities.</returns>
        /// <param name="worldObjectLayers">WorldObjectLayers.</param>
        internal static IEnumerable<WorldObjectLayerEntity> ToEntities(this IEnumerable<WorldObjectLayer> worldObjectLayers)
        {
            IEnumerable<WorldObjectLayerEntity> worldObjectLayerEntities = worldObjectLayers.Select(worldObjectLayer => worldObjectLayer.ToEntity());

            return worldObjectLayerEntities;
        }
    }
}
