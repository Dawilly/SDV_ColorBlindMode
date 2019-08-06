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
            width = 700;
            height = 300;
            Vector2 topLeft = Utility.getTopLeftPositionForCenteringOnScreen(width, height);
            xPositionOnScreen = (int)topLeft.X;
            yPositionOnScreen = (int)topLeft.Y;
        }

        public override void draw(SpriteBatch batch) {
            batch.Draw(Game1.fadeToBlackRect, Game1.graphics.GraphicsDevice.Viewport.Bounds, Color.Black * 0.50f);
            IClickableMenu.drawTextureBox(batch, xPositionOnScreen, yPositionOnScreen, width, height, Color.White);

            batch.Draw(Icons.Sheet, new Vector2(xPositionOnScreen + 100, yPositionOnScreen + 25), Icons.SilverStar, Color.White, 0, Vector2.Zero, 10f, SpriteEffects.None, 1f);
            batch.Draw(Icons.Sheet, new Vector2(xPositionOnScreen + 300, yPositionOnScreen + 25), Icons.GoldStar, Color.White, 0, Vector2.Zero, 10f, SpriteEffects.None, 1f);
            batch.Draw(Icons.Sheet, new Vector2(xPositionOnScreen + 500, yPositionOnScreen + 25), Icons.IridiumStar, Color.White, 0, Vector2.Zero, 10f, SpriteEffects.None, 1f);

            batch.DrawString(Game1.smallFont, "Silver", new Vector2(xPositionOnScreen + 100, yPositionOnScreen + 100), Color.Black);
            batch.DrawString(Game1.smallFont, "Gold", new Vector2(xPositionOnScreen + 300, yPositionOnScreen + 100), Color.Black);
            batch.DrawString(Game1.smallFont, "Iridium", new Vector2(xPositionOnScreen + 500, yPositionOnScreen + 100), Color.Black);

            base.draw(batch);
            base.drawMouse(batch);
        }

        public override void gameWindowSizeChanged(Rectangle oldBounds, Rectangle newBounds) {

        }
    }
}
