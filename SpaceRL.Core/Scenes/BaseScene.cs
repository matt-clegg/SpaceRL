using Monocle;
using Monocle.Renderers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRL.Core.Scenes
{
    public class BaseScene : Scene
    {
        public BaseScene()
        {
            Add(new EverythingRenderer());
        }
    }
}
