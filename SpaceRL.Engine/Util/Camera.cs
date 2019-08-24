using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SpaceRL.Engine.Util
{
    public class Camera
    {
        private Matrix _matrix = Matrix.Identity;
        private Matrix _inverse = Matrix.Identity;
        private bool _changed;

        private Vector2 _position = Vector2.Zero;
        private Vector2 _zoom = Vector2.One;
        private Vector2 _origin = Vector2.Zero;
        private float _angle;

        public Camera() : this(Engine.Width, Engine.Height)
        {

        }

        public Camera(int width, int height)
        {
            Viewport = new Viewport
            {
                Width = width,
                Height = height
            };
            UpdateMatrices();
        }

        public Viewport Viewport { get; set; }

        public Matrix Matrix {
            get {
                if (_changed)
                {
                    UpdateMatrices();
                }
                return _matrix;
            }
        }

        public Matrix Inverse {
            get {
                if (_changed)
                {
                    UpdateMatrices();
                }
                return _inverse;
            }
        }

        public Vector2 Position {
            get => _position;
            set {
                _changed = true;
                _position = value;
            }
        }

        public Vector2 Origin {
            get => _origin;
            set {
                _changed = true;
                _origin = value;
            }
        }

        public float X {
            get => _position.X;
            set {
                _changed = true;
                _position.X = value;
            }
        }

        public float Y {
            get => _position.Y;
            set {
                _changed = true;
                _position.Y = value;
            }
        }

        public float Zoom {
            get => _zoom.X;
            set {
                _changed = true;
                _zoom.X = _zoom.Y = value;
            }
        }

        public float Angle {
            get => _angle;
            set {
                _changed = true;
                _angle = value;
            }
        }

        private void UpdateMatrices()
        {
            _matrix = Matrix.Identity *
                      Matrix.CreateTranslation(new Vector3(-new Vector2((int)Math.Floor(_position.X), (int)Math.Floor(_position.Y)), 0)) *
                      Matrix.CreateRotationZ(_angle) *
                      Matrix.CreateScale(new Vector3(_zoom, 1f)) *
                      Matrix.CreateTranslation(new Vector3(new Vector2((int)Math.Floor(_origin.X), (int)Math.Floor(_origin.Y)), 0));

            _inverse = Matrix.Invert(_matrix);
            _changed = false;
        }

        public void CopyFrom(Camera other)
        {
            _position = other._position;
            _origin = other._origin;
            _angle = other._angle;
            _zoom = other._zoom;
            _changed = true;
        }


        public void CenterOrigin()
        {
            _origin = new Vector2((float)Viewport.Width / 2, (float)Viewport.Height / 2);
            _changed = true;
        }

        public void RoundPosition()
        {
            _position.X = (float)Math.Round(_position.X);
            _position.Y = (float)Math.Round(_position.Y);
            _changed = true;
        }

        public Vector2 ScreenToCamera(Vector2 position)
        {
            return Vector2.Transform(position, Inverse);
        }

        public Vector2 CameraToScreen(Vector2 position)
        {
            return Vector2.Transform(position, Matrix);
        }

        public void Approach(Vector2 position, float ease)
        {
            Position += (position - Position) * ease;
        }

        public void Approach(Vector2 position, float ease, float maxDistance)
        {
            Vector2 move = (position - Position) * ease;
            if (move.Length() > maxDistance)
            {
                Position += Vector2.Normalize(move) * maxDistance;
            }
            else
            {
                Position += move;
            }
        }
    }
}
