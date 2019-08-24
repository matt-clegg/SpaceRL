using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Runtime;

namespace SpaceRL.Engine
{
    public class Engine : Game
    {
        public static Matrix ScreenMatrix;

        public static float TimeRate = DefaultTimeRate;
        public static float FreezeTimer;
        public static int FPS;

        public static Action OverloadGameLoop;

        private static int _viewPadding;
        private static bool _resizing;

        private const float DefaultTimeRate = 1f;

        private Scene _scene;
        private Scene _nextScene;

        private TimeSpan _counterElapsed = TimeSpan.Zero;
        private int _fpsCounter;

        public Engine(int width, int height, int windowWidth, int windowHeight, string title, bool fullscreen)
        {
            Instance = this;

            Title = Window.Title = title;
            Width = width;
            Height = height;
            ClearColor = Color.Black;

            Graphics = new GraphicsDeviceManager(this)
            {
                SynchronizeWithVerticalRetrace = true,
                PreferMultiSampling = false,
                GraphicsProfile = GraphicsProfile.HiDef,
                PreferredBackBufferFormat = SurfaceFormat.Color,
                PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8
            };

            Graphics.DeviceReset += OnGraphicsReset;
            Graphics.DeviceCreated += OnGraphicsCreate;

#if PS4 || XBOXONE
      Graphics.PreferredBackBufferWidth = 1920;
      Graphics.PreferredBackBufferHeight = 1080;
#elif NSWITCH
      Graphics.PreferredBackBufferWidth = 1280;
      Graphics.PreferredBackBufferHeight = 720;
#else
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += OnClientSizeChanged;

            if (fullscreen)
            {
                Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                Graphics.IsFullScreen = true;
            }
            else
            {
                Graphics.PreferredBackBufferWidth = windowWidth;
                Graphics.PreferredBackBufferHeight = windowHeight;
                Graphics.IsFullScreen = false;
            }
#endif

            Graphics.ApplyChanges();

            IsMouseVisible = true;
            IsFixedTimeStep = false;

            GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;
        }

        public string Title { get; }
        public static Color ClearColor { get; set; }

        public static Engine Instance { get; private set; }
        public static GraphicsDeviceManager Graphics { get; private set; }

        public static Viewport Viewport { get; private set; }

        public static int Width { get; private set; }
        public static int Height { get; private set; }
        public static int ViewWidth { get; private set; }
        public static int ViewHeight { get; private set; }
        public static int ViewPadding {
            get => _viewPadding;
            set {
                _viewPadding = value;
                Instance.UpdateView();
            }
        }

        public static float DeltaTime { get; private set; }
        public static float RawDeltaTime { get; private set; }

        public static Scene Scene {
            get => Instance._scene;
            set => Instance._nextScene = value;
        }

        public static void SetWindowed(int width, int height)
        {
#if !CONSOLE
            if (width > 0 && height > 0)
            {
                _resizing = true;
                Graphics.PreferredBackBufferWidth = width;
                Graphics.PreferredBackBufferHeight = height;
                Graphics.IsFullScreen = false;
                Graphics.ApplyChanges();
                _resizing = false;
            }
#endif
        }

        public static void SetFullscreen()
        {
#if !CONSOLE
            _resizing = true;
            Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            Graphics.IsFullScreen = true;
            Graphics.ApplyChanges();
            _resizing = false;
#endif
        }

        protected virtual void OnGraphicsReset(object sender, EventArgs e)
        {
            UpdateView();

            _scene?.HandleGraphicsReset();
            _nextScene?.HandleGraphicsReset();
        }

        protected virtual void OnGraphicsCreate(object sender, EventArgs e)
        {
            UpdateView();

            _scene?.HandleGraphicsCreate();
            _nextScene?.HandleGraphicsCreate();
        }

        protected override void OnActivated(object sender, EventArgs args)
        {
            base.OnActivated(sender, args);
            _scene?.GainFocus();
        }

        protected override void OnDeactivated(object sender, EventArgs args)
        {
            base.OnDeactivated(sender, args);
            _scene?.LoseFocus();
        }

        protected override void Initialize()
        {
            base.Initialize();

            // todo: init
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            Util.Draw.Initialize(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            RawDeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            DeltaTime = RawDeltaTime * TimeRate;

            // update input

            if (OverloadGameLoop != null)
            {
                OverloadGameLoop();
                base.Update(gameTime);
                return;
            }

            if (FreezeTimer > 0)
            {
                FreezeTimer = Math.Max(FreezeTimer - RawDeltaTime, 0);
            }
            else if (_scene != null)
            {
                _scene.BeforeUpdate();
                _scene.Update();
                _scene.AfterUpdate();
            }

            if (_scene != _nextScene)
            {
                Scene lastScene = _scene;
                _scene?.End();
                _scene = _nextScene;
                OnSceneTransition(lastScene, _nextScene);
                _scene?.Begin();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            RenderCore();

            base.Draw(gameTime);
            _fpsCounter++;
            _counterElapsed += gameTime.ElapsedGameTime;

            if (_counterElapsed >= TimeSpan.FromSeconds(1))
            {
                Window.Title = $"{Title} {_fpsCounter} fps - {GC.GetTotalMemory(false) / 1048576f:F} MB";
                FPS = _fpsCounter;
                _fpsCounter = 0;
                _counterElapsed -= TimeSpan.FromSeconds(1);
            }
        }

        protected virtual void RenderCore()
        {
            _scene?.BeforeRender();

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Viewport = Viewport;
            GraphicsDevice.Clear(ClearColor);

            if (_scene != null)
            {
                _scene.Render();
                _scene.AfterRender();
            }
        }

#if !CONSOLE
        protected virtual void OnClientSizeChanged(object sender, EventArgs e)
        {
            if (Window.ClientBounds.Width > 0 && Window.ClientBounds.Height > 0 && !_resizing)
            {
                _resizing = true;

                Graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
                Graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
                UpdateView();

                _resizing = false;
            }
        }
#endif

        protected virtual void OnSceneTransition(Scene from, Scene to)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            TimeRate = 1f;
        }

        private void UpdateView()
        {
            float screenWidth = GraphicsDevice.PresentationParameters.BackBufferWidth;
            float screenHeight = GraphicsDevice.PresentationParameters.BackBufferHeight;

            if (screenWidth / Width > screenHeight / Height)
            {
                ViewWidth = (int)(screenHeight / Height * Width);
                ViewHeight = (int)screenHeight;
            }
            else
            {
                ViewWidth = (int)screenWidth;
                ViewHeight = (int)(screenWidth / Width * Height);
            }

            float aspect = ViewHeight / (float)ViewWidth;
            ViewWidth -= ViewPadding * 2;
            ViewHeight -= (int)(aspect * ViewPadding * 2);

            ScreenMatrix = Matrix.CreateScale(ViewWidth / (float)Width);

            Viewport = new Viewport
            {
                X = (int)(screenWidth / 2 - ViewWidth / 2f),
                Y = (int)(screenHeight / 2 - ViewHeight / 2f),
                Width = ViewWidth,
                Height = ViewHeight,
                MinDepth = 0,
                MaxDepth = 1
            };
        }
    }
}
