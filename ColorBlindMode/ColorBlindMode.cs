using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorBlindMode
{
    public class ColorBlindMode : Mod
    {
        private Effect shader;
        private float strength;

        public override void Entry(IModHelper helper)
        {
            shader = helper.Content.Load<Effect>("ColorBlindShader.xnb");
            helper.Shaders.Add(shader);
            helper.ConsoleCommands.Add("ColorBlindMode", "Sets a filter for color blindness.\n" +
                                                         "Usage: ColorBlindMode <option>\n" +
                                                         "- option: Can be one of the follow:\n" +
                                                         "-- protanopia (pro, or 1)\n" +
                                                         "-- deuteranopia (deu or 2)\n" +
                                                         "-- tritanopia (tri or 3)\n" +
                                                         "-- none (off or 0)",
                                                         ApplyColorBlind);
            helper.ConsoleCommands.Add("ColorBlindStr",  "Sets the strength for color blind filters.\n" +
                                                         "Usage: ColorBlindStrength <value>\n" +
                                                         "- value: A decimal value reprsenting percentage.\n" +
                                                         "-- Examples: 0 for 0%, 1.0 for 100%, 0.45 for 45%.",
                                                         AdjustStrength);
            strength = 1.0f;
        }

        private void AdjustStrength(string command, string[] args) {
            float value;

            if (args.Length < 1) {
                Monitor.Log("Invalid use of ColorBlindStrength. Type help for syntax and usage.");
                return;
            }

            value = float.Parse(args[0]);

            if (value < 0) {
                value = 0f;
            } else if (value > 1.0f) {
                value = 1.0f;
            }

            strength = value;
            shader.Parameters["colorBlindStrength"].SetValue(strength);
            return;
        } 

        private void ApplyColorBlind(string command, string[] args) {
            int setValue = 0;

            if (args.Length < 1) {
                Monitor.Log("Invalid use of ColorBlindMode. Type help for syntax and usage.");
                return;
            }

            switch(args[0]) {
                case "protanopia":
                case "pro":
                case "1":
                    setValue = 1;
                    break;
                case "deuteranopia":
                case "deu":
                case "2":
                    setValue = 2;
                    break;
                case "tritanopia":
                case "tri":
                case "3":
                    setValue = 3;
                    break;
                case "none":
                case "off":
                case "0":
                    Helper.Shaders.Set(0);
                    return;
                default:
                    Monitor.Log("Invalid use of ColorBlindMode. Type help for syntax and usage.");
                    return;
            }

            shader.Parameters["colorBlindFilter"].SetValue(setValue);
            Helper.Shaders.Set(1);
        }
    }
}
