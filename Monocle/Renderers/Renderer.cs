namespace Monocle.Renderers
{
    public abstract class Renderer
    {
        public bool IsVisible { get; set; } = true;

        public virtual void Update(Scene scene) { }
        public virtual void BeforeRender(Scene scene) { }
        public virtual void Render(Scene scene) { }
        public virtual void AfterRender(Scene scene) { }
    }
}
