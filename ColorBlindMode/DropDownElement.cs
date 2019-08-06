using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewValley;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorBlindMode {
    public class DropDownElement : OptionsElement {

        public DropDownElement(string label, int whichOption, int x = -1, int y = -1) : base(label, x, y, (int)Game1.smallFont.MeasureString("Windowed Borderless Mode   ").X + 48, 44, whichOption) {
            //Game1.options.setDropDownToProperValue(this);
            this.dropDownBounds = new Rectangle(this.bounds.X, this.bounds.Y, this.bounds.Width - 48, this.bounds.Height * this.dropDownOptions.Count);
        }

        // Token: 0x06001AA6 RID: 6822 RVA: 0x001AD9CC File Offset: 0x001ABBCC
        public override void leftClickHeld(int x, int y) {
            if (!this.greyedOut) {
                base.leftClickHeld(x, y);
                this.clicked = true;
                this.dropDownBounds.Y = Math.Min(this.dropDownBounds.Y, Game1.viewport.Height - this.dropDownBounds.Height - this.recentSlotY);
                if (!Game1.options.SnappyMenus) {
                    this.selectedOption = (int)Math.Max(Math.Min((float)(y - this.dropDownBounds.Y) / (float)this.bounds.Height, (float)(this.dropDownOptions.Count - 1)), 0f);
                }
            }
        }

        // Token: 0x06001AA7 RID: 6823 RVA: 0x001ADA76 File Offset: 0x001ABC76
        public override void receiveLeftClick(int x, int y) {
            if (!this.greyedOut) {
                base.receiveLeftClick(x, y);
                this.startingSelected = this.selectedOption;
                if (!this.clicked) {
                    Game1.playSound("shwip");
                }
                this.leftClickHeld(x, y);
                //OptionsDropDown.selected = this;
            }
        }

        // Token: 0x06001AA8 RID: 6824 RVA: 0x001ADAB4 File Offset: 0x001ABCB4
        public override void leftClickReleased(int x, int y) {
            if (!this.greyedOut && this.dropDownOptions.Count > 0) {
                base.leftClickReleased(x, y);
                if (this.clicked) {
                    Game1.playSound("drumkit6");
                }
                this.clicked = false;
                if (this.dropDownBounds.Contains(x, y)) {
                    Game1.options.changeDropDownOption(this.whichOption, this.selectedOption, this.dropDownOptions);
                } else {
                    this.selectedOption = this.startingSelected;
                }
                OptionsDropDown.selected = null;
            }
        }

        // Token: 0x06001AA9 RID: 6825 RVA: 0x001ADB38 File Offset: 0x001ABD38
        public override void receiveKeyPress(Keys key) {
            base.receiveKeyPress(key);
            if (Game1.options.SnappyMenus && !this.greyedOut) {
                if (!this.clicked) {
                    if (Game1.options.doesInputListContain(Game1.options.moveRightButton, key)) {
                        this.selectedOption++;
                        if (this.selectedOption >= this.dropDownOptions.Count) {
                            this.selectedOption = 0;
                        }
                        Game1.options.changeDropDownOption(this.whichOption, this.selectedOption, this.dropDownOptions);
                        return;
                    }
                    if (Game1.options.doesInputListContain(Game1.options.moveLeftButton, key)) {
                        this.selectedOption--;
                        if (this.selectedOption < 0) {
                            this.selectedOption = this.dropDownOptions.Count - 1;
                        }
                        Game1.options.changeDropDownOption(this.whichOption, this.selectedOption, this.dropDownOptions);
                        return;
                    }
                } else if (Game1.options.doesInputListContain(Game1.options.moveDownButton, key)) {
                    Game1.playSound("shiny4");
                    this.selectedOption++;
                    if (this.selectedOption >= this.dropDownOptions.Count) {
                        this.selectedOption = 0;
                        return;
                    }
                } else if (Game1.options.doesInputListContain(Game1.options.moveUpButton, key)) {
                    Game1.playSound("shiny4");
                    this.selectedOption--;
                    if (this.selectedOption < 0) {
                        this.selectedOption = this.dropDownOptions.Count - 1;
                    }
                }
            }
        }

        // Token: 0x06001AAA RID: 6826 RVA: 0x001ADCC4 File Offset: 0x001ABEC4
        public override void draw(SpriteBatch b, int slotX, int slotY) {
            this.recentSlotY = slotY;
            base.draw(b, slotX, slotY);
            float alpha = this.greyedOut ? 0.33f : 1f;
            if (this.clicked) {
                IClickableMenu.drawTextureBox(b, Game1.mouseCursors, OptionsDropDown.dropDownBGSource, slotX + this.dropDownBounds.X, slotY + this.dropDownBounds.Y, this.dropDownBounds.Width, this.dropDownBounds.Height, Color.White * alpha, 4f, false);
                for (int i = 0; i < this.dropDownDisplayOptions.Count; i++) {
                    if (i == this.selectedOption) {
                        b.Draw(Game1.staminaRect, new Rectangle(slotX + this.dropDownBounds.X, slotY + this.dropDownBounds.Y + i * this.bounds.Height, this.dropDownBounds.Width, this.bounds.Height), new Rectangle?(new Rectangle(0, 0, 1, 1)), Color.Wheat, 0f, Vector2.Zero, SpriteEffects.None, 0.975f);
                    }
                    b.DrawString(Game1.smallFont, this.dropDownDisplayOptions[i], new Vector2((float)(slotX + this.dropDownBounds.X + 4), (float)(slotY + this.dropDownBounds.Y + 8 + this.bounds.Height * i)), Game1.textColor * alpha, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.98f);
                }
                b.Draw(Game1.mouseCursors, new Vector2((float)(slotX + this.bounds.X + this.bounds.Width - 48), (float)(slotY + this.bounds.Y)), new Rectangle?(OptionsDropDown.dropDownButtonSource), Color.Wheat * alpha, 0f, Vector2.Zero, 4f, SpriteEffects.None, 0.981f);
                return;
            }
            IClickableMenu.drawTextureBox(b, Game1.mouseCursors, OptionsDropDown.dropDownBGSource, slotX + this.bounds.X, slotY + this.bounds.Y, this.bounds.Width - 48, this.bounds.Height, Color.White * alpha, 4f, false);
            if (OptionsDropDown.selected == null || OptionsDropDown.selected.Equals(this)) {
                b.DrawString(Game1.smallFont, (this.selectedOption < this.dropDownDisplayOptions.Count && this.selectedOption >= 0) ? this.dropDownDisplayOptions[this.selectedOption] : "", new Vector2((float)(slotX + this.bounds.X + 4), (float)(slotY + this.bounds.Y + 8)), Game1.textColor * alpha, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.88f);
            }
            b.Draw(Game1.mouseCursors, new Vector2((float)(slotX + this.bounds.X + this.bounds.Width - 48), (float)(slotY + this.bounds.Y)), new Rectangle?(OptionsDropDown.dropDownButtonSource), Color.White * alpha, 0f, Vector2.Zero, 4f, SpriteEffects.None, 0.88f);
        }

        // Token: 0x040016C0 RID: 5824
        public const int pixelsHigh = 11;

        // Token: 0x040016C1 RID: 5825
        public static OptionsDropDown selected;

        // Token: 0x040016C2 RID: 5826
        public List<string> dropDownOptions = new List<string>();

        // Token: 0x040016C3 RID: 5827
        public List<string> dropDownDisplayOptions = new List<string>();

        // Token: 0x040016C4 RID: 5828
        public int selectedOption;

        // Token: 0x040016C5 RID: 5829
        public int recentSlotY;

        // Token: 0x040016C6 RID: 5830
        public int startingSelected;

        // Token: 0x040016C7 RID: 5831
        private bool clicked;

        // Token: 0x040016C8 RID: 5832
        private Rectangle dropDownBounds;

        // Token: 0x040016C9 RID: 5833
        public static Rectangle dropDownBGSource = new Rectangle(433, 451, 3, 3);

        // Token: 0x040016CA RID: 5834
        public static Rectangle dropDownButtonSource = new Rectangle(437, 450, 10, 11);
    }
}