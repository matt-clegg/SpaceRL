using SpaceRL.Engine.InternalUtilities;
using SpaceRL.Engine.Renderers;
using System;
using System.Collections.Generic;

namespace SpaceRL.Engine
{
    public class Scene
    {
        private readonly Dictionary<int, double> _actualDepthLookup = new Dictionary<int, double>();

        public Scene()
        {
            Entities = new EntityList(this);
            Renderers = new RendererList(this);
        }

        public event Action OnEndOfFrame;

        public bool IsPaused { get; private set; }
        public bool IsFocused { get; private set; }

        public float TimeActive { get; private set; }
        public float RawTimeActive { get; private set; }

        public EntityList Entities { get; private set; }
        public RendererList Renderers { get; private set; }

        public void Begin()
        {
            IsFocused = true;
            foreach (Entity entity in Entities)
            {
                entity.SceneBegin(this);
            }
        }

        public void End()
        {
            IsFocused = false;
            foreach (Entity entity in Entities)
            {
                entity.SceneEnd(this);
            }
        }

        public void BeforeUpdate()
        {
            if (!IsPaused)
            {
                TimeActive += Engine.DeltaTime;
            }
            RawTimeActive += Engine.RawDeltaTime;

            Entities.UpdateLists();
            Renderers.UpdateLists();
        }

        public void Update()
        {
            if (!IsPaused)
            {
                Entities.Update();
                Renderers.Update();
            }
        }

        public void AfterUpdate()
        {
            if (OnEndOfFrame != null)
            {
                OnEndOfFrame();
                OnEndOfFrame = null;
            }
        }

        public void BeforeRender()
        {
            Renderers.BeforeRender();
        }

        public void Render()
        {
            Renderers.Render();
        }

        public void AfterRender()
        {
            Renderers.AfterRender();
        }

        public void HandleGraphicsCreate()
        {
            Entities.HandleGraphicsCreate();
        }

        public void HandleGraphicsReset()
        {
            Entities.HandleGraphicsReset();
        }

        public void Add(Entity entity)
        {
            Entities.Add(entity);
        }

        public void Remove(Entity entity)
        {
            Entities.Remove(entity);
        }

        public void Add(IEnumerable<Entity> entities)
        {
            Entities.Add(entities);
        }

        public void Remove(IEnumerable<Entity> entities)
        {
            Entities.Remove(entities);
        }

        public void Add(params Entity[] entities)
        {
            Entities.Add(entities);
        }

        public void Remove(params Entity[] entities)
        {
            Entities.Remove(entities);
        }

        public void Add(Renderer renderer)
        {
            Renderers.Add(renderer);
        }

        public void Remove(Renderer renderer)
        {
            Renderers.Remove(renderer);
        }

        public virtual void GainFocus()
        {

        }

        public virtual void LoseFocus()
        {

        }

        internal void SetActualDepth(Entity entity)
        {
            const double theta = .000001d;

            if (_actualDepthLookup.TryGetValue(entity.Depth, out double add))
            {
                _actualDepthLookup[entity.Depth] += theta;
            }
            else
            {
                _actualDepthLookup.Add(entity.Depth, theta);
            }

            entity.ActualDepth = entity.Depth - add;

            Entities.MarkAsUnsorted();
        }

    }
}
