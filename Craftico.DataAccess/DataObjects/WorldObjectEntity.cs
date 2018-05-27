using System.Collections.Generic;

using NuciXNA.DataAccess.DataObjects;

namespace Craftico.DataAccess.DataObjects
{
    public sealed class WorldObjectEntity : EntityBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Colour { get; set; }

        public List<WorldObjectLayerEntity> Layers { get; set; }
    }
}
