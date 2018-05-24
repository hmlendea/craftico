using System.Linq;

using NuciXNA.DataAccess.Exceptions;
using NuciXNA.DataAccess.Repositories;

using Craftico.DataAccess.DataObjects;

namespace Craftico.DataAccess.Repositories
{
    /// <summary>
    /// Item repository implementation.
    /// </summary>
    public class ItemRepository : XmlRepository<ItemEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemRepository"/> class.
        /// </summary>
        /// <param name="fileName">File name.</param>
        public ItemRepository(string fileName) : base(fileName)
        {

        }

        /// <summary>
        /// Updates the specified item.
        /// </summary>
        /// <param name="itemEntity">Item.</param>
        public override void Update(ItemEntity itemEntity)
        {
            LoadEntitiesIfNeeded();

            ItemEntity itemEntityToUpdate = Entities.FirstOrDefault(x => x.Id == itemEntity.Id);

            if (itemEntityToUpdate == null)
            {
                throw new EntityNotFoundException(itemEntity.Id, nameof(ItemEntity));
            }

            itemEntityToUpdate.Description = itemEntity.Description;
            itemEntityToUpdate.Type = itemEntity.Type;
            itemEntityToUpdate.SpriteSheet = itemEntity.SpriteSheet;

            XmlFile.SaveEntities(Entities);
        }
    }
}
