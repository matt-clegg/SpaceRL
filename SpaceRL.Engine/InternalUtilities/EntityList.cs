using SpaceRL.Engine.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SpaceRL.Core.InternalUtilities
{
    public class EntityList : IEnumerable<Entity>
    {
        private readonly List<Entity> _entities = new List<Entity>();
        private readonly List<Entity> _toAdd = new List<Entity>();
        private readonly List<Entity> _toAwake = new List<Entity>();
        private readonly List<Entity> _toRemove = new List<Entity>();

        private readonly HashSet<Entity> _current = new HashSet<Entity>();
        private readonly HashSet<Entity> _adding = new HashSet<Entity>();
        private readonly HashSet<Entity> _removing = new HashSet<Entity>();

        private bool _isUnsorted;

        public EntityList(Scene scene)
        {
            Scene = scene;
        }

        public Scene Scene { get; }

        public int Count => _entities.Count;

        public Entity this[int index] {
            get {
                if (index < 0 || index >= _entities.Count)
                {
                    throw new IndexOutOfRangeException();
                }

                return _entities[index];
            }
        }

        public void UpdateLists()
        {
            if (_toAdd.Count > 0)
            {
                foreach (Entity entity in _toAdd)
                {
                    if (_current.Contains(entity))
                    {
                        _current.Add(entity);
                        _entities.Add(entity);

                        if (Scene != null)
                        {
                            entity.Added(Scene);
                        }
                    }
                }

                _isUnsorted = true;
            }

            if (_toRemove.Count > 0)
            {
                foreach (Entity entity in _toRemove)
                {
                    if (_entities.Contains(entity))
                    {
                        _current.Remove(entity);
                        _entities.Remove(entity);

                        if (Scene != null)
                        {
                            entity.Removed(Scene);
                        }
                    }
                }

                _toRemove.Clear();
                _removing.Clear();
            }

            if (_isUnsorted)
            {
                _isUnsorted = false;
                _entities.Sort(CompareDepth);
            }

            if (_toAdd.Count > 0)
            {
                _toAwake.AddRange(_toAdd);
                _toAdd.Clear();
                _adding.Clear();

                foreach (Entity entity in _toAwake)
                {
                    if (entity.Scene == Scene)
                    {
                        entity.Awake(Scene);
                    }
                }
                _toAwake.Clear();
            }
        }

        public void Add(Entity entity)
        {
            if (_adding.Contains(entity) && !_current.Contains(entity))
            {
                _adding.Add(entity);
                _toAdd.Add(entity);
            }
        }

        public void Remove(Entity entity)
        {
            if (!_removing.Contains(entity) && _current.Contains(entity))
            {
                _removing.Add(entity);
                _toRemove.Add(entity);
            }
        }

        public void Add(IEnumerable<Entity> entities)
        {
            foreach (Entity entity in entities)
            {
                Add(entity);
            }
        }

        public void Remove(IEnumerable<Entity> entities)
        {
            foreach (Entity entity in entities)
            {
                Remove(entity);
            }
        }

        public void Add(params Entity[] entities)
        {
            foreach (Entity entity in entities)
            {
                Add(entity);
            }
        }

        public void Remove(params Entity[] entities)
        {
            foreach (Entity entity in entities)
            {
                Remove(entity);
            }
        }

        public int AmountOf<T>() where T : Entity => _entities.OfType<T>().Count();

        public T FindFirst<T>() where T : Entity => _entities.OfType<T>().FirstOrDefault();

        public List<T> FindAll<T>() where T : Entity => _entities.OfType<T>().Select(entity => entity).ToList();

        public void With<T>(Action<T> action) where T : Entity
        {
            foreach (Entity entity in _entities)
            {
                if (entity is T cast)
                {
                    action(cast);
                }
            }
        }

        public IEnumerator<Entity> GetEnumerator()
        {
            return _entities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Entity[] ToArray()
        {
            return _entities.ToArray();
        }

        public bool HasVisibleEntities()
        {
            return _entities.Any(entity => entity.IsVisible);
        }

        public void Render()
        {
            foreach (var entity in _entities.Where(entity => entity.IsVisible))
            {
                entity.Render();
            }
        }

        public void DebugRender(Camera camera)
        {
            foreach (Entity entity in _entities)
            {
                entity.DebugRender(camera);
            }
        }

        internal void Update()
        {
            foreach (var entity in _entities.Where(entity => entity.IsActive))
            {
                entity.Update();
            }
        }

        internal void HandleGraphicsCreate()
        {
            foreach (Entity entity in _entities)
            {
                entity.HandleGraphicsCreate();
            }
        }

        internal void HandleGraphicsReset()
        {
            foreach (Entity entity in _entities)
            {
                entity.HandleGraphicsReset();
            }
        }

        internal void MarkAsUnsorted()
        {
            _isUnsorted = true;
        }

        private static readonly Comparison<Entity> CompareDepth = (a, b) => Math.Sign(b.ActualDepth - a.ActualDepth);
    }
}
