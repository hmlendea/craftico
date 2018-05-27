using System.Collections.Generic;

using NuciXNA.Primitives;

namespace Craftico.Models
{
    public sealed class WorldObject
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Colour Colour { get; set; }

        public List<WorldObjectLayer> Layers { get; set; }
    }
}
