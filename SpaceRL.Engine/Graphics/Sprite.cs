using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace SpaceRL.Engine.Graphics
{
    public class Sprite : IDisposable
    {
        public Texture2D Texture { get; private set; }
        public Rectangle Bounds { get; private set; }
        public Vector2 DrawOffset { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Vector2 Center { get; private set; }

        public Sprite(Texture2D texture)
        {
            Texture = texture;
            Bounds = new Rectangle(0, 0, Texture.Width, Texture.Height);
            DrawOffset = Vector2.Zero;
            Width = Bounds.Width;
            Height = Bounds.Height;
            SetUtil();
        }

        public Sprite(Sprite parent, int x, int y, int width, int height)
        {
            Texture = parent.Texture;
            Bounds = parent.GetRelativeRect(x, y, width, height);
            DrawOffset = new Vector2(-Math.Min(x - parent.DrawOffset.X, 0), -Math.Min(y - parent.DrawOffset.Y, 0));
            Width = width;
            Height = height;
            SetUtil();
        }

        public Sprite(int width, int height, Color color)
        {
            Texture = new Texture2D(Engine.Instance.GraphicsDevice, width, height);
            var colors = new Color[width * height];
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = color;
            }
            Texture.SetData(colors);

            Bounds = new Rectangle(0, 0, width, height);
            DrawOffset = Vector2.Zero;
            Width = width;
            Height = height;
            SetUtil();
        }

        public Rectangle GetRelativeRect(Rectangle rectangle)
        {
            return GetRelativeRect(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        public Rectangle GetRelativeRect(int x, int y, int width, int height)
        {
            int atX = (int)(Bounds.X - DrawOffset.X + x);
            int atY = (int)(Bounds.Y - DrawOffset.Y + y);

            int rX = MathHelper.Clamp(atX, Bounds.Left, Bounds.Right);
            int rY = MathHelper.Clamp(atY, Bounds.Top, Bounds.Bottom);
            int rW = Math.Max(0, Math.Min(atX + width, Bounds.Right) - rX);
            int rH = Math.Max(0, Math.Min(atY + height, Bounds.Bottom) - rY);

            return new Rectangle(rX, rY, rW, rH);
        }

        public void Dispose()
        {
            Texture?.Dispose();
        }

        public void Unload()
        {
            Texture?.Dispose();
            Texture = null;
        }

        public void Draw(Vector2 position)
        {
            CheckDisposed();
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, Color.White, 0, -DrawOffset, 1f, SpriteEffects.None, 0);
        }

        public void Draw(Vector2 position, Vector2 origin)
        {
            CheckDisposed();
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, Color.White, 0, origin - DrawOffset, 1f, SpriteEffects.None, 0);
        }

        public void Draw(Vector2 position, Vector2 origin, Color color)
        {
            CheckDisposed();
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, 0, origin - DrawOffset, 1f, SpriteEffects.None, 0);
        }

        public void Draw(Vector2 position, Vector2 origin, Color color, float scale)
        {
            CheckDisposed();
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, 0, origin - DrawOffset, scale, SpriteEffects.None, 0);
        }

        public void Draw(Vector2 position, Vector2 origin, Color color, float scale, float rotation)
        {
            CheckDisposed();
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, rotation, origin - DrawOffset, scale, SpriteEffects.None, 0);
        }

        public void Draw(Vector2 position, Vector2 origin, Color color, float scale, float rotation, SpriteEffects effects)
        {
            CheckDisposed();
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, rotation, origin - DrawOffset, scale, effects, 0);
        }

        public void Draw(Vector2 position, Vector2 origin, Color color, Vector2 scale)
        {
            CheckDisposed();
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, 0, origin - DrawOffset, scale, SpriteEffects.None, 0);
        }

        public void Draw(Vector2 position, Vector2 origin, Color color, Vector2 scale, float rotation)
        {
            CheckDisposed();
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, rotation, origin - DrawOffset, scale, SpriteEffects.None, 0);
        }

        public void Draw(Vector2 position, Vector2 origin, Color color, Vector2 scale, float rotation, SpriteEffects effects)
        {
            CheckDisposed();
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, rotation, origin - DrawOffset, scale, effects, 0);
        }

        public void Draw(Vector2 position, Vector2 origin, Color color, Vector2 scale, float rotation, Rectangle clip)
        {
            CheckDisposed();
            Util.Draw.SpriteBatch.Draw(Texture, position, GetRelativeRect(clip), color, rotation, origin - DrawOffset, scale, SpriteEffects.None, 0);
        }

        public void DrawCentered(Vector2 position)
        {
            CheckDisposed();
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, Color.White, 0, Center - DrawOffset, 1f, SpriteEffects.None, 0);
        }

        public void DrawCentered(Vector2 position, Color color)
        {
            CheckDisposed();
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, 0, Center - DrawOffset, 1f, SpriteEffects.None, 0);
        }

        public void DrawCentered(Vector2 position, Color color, float scale)
        {
            CheckDisposed();
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, 0, Center - DrawOffset, scale, SpriteEffects.None, 0);
        }

        public void DrawCentered(Vector2 position, Color color, float scale, float rotation)
        {
            CheckDisposed();
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, rotation, Center - DrawOffset, scale, SpriteEffects.None, 0);
        }

        public void DrawCentered(Vector2 position, Color color, float scale, float rotation, SpriteEffects effects)
        {
            CheckDisposed();
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, rotation, Center - DrawOffset, scale, effects, 0);
        }

        public void DrawCentered(Vector2 position, Color color, Vector2 scale)
        {
            CheckDisposed();
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, 0, Center - DrawOffset, scale, SpriteEffects.None, 0);
        }

        public void DrawCentered(Vector2 position, Color color, Vector2 scale, float rotation)
        {
            CheckDisposed();
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, rotation, Center - DrawOffset, scale, SpriteEffects.None, 0);
        }

        public void DrawCentered(Vector2 position, Color color, Vector2 scale, float rotation, SpriteEffects effects)
        {
            CheckDisposed();
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, rotation, Center - DrawOffset, scale, effects, 0);
        }

        public void DrawJustified(Vector2 position, Vector2 justify)
        {
            CheckDisposed();
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, Color.White, 0, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, 1f, SpriteEffects.None, 0);
        }

        public void DrawJustified(Vector2 position, Vector2 justify, Color color)
        {
            CheckDisposed();
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, 0, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, 1f, SpriteEffects.None, 0);
        }

        public void DrawJustified(Vector2 position, Vector2 justify, Color color, float scale)
        {
            CheckDisposed();
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, 0, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, SpriteEffects.None, 0);
        }

        public void DrawJustified(Vector2 position, Vector2 justify, Color color, float scale, float rotation)
        {
            CheckDisposed();
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, rotation, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, SpriteEffects.None, 0);
        }

        public void DrawJustified(Vector2 position, Vector2 justify, Color color, float scale, float rotation, SpriteEffects effects)
        {
            CheckDisposed();
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, rotation, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, effects, 0);
        }

        public void DrawJustified(Vector2 position, Vector2 justify, Color color, Vector2 scale)
        {
            CheckDisposed();
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, 0, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, SpriteEffects.None, 0);
        }

        public void DrawJustified(Vector2 position, Vector2 justify, Color color, Vector2 scale, float rotation)
        {
            CheckDisposed();
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, rotation, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, SpriteEffects.None, 0);
        }

        public void DrawJustified(Vector2 position, Vector2 justify, Color color, Vector2 scale, float rotation, SpriteEffects effects)
        {
            CheckDisposed();
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, rotation, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, effects, 0);
        }

        public void DrawOutline(Vector2 position)
        {
            CheckDisposed();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        Util.Draw.SpriteBatch.Draw(Texture, position + new Vector2(i, j), Bounds, Color.Black, 0, -DrawOffset, 1f, SpriteEffects.None, 0);
                    }
                }
            }
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, Color.White, 0, -DrawOffset, 1f, SpriteEffects.None, 0);
        }

        public void DrawOutline(Vector2 position, Vector2 origin)
        {
            CheckDisposed();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        Util.Draw.SpriteBatch.Draw(Texture, position + new Vector2(i, j), Bounds, Color.Black, 0, origin - DrawOffset, 1f, SpriteEffects.None, 0);
                    }
                }
            }
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, Color.White, 0, origin - DrawOffset, 1f, SpriteEffects.None, 0);
        }

        public void DrawOutline(Vector2 position, Vector2 origin, Color color)
        {
            CheckDisposed();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        Util.Draw.SpriteBatch.Draw(Texture, position + new Vector2(i, j), Bounds, Color.Black, 0, origin - DrawOffset, 1f, SpriteEffects.None, 0);
                    }
                }
            }
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, 0, origin - DrawOffset, 1f, SpriteEffects.None, 0);
        }

        public void DrawOutline(Vector2 position, Vector2 origin, Color color, float scale)
        {
            CheckDisposed();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        Util.Draw.SpriteBatch.Draw(Texture, position + new Vector2(i, j), Bounds, Color.Black, 0, origin - DrawOffset, scale, SpriteEffects.None, 0);
                    }
                }
            }
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, 0, origin - DrawOffset, scale, SpriteEffects.None, 0);
        }

        public void DrawOutline(Vector2 position, Vector2 origin, Color color, float scale, float rotation)
        {
            CheckDisposed();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        Util.Draw.SpriteBatch.Draw(Texture, position + new Vector2(i, j), Bounds, Color.Black, rotation, origin - DrawOffset, scale, SpriteEffects.None, 0);
                    }
                }
            }
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, rotation, origin - DrawOffset, scale, SpriteEffects.None, 0);
        }

        public void DrawOutline(Vector2 position, Vector2 origin, Color color, float scale, float rotation, SpriteEffects effects)
        {
            CheckDisposed();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        Util.Draw.SpriteBatch.Draw(Texture, position + new Vector2(i, j), Bounds, Color.Black, rotation, origin - DrawOffset, scale, effects, 0);
                    }
                }
            }
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, rotation, origin - DrawOffset, scale, effects, 0);
        }

        public void DrawOutline(Vector2 position, Vector2 origin, Color color, Vector2 scale)
        {
            CheckDisposed();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        Util.Draw.SpriteBatch.Draw(Texture, position + new Vector2(i, j), Bounds, Color.Black, 0, origin - DrawOffset, scale, SpriteEffects.None, 0);
                    }
                }
            }
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, 0, origin - DrawOffset, scale, SpriteEffects.None, 0);
        }

        public void DrawOutline(Vector2 position, Vector2 origin, Color color, Vector2 scale, float rotation)
        {
            CheckDisposed();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        Util.Draw.SpriteBatch.Draw(Texture, position + new Vector2(i, j), Bounds, Color.Black, rotation, origin - DrawOffset, scale, SpriteEffects.None, 0);
                    }
                }
            }
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, rotation, origin - DrawOffset, scale, SpriteEffects.None, 0);
        }

        public void DrawOutline(Vector2 position, Vector2 origin, Color color, Vector2 scale, float rotation, SpriteEffects flip)
        {
            CheckDisposed();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        Util.Draw.SpriteBatch.Draw(Texture, position + new Vector2(i, j), Bounds, Color.Black, rotation, origin - DrawOffset, scale, flip, 0);
                    }
                }
            }
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, rotation, origin - DrawOffset, scale, flip, 0);
        }

        public void DrawOutlineCentered(Vector2 position)
        {
            CheckDisposed();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        Util.Draw.SpriteBatch.Draw(Texture, position + new Vector2(i, j), Bounds, Color.Black, 0, Center - DrawOffset, 1f, SpriteEffects.None, 0);
                    }
                }
            }
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, Color.White, 0, Center - DrawOffset, 1f, SpriteEffects.None, 0);
        }

        public void DrawOutlineCentered(Vector2 position, Color color)
        {
            CheckDisposed();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        Util.Draw.SpriteBatch.Draw(Texture, position + new Vector2(i, j), Bounds, Color.Black, 0, Center - DrawOffset, 1f, SpriteEffects.None, 0);
                    }
                }
            }
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, 0, Center - DrawOffset, 1f, SpriteEffects.None, 0);
        }

        public void DrawOutlineCentered(Vector2 position, Color color, float scale)
        {
            CheckDisposed();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        Util.Draw.SpriteBatch.Draw(Texture, position + new Vector2(i, j), Bounds, Color.Black, 0, Center - DrawOffset, scale, SpriteEffects.None, 0);
                    }
                }
            }
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, 0, Center - DrawOffset, scale, SpriteEffects.None, 0);
        }

        public void DrawOutlineCentered(Vector2 position, Color color, float scale, float rotation)
        {
            CheckDisposed();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        Util.Draw.SpriteBatch.Draw(Texture, position + new Vector2(i, j), Bounds, Color.Black, rotation, Center - DrawOffset, scale, SpriteEffects.None, 0);
                    }
                }
            }
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, rotation, Center - DrawOffset, scale, SpriteEffects.None, 0);
        }

        public void DrawOutlineCentered(Vector2 position, Color color, float scale, float rotation, SpriteEffects flip)
        {
            CheckDisposed();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        Util.Draw.SpriteBatch.Draw(Texture, position + new Vector2(i, j), Bounds, Color.Black, rotation, Center - DrawOffset, scale, flip, 0);
                    }
                }
            }
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, rotation, Center - DrawOffset, scale, flip, 0);
        }

        public void DrawOutlineCentered(Vector2 position, Color color, Vector2 scale)
        {
            CheckDisposed();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        Util.Draw.SpriteBatch.Draw(Texture, position + new Vector2(i, j), Bounds, Color.Black, 0, Center - DrawOffset, scale, SpriteEffects.None, 0);
                    }
                }
            }
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, 0, Center - DrawOffset, scale, SpriteEffects.None, 0);
        }

        public void DrawOutlineCentered(Vector2 position, Color color, Vector2 scale, float rotation)
        {
            CheckDisposed();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        Util.Draw.SpriteBatch.Draw(Texture, position + new Vector2(i, j), Bounds, Color.Black, rotation, Center - DrawOffset, scale, SpriteEffects.None, 0);
                    }
                }
            }
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, rotation, Center - DrawOffset, scale, SpriteEffects.None, 0);
        }


        public void DrawOutlineCentered(Vector2 position, Color color, Vector2 scale, float rotation, SpriteEffects flip)
        {
            CheckDisposed();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        Util.Draw.SpriteBatch.Draw(Texture, position + new Vector2(i, j), Bounds, Color.Black, rotation, Center - DrawOffset, scale, flip, 0);
                    }
                }
            }
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, rotation, Center - DrawOffset, scale, flip, 0);
        }

        public void DrawOutlineJustified(Vector2 position, Vector2 justify)
        {
            CheckDisposed();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        Util.Draw.SpriteBatch.Draw(Texture, position + new Vector2(i, j), Bounds, Color.Black, 0, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, 1f, SpriteEffects.None, 0);
                    }
                }
            }
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, Color.White, 0, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, 1f, SpriteEffects.None, 0);
        }

        public void DrawOutlineJustified(Vector2 position, Vector2 justify, Color color)
        {
            CheckDisposed();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        Util.Draw.SpriteBatch.Draw(Texture, position + new Vector2(i, j), Bounds, Color.Black, 0, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, 1f, SpriteEffects.None, 0);
                    }
                }
            }
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, 0, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, 1f, SpriteEffects.None, 0);
        }

        public void DrawOutlineJustified(Vector2 position, Vector2 justify, Color color, float scale)
        {
            CheckDisposed();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        Util.Draw.SpriteBatch.Draw(Texture, position + new Vector2(i, j), Bounds, Color.Black, 0, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, SpriteEffects.None, 0);
                    }
                }
            }
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, 0, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, SpriteEffects.None, 0);
        }

        public void DrawOutlineJustified(Vector2 position, Vector2 justify, Color color, float scale, float rotation)
        {
            CheckDisposed();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        Util.Draw.SpriteBatch.Draw(Texture, position + new Vector2(i, j), Bounds, Color.Black, rotation, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, SpriteEffects.None, 0);
                    }
                }
            }
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, rotation, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, SpriteEffects.None, 0);
        }

        public void DrawOutlineJustified(Vector2 position, Vector2 justify, Color color, float scale, float rotation, SpriteEffects flip)
        {
            CheckDisposed();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        Util.Draw.SpriteBatch.Draw(Texture, position + new Vector2(i, j), Bounds, Color.Black, rotation, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, flip, 0);
                    }
                }
            }
            Util.Draw.SpriteBatch.Draw(Texture, position, Bounds, color, rotation, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, flip, 0);
        }

        [Conditional("DEBUG")]
        private void CheckDisposed()
        {
            if (Texture.IsDisposed)
            {
                throw new InvalidOperationException("Cannot draw sprite, texture is disposed.");
            }
        }

        private void SetUtil()
        {
            Center = new Vector2(Width, Height) * 0.5f;
        }
    }
}
