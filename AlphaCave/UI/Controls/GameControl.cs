using AlphaCave.Components;
using AlphaCave.Core.Maps;
using engenious.Graphics;
using EngeniousUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious;
using AlphaCave.Core;
using engenious;
using Bitmap = System.Drawing.Bitmap;
using AlphaCave.Map;
using AlphaCave.Renderer;
using engenious.Input;

namespace AlphaCave.UI.Controls
{
    public class GameControl : Control
    {
        private SpriteBatch batch;
        private World world;

        private Texture2D texture;

        private ScreenManager manager;

        private readonly ChunkFloorRenderer chunkRenderer;
        private readonly BottomWallRenderer wallRenderer;
        private readonly CharacterRender characterRender;
        
        int xAdd = 0, yAdd = 0;
        

        public GameControl(ScreenManager manager, string style = "") : base(manager, style)
        {
            this.manager = manager;

            //texture = AssetManager.Instance.Spritesheets["TileSheetDungeon"];

            world = World.CreateDebugWorld();

            var floor = world.Floors.First();

            var chunk = floor.GetChunk(0, 0);

            chunkRenderer = new ChunkFloorRenderer(manager.Game, AssetManager.Instance.Spritesheets["TileSheetDungeon"], chunk);
            wallRenderer = new BottomWallRenderer(manager.Game, AssetManager.Instance.Spritesheets["TileSheetDungeon"], chunk);
            characterRender = new CharacterRender(manager.Game,AssetManager.Instance.Spritesheets["TileSheetCharacter"]);
            
            batch = new SpriteBatch(manager.GraphicsDevice);
        }
        private RenderTarget2D ControlTexture;

        
        protected override void OnUpdate(GameTime gameTime)
        {
            base.OnUpdate(gameTime);

            var simulation = manager.Game.SimulationComponent;
            
            simulation.Player.Position += new Vector2(xAdd,yAdd) * 0.25f;

        }

        protected override void OnPreDraw(GameTime gameTime)
        {
            base.OnPreDraw(gameTime);
            if (ActualClientArea.Width == 0 || ActualClientArea.Height == 0)
                return;
            if (ControlTexture == null || ControlTexture.Width != ActualClientArea.Width || ControlTexture.Height != ActualClientArea.Height)
            {
                ControlTexture?.Dispose();
                ControlTexture = new RenderTarget2D(manager.GraphicsDevice, ActualClientArea.Width, ActualClientArea.Height, PixelInternalFormat.Rgb8);

            }

            manager.GraphicsDevice.SetRenderTarget(ControlTexture);
            manager.GraphicsDevice.Clear(Color.Transparent);

            Matrix projection = Matrix.CreateOrthographicOffCenter(0, ControlTexture.Width , 0 ,
                ControlTexture.Height, -0.1f, 1000);
            Matrix view = Matrix.CreateLookAt(new Vector3(0,0,1000),Vector3.Zero, Vector3.UnitY)*Matrix.CreateScaling(new Vector3(2));
            chunkRenderer.Render(manager.GraphicsDevice, view, projection);
            wallRenderer.Render(manager.GraphicsDevice, view,projection);
            characterRender.Render(manager.GraphicsDevice, view,projection,manager.Game.SimulationComponent.Player);

            manager.GraphicsDevice.SetRenderTarget(null);

            /* using (var bmp = Texture2D.ToBitmap(ControlTexture))
                 bmp.Save("test.png", System.Drawing.Imaging.ImageFormat.Png);*/
        }
        protected override void OnDraw(SpriteBatch batch, Rectangle controlArea, GameTime gameTime)
        {
            base.OnDraw(batch, controlArea, gameTime);

            batch.Begin();

            batch.Draw(ControlTexture, controlArea, Color.White);

            batch.End();
        }
        

        

        protected override void OnKeyPress(KeyEventArgs args)
        {
            OnKeyDown(args);

            switch (args.Key)
            {
                case Keys.A:
                    xAdd = -1; break;
                case Keys.D:
                    xAdd = +1; break;
                case Keys.W:
                    yAdd = +1; break;
                case Keys.S:
                    yAdd = -1; break;
            }
        }

        protected override void OnKeyUp(KeyEventArgs args)
        {
            base.OnKeyUp(args);
            switch (args.Key)
            {
                case Keys.A:
                    xAdd = 0; break;
                case Keys.D:
                    xAdd = 0; break;
                case Keys.W:
                    yAdd = 0; break;
                case Keys.S:
                    yAdd = 0; break;
            }
        }
    }
}
