using System.Collections.Generic;
using System.IO;
using System.Linq;

using NuciXNA.DataAccess.IO;

using Craftico.DataAccess.DataObjects;
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

        /// <summary>
        /// Loads the entities in memory.
        /// </summary>
        public void LoadContent()
        {
            string itemPath = Path.Combine(ApplicationPaths.EntitiesDirectory, "items.xml");
            string mobPath = Path.Combine(ApplicationPaths.EntitiesDirectory, "mobs.xml");
            string prayerPath = Path.Combine(ApplicationPaths.EntitiesDirectory, "prayers.xml");
            string spellPath = Path.Combine(ApplicationPaths.EntitiesDirectory, "spells.xml");

            ItemRepository itemRepository = new ItemRepository(itemPath);
            //MobRepository mobRepository = new MobRepository(mobPath);

            items = itemRepository.GetAll().ToDomainModels().ToList();
            //mobs = mobRepository.GetAll().ToDomainModels().ToList();
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <returns>The item.</returns>
        /// <param name="id">Identifier.</param>
        public Item GetItem(string id)
        {
            return items.FirstOrDefault(x => x.Id == id);
        }


        /// <summary>
        /// Gets the mob.
        /// </summary>
        /// <returns>The mob.</returns>
        /// <param name="id">Identifier.</param>
        public Mob GetMob(string id)
        {
            return mobs.FirstOrDefault(x => x.Id == id);
        }
    }
}
