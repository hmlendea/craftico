using NuciXNA.DataAccess.Exceptions;
using NuciXNA.DataAccess.Repositories;

using Craftico.DataAccess.DataObjects;

namespace Craftico.DataAccess.Repositories
{
    /// <summary>
    /// Mob repository implementation.
    /// </summary>
    public class MobRepository : XmlRepository<MobEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MobRepository"/> class.
        /// </summary>
        /// <param name="fileName">File name.</param>
        public MobRepository(string fileName) : base(fileName)
        {

        }

        /// <summary>
        /// Updates the specified mob.
        /// </summary>
        /// <param name="entity">Mob.</param>
        public override void Update(MobEntity entity)
        {
            LoadEntitiesIfNeeded();

            MobEntity mobEntityToUpdate = Get(entity.Id);

            if (mobEntityToUpdate == null)
            {
                throw new EntityNotFoundException(entity.Id, nameof(MobEntity));
            }

            mobEntityToUpdate.Name = entity.Name;
            mobEntityToUpdate.Description = entity.Description;
            mobEntityToUpdate.MovementSpeed = entity.MovementSpeed;
            mobEntityToUpdate.InventorySize = entity.InventorySize;
            mobEntityToUpdate.VigourLevel = entity.VigourLevel;
            mobEntityToUpdate.FaithLevel = entity.FaithLevel;

            XmlFile.SaveEntities(Entities.Values);
        }
    }
}
