﻿using NuciXNA.Primitives;

namespace Craftico.Settings
{
    public class GraphicsSettings
    {
        /// <summary>
        /// Gets or sets the resolution.
        /// </summary>
        /// <value>The resolution.</value>
        public Size2D Resolution { get; set; }

        /// <summary>
        /// Gets or sets the fullscreen mode toggle.
        /// </summary>
        /// <value>The fullscreen mode.</value>
        public bool Fullscreen { get; set; }

        public bool FogOfWar { get; set; }

        public GraphicsSettings()
        {
            Resolution = new Size2D(1280, 768);
        }
    }
}
