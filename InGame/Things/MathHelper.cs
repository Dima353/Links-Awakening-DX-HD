using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ProjectZ.InGame.Things
{
    public static class GameMath
    {
        public static float GetRandomFloat(float min, float max)
        {
            return (float)(min + (max - min) * Game1.RandomNumber.NextDouble());
        }
    }
}
