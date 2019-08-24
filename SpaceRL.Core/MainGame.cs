using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;
using Monocle.Graphics;
using Monocle.Renderers;
using SpaceRL.Core.Scenes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRL.Core
{
    public class MainGame : Engine
    {
        public MainGame(int width, int height, int windowWidth, int windowHeight, string title, bool fullscreen) 
            : base(width, height, windowWidth, windowHeight, title, fullscreen)
        {

        }

        protected override void Initialize()
        {
            base.Initialize();

            Scene scene = new BaseScene();

            Sprite sheet = new Sprite(Content.Load<Texture2D>("Content/Textures/oryx_16bit_scifi_creatures"));
            Sprite sprite = new Sprite(sheet, 0, 0, 24, 24);
            Creature creature = new Creature(new Vector2(50, 50), sprite);

            scene.Add(creature);
            Scene = scene;
        }

        protected override void LoadContent()
        {
            base.LoadContent();

        }
    }
}
