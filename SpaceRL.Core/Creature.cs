using Microsoft.Xna.Framework;
using Monocle;
using Monocle.Graphics;
using Monocle.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRL.Core
{
    public class Creature : Entity
    {
        public Sprite Sprite { get; set; }

        public Creature(Vector2? position, Sprite sprite) : base(position)
        {
            Sprite = sprite;
        }

        public override void Added(Scene scene)
        {
            base.Added(scene);
        }

        public override void Awake(Scene scene)
        {
            base.Awake(scene);
        }

        public override void DebugRender(Camera camera)
        {
            base.DebugRender(camera);
        }

        public override void HandleGraphicsCreate()
        {
            base.HandleGraphicsCreate();
        }

        public override void HandleGraphicsReset()
        {
            base.HandleGraphicsReset();
        }

        public override void Removed(Scene scene)
        {
            base.Removed(scene);
        }

        public override void Render()
        {
            base.Render();
            Sprite.Draw(Position);
        }

        public override void SceneBegin(Scene scene)
        {
            base.SceneBegin(scene);
        }

        public override void SceneEnd(Scene scene)
        {
            base.SceneEnd(scene);
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
