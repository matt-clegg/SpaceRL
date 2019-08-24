using Microsoft.Xna.Framework.Graphics;
using Monocle.Util;

namespace Monocle.Renderers
{
    public class EverythingRenderer : Renderer
    {
        public EverythingRenderer()
        {
            BlendState = BlendState.AlphaBlend;
            SamplerState = SamplerState.PointClamp;
            Camera = new Camera();
        }

        public BlendState BlendState { get; set; }
        public SamplerState SamplerState { get; set; }
        public Effect Effect { get; set; }
        public Camera Camera { get; }

        public override void Render(Scene scene)
        {
            Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState, SamplerState, DepthStencilState.None, RasterizerState.CullNone, Effect, Camera.Matrix * Engine.ScreenMatrix);
            scene.Entities.Render();
            Draw.SpriteBatch.End();
        }
    }
}
