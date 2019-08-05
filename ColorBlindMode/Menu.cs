using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;

namespace ColorBlindMode {
    /// <summary>A UI which shows information about an item.</summary>
    internal class Menu : IClickableMenu {
        private ShaderContainer container;
        public Menu(ShaderContainer container) {
            this.container = container;

            
        }

        public override void draw(SpriteBatch batch) {
            batch.Draw(Game1.fadeToBlackRect, Game1.graphics.GraphicsDevice.Viewport.Bounds, Color.Black * 0.75f);
            IClickableMenu.drawTextureBox(batch, 300, 300, 300, 300, Color.White);


            base.draw(batch);
            base.drawMouse(batch);
        }

        public override void gameWindowSizeChanged(Rectangle oldBounds, Rectangle newBounds) {

        }
    }
}
