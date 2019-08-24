using Monocle.Renderers;
using Monocle.Util;
using System.Collections.Generic;

namespace Monocle.InternalUtilities
{
    public class RendererList
    {
        private readonly List<Renderer> _adding = new List<Renderer>();
        private readonly List<Renderer> _removing = new List<Renderer>();

        private readonly Scene _scene;

        public RendererList(Scene scene)
        {
            _scene = scene;

            Renderers = new List<Renderer>();
        }

        public List<Renderer> Renderers { get; }

        public void MoveToFront(Renderer renderer)
        {
            Renderers.Remove(renderer);
            Renderers.Add(renderer);
        }

        public void Add(Renderer renderer)
        {
            _adding.Add(renderer);
        }

        public void Remove(Renderer renderer)
        {
            _removing.Add(renderer);
        }

        internal void UpdateLists()
        {
            if (_adding.Count > 0)
            {
                foreach (Renderer renderer in _adding)
                {
                    Renderers.Add(renderer);
                }
                _adding.Clear();
            }

            if (_removing.Count > 0)
            {
                foreach (Renderer renderer in _removing)
                {
                    Renderers.Remove(renderer);
                }
                _removing.Clear();
            }
        }

        internal void Update()
        {
            foreach (Renderer renderer in Renderers)
            {
                renderer.Update(_scene);
            }
        }

        internal void BeforeRender()
        {
            foreach (Renderer renderer in Renderers)
            {
                if (!renderer.IsVisible)
                {
                    continue;
                }

                Draw.Renderer = renderer;
                renderer.BeforeRender(_scene);
            }
        }

        internal void Render()
        {
            foreach (Renderer renderer in Renderers)
            {
                if (!renderer.IsVisible)
                {
                    continue;
                }

                Draw.Renderer = renderer;
                renderer.Render(_scene);
            }
        }

        internal void AfterRender()
        {
            foreach (Renderer renderer in Renderers)
            {
                if (!renderer.IsVisible)
                {
                    continue;
                }

                Draw.Renderer = renderer;
                renderer.AfterRender(_scene);
            }
        }
    }
}
