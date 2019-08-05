using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorBlindMode {
    class Sprites {
        /// <summary>Sprites used to draw icons.</summary>
        public static class Icons {
            /// <summary>The sprite sheet containing the icon sprites.</summary>
            public static Texture2D Sheet => Game1.mouseCursors;

            /// <summary>An empty checkbox icon.</summary>
            public static readonly Rectangle EmptyCheckbox = new Rectangle(227, 425, 9, 9);

            /// <summary>A filled checkbox icon.</summary>
            public static readonly Rectangle FilledCheckbox = new Rectangle(236, 425, 9, 9);

            /// <summary>A filled heart indicating a friendship level.</summary>
            public static readonly Rectangle FilledHeart = new Rectangle(211, 428, 7, 6);

            /// <summary>An empty heart indicating a missing friendship level.</summary>
            public static readonly Rectangle EmptyHeart = new Rectangle(218, 428, 7, 6);

            /// <summary>A down arrow for scrolling content.</summary>
            public static readonly Rectangle DownArrow = new Rectangle(12, 76, 40, 44);

            /// <summary>An up arrow for scrolling content.</summary>
            public static readonly Rectangle UpArrow = new Rectangle(76, 72, 40, 44);

            /// <summary>A stardrop icon.</summary>
            public static readonly Rectangle Stardrop = new Rectangle(346, 392, 8, 8);
        }
    }
}
