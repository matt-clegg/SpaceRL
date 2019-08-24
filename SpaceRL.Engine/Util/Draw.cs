using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceRL.Engine.Renderers;
using System;

namespace SpaceRL.Engine.Util
{
    public static class Draw
    {
        private const float DefaultThickness = 1f;

        private static Rectangle _rect;

        public static Renderer Renderer { get; internal set; }
        public static SpriteBatch SpriteBatch { get; private set; }

        public static Texture2D Pixel { get; set; }

        public static void UseDebugPixelTexture(GraphicsDevice graphicsDevice)
        {
            Pixel = new Texture2D(graphicsDevice, 1, 1);
            Pixel.SetData(new[] { Color.White });
        }

        public static void Point(Vector2 position, Color color)
        {
            SpriteBatch.Draw(Pixel, position, Pixel.Bounds, color, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
        }

        public static void Line(Vector2 start, Vector2 end, Color color, float thickness = DefaultThickness)
        {
            LineAngle(start, Calc.Angle(start, end), Vector2.Distance(start, end), color, thickness);
        }

        public static void Line(float x1, float y1, float x2, float y2, Color color)
        {
            Line(new Vector2(x1, y1), new Vector2(x2, y2), color);
        }

        public static void LineAngle(Vector2 start, float angle, float length, Color color, float thickness = DefaultThickness)
        {
            if (thickness < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(thickness), "Line thickness must be greater than 1.");
            }

            SpriteBatch.Draw(Pixel, start, Pixel.Bounds, color, angle, new Vector2(0, 0.5f), new Vector2(length, thickness), SpriteEffects.None, 0);
        }

        public static void LineAngle(float startX, float startY, float angle, float length, Color color)
        {
            LineAngle(new Vector2(startX, startY), angle, length, color);
        }

        public static void Circle(Vector2 position, float radius, Color color, int resolution, float thickness = DefaultThickness)
        {
            if (thickness < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(thickness), "Circle thickness must be greater than 1.");
            }

            Vector2 last = Vector2.UnitX * radius;
            Vector2 lastP = last.Perpendicular();

            for (int i = 1; i <= resolution; i++)
            {
                Vector2 at = Calc.AngleToVector(i * MathHelper.PiOver2 / resolution, radius);
                Vector2 atP = at.Perpendicular();

                Line(position + last, position + at, color, thickness);
                Line(position - last, position - at, color, thickness);
                Line(position + lastP, position + atP, color, thickness);
                Line(position - lastP, position - atP, color, thickness);

                last = at;
                lastP = atP;
            }
        }

        public static void Circle(float x, float y, float radius, Color color, int resolution, float thickness = DefaultThickness)
        {
            Circle(new Vector2(x, y), radius, color, resolution, thickness);
        }

        public static void Rect(float x, float y, float width, float height, Color color)
        {
            _rect.X = (int)x;
            _rect.Y = (int)y;
            _rect.Width = (int)width;
            _rect.Height = (int)height;
            SpriteBatch.Draw(Pixel, _rect, Pixel.Bounds, color);
        }

        public static void Rect(Vector2 position, float width, float height, Color color)
        {
            Rect(position.X, position.Y, width, height, color);
        }

        public static void Rect(Rectangle rect, Color color)
        {
            _rect = rect;
            SpriteBatch.Draw(Pixel, _rect, Pixel.Bounds, color);
        }

        public static void HollowRect(float x, float y, float width, float height, Color color)
        {
            _rect.X = (int)x;
            _rect.Y = (int)y;
            _rect.Width = (int)width;
            _rect.Height = 1;

            SpriteBatch.Draw(Pixel, _rect, Pixel.Bounds, color);

            _rect.Y += (int)height - 1;

            SpriteBatch.Draw(Pixel, _rect, Pixel.Bounds, color);

            _rect.Y -= (int)height - 1;
            _rect.Width = 1;
            _rect.Height = (int)height;

            SpriteBatch.Draw(Pixel, _rect, Pixel.Bounds, color);

            _rect.X += (int)width - 1;

            SpriteBatch.Draw(Pixel, _rect, Pixel.Bounds, color);
        }

        public static void HollowRect(Vector2 position, float width, float height, Color color)
        {
            HollowRect(position.X, position.Y, width, height, color);
        }

        public static void HolllowRect(Rectangle rect, Color color)
        {
            HollowRect(rect.X, rect.Y, rect.Width, rect.Height, color);
        }

        internal static void Initialize(GraphicsDevice graphicsDevice)
        {
            SpriteBatch = new SpriteBatch(graphicsDevice);
            UseDebugPixelTexture(graphicsDevice);
        }

    }
}
