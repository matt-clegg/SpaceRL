using Microsoft.Xna.Framework;
using SpaceRL.Engine.Util;

namespace SpaceRL.Engine
{
    public abstract class Entity
    {
        private Vector2 _position;
        private int _depth;

        public Entity(Vector2? position)
        {
            Position = position ?? Vector2.Zero;
        }

        public bool IsActive { get; set; }
        public bool IsVisible { get; set; }

        public double ActualDepth { get; set; }

        public int Depth {
            get => _depth;
            set {
                if (_depth != value)
                {
                    _depth = value;
                    Scene?.SetActualDepth(this);
                }
            }
        }

        public Vector2 Position {
            get => _position;
            set => _position = value;
        }

        public float X {
            get => _position.X;
            set => _position.X = value;
        }

        public float Y {
            get => _position.Y;
            set => _position.Y = value;
        }

        public Scene Scene { get; private set; }

        public virtual void SceneBegin(Scene scene)
        {

        }

        public virtual void SceneEnd(Scene scene)
        {

        }

        public virtual void Awake(Scene scene)
        {

        }

        public virtual void Added(Scene scene)
        {
            Scene = scene;
            Scene.SetActualDepth(this);
        }

        public virtual void Removed(Scene scene)
        {
            Scene = null;
        }

        public virtual void Update()
        {

        }

        public virtual void Render()
        {

        }

        public virtual void DebugRender(Camera camera)
        {

        }

        public virtual void HandleGraphicsCreate()
        {

        }

        public virtual void HandleGraphicsReset()
        {

        }

        public void RemoveSelf()
        {
            Scene?.Entities.Remove(this);
        }

        public Entity Closest(params Entity[] entities)
        {
            Entity closest = entities[0];
            float dist = Vector2.DistanceSquared(Position, closest.Position);

            for (int i = 1; i < entities.Length; i++)
            {
                float current = Vector2.DistanceSquared(Position, entities[i].Position);
                if (current < dist)
                {
                    closest = entities[i];
                    dist = current;
                }
            }

            return closest;
        }

        public T SceneAs<T>() where T : Scene
        {
            return Scene as T;
        }
    }
}
