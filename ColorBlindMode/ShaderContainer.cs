using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorBlindMode {
    internal class ShaderContainer {
        public int Filter { get; private set; }
        public Effect Shader { get; }
        public float Strength { get; private set; }

        public ShaderContainer(Effect Shader) {
            this.Shader = Shader;
            Filter = 0;
            Strength = 1.0f;
        }

        public bool AdjustFilter(string Filter) {
            int value;
            switch (Filter) {
                case "protanopia":
                case "pro":
                case "1":
                    value = 1;
                    break;
                case "deuteranopia":
                case "deu":
                case "2":
                    value = 2;
                    break;
                case "tritanopia":
                case "tri":
                case "3":
                    value = 3;
                    break;
                case "none":
                case "off":
                case "0":
                    value = 0;
                    break;
                default:
                    return false;
            }

            return AdjustFilter(value);
        }
        
        public bool AdjustFilter(int Filter) {
            if (Filter < 0 || Filter > 3) return false;
            this.Filter = Filter;
            return true;
        }

        public bool AdjustStrength(float Strength) {
            if (Strength < 0 || Strength > 1.0f) return false;
            this.Strength = Strength;
            return true;
        }

        public void Apply() {
            //Shader.Parameters["colorBlindStrength"].SetValue(Strength);
            Shader.Parameters["colorBlindFilter"].SetValue(Filter);
        }
    }
}
