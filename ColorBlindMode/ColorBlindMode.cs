using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorBlindMode {
    public class ColorBlindMode : Mod {
        private ShaderContainer container;

        public override void Entry(IModHelper helper) {
            Effect shader = helper.Content.Load<Effect>("ColorBlindShader.xnb");
            container = new ShaderContainer(shader);
            helper.Shaders.Add(shader);
            Helper.Shaders.Set(0);

            helper.Events.Input.ButtonPressed += OnButtonPressed;
            helper.ConsoleCommands.Add("ColorBlindMode", "Sets a filter for color blindness.\n" +
                                                         "Usage: ColorBlindMode <option>\n" +
                                                         "- option: Can be one of the follow:\n" +
                                                         "-- protanopia (pro, or 1)\n" +
                                                         "-- deuteranopia (deu or 2)\n" +
                                                         "-- tritanopia (tri or 3)\n" +
                                                         "-- none (off or 0)",
                                                         ApplyColorBlind);
            helper.ConsoleCommands.Add("ColorBlindStr", "Sets the strength for color blind filters.\n" +
                                                         "Usage: ColorBlindStrength <value>\n" +
                                                         "- value: A decimal value reprsenting percentage.\n" +
                                                         "-- Examples: 0 for 0%, 1.0 for 100%, 0.45 for 45%.",
                                                         AdjustStrength);
        }

        private void OnButtonPressed(object sender, ButtonPressedEventArgs e) {
            if (e.Button == SButton.V) {
                if (Game1.activeClickableMenu != null) return;

                Game1.activeClickableMenu = new Menu(container);
            }
        }

        private void AdjustStrength(string command, string[] args) {
            if (args.Length < 1) {
                Monitor.Log("Invalid use of ColorBlindStrength. Type help for syntax and usage.");
                return;
            }

            float value = float.Parse(args[0]);
            if (!(container.AdjustStrength(value))) {
                Monitor.Log("Invalid use of ColorBlindStrength. Type help for syntax and usage.");
                return;
            } 

            container.Apply();
            return;
        }

        private void ApplyColorBlind(string command, string[] args) {
            if (args.Length < 1) {
                Monitor.Log("Invalid use of ColorBlindMode. Type help for syntax and usage.");
                return;
            }

            if (!(container.AdjustFilter(args[0]))) {
                Monitor.Log("Invalid use of ColorBlindMode. Type help for syntax and usage.");
                return;
            }

            container.Apply();
            Helper.Shaders.Set((container.Filter == 0) ? 0 : 1);
        }
    }
}