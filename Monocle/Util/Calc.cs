using Microsoft.Xna.Framework;
using System;

namespace Monocle.Util
{
    public static class Calc
    {
        public static Vector2 AngleToVector(float angleRadians, float length)
        {
            return new Vector2((float)Math.Cos(angleRadians) * length, (float)Math.Sin(angleRadians) * length);
        }

        public static float Angle(Vector2 from, Vector2 to)
        {
            return (float)Math.Atan2(to.Y - from.Y, to.X - from.X);
        }

        public static Vector2 Perpendicular(this Vector2 vector)
        {
            return new Vector2(-vector.X, vector.Y);
        }
    }
}
