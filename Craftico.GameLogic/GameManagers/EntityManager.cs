using System.Collections.Generic;
using System.IO;
using System.Linq;

using Microsoft.Xna.Framework;

using Craftico.DataAccess.Repositories;
using Craftico.GameLogic.Mapping;
using Craftico.Models;
using Craftico.Settings;

namespace Craftico.GameLogic.GameManagers
{
    public class EntityManager : IEntityManager
    {
        List<Item> items;
        List<Mob> mobs;
        List<Terrain> terrains;
        List<WorldObject> worldObjects;

        /// <summary>
        /// Loads the entities in memory.
        /// </summary>
        public void LoadContent()
        {
            string itemPath = Path.Combine(ApplicationPaths.EntitiesDirectory, "items.xml");
            string mobPath = Path.Combine(ApplicationPaths.EntitiesDirectory, "mobs.xml");
            string prayerPath = Path.Combine(ApplicationPaths.EntitiesDirectory, "prayers.xml");
            string spellPath = Path.Combine(ApplicationPaths.EntitiesDirectory, "spells.xml");
            string terrainsPath = Path.Combine(ApplicationPaths.EntitiesDirectory, "terrains.xml");
            string worldObjectsPath = Path.Combine(ApplicationPaths.EntitiesDirectory, "worldObjects.xml");

            ItemRepository itemRepository = new ItemRepository(itemPath);
            //MobRepository mobRepository = new MobRepository(mobPath);
            TerrainRepository terrainRepository = new TerrainRepository(terrainsPath);
            WorldObjectRepository worldObjectRepository = new WorldObjectRepository(worldObjectsPath);

            items = itemRepository.GetAll().ToDomainModels().ToList();
            //mobs = mobRepository.GetAll().ToDomainModels().ToList();
            terrains = terrainRepository.GetAll().ToDomainModels().ToList();
            worldObjects = worldObjectRepository.GetAll().ToDomainModels().ToList();
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public void UnloadContent()
        {
            items.Clear();
            mobs.Clear();
            terrains.Clear();
            worldObjects.Clear();
        }

        /// <summary>
        /// Update the content.
        /// </summary>
        /// <param name="gameTime">Game time.</param>
        public void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <returns>The item.</returns>
        /// <param name="id">Identifier.</param>
        public Item GetItem(string id)
        => items.FirstOrDefault(x => x.Id == id);

        /// <summary>
        /// Gets the mob.
        /// </summary>
        /// <returns>The mob.</returns>
        /// <param name="id">Identifier.</param>
        public Mob GetMob(string id)
        => mobs.FirstOrDefault(x => x.Id == id);

        /// <summary>
        /// Gets the terrain.
        /// </summary>
        /// <returns>The terrain.</returns>
        /// <param name="id">Identifier.</param>
        public Terrain GetTerrain(string id)
        => terrains.FirstOrDefault(x => x.Id == id);

        /// <summary>
        /// Gets the world object.
        /// </summary>
        /// <returns>The world object.</returns>
        /// <param name="id">Identifier.</param>
        public WorldObject GetWorldObject(string id)
        => worldObjects.FirstOrDefault(x => x.Id == id);

        public IEnumerable<Terrain> GetTerrains()
        => terrains;

        public IEnumerable<WorldObject> GetWorldObjects()
        => worldObjects;
    }
}
