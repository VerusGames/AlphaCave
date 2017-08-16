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

namespace AlphaCave.UI.Controls
{
    public class GameControl : Control
    {
        private SpriteBatch batch;
        private World world;

        private Texture2D texture;

        private ScreenManager manager;

        private ChunkFloorRenderer _chunkRenderer;
        private BottomWallRenderer _wallRenderer;

        public GameControl(ScreenManager manager, string style = "") : base(manager, style)
        {
            this.manager = manager;

            //texture = AssetManager.Instance.Spritesheets["TileSheetDungeon"];

            world = World.CreateDebugWorld();

            var floor = world.Floors.First();

            var chunk = floor.GetChunk(0, 0);

            _chunkRenderer = new ChunkFloorRenderer(manager.Game, AssetManager.Instance.Spritesheets["TileSheetDungeon"], chunk);
            _wallRenderer = new BottomWallRenderer(manager.Game, AssetManager.Instance.Spritesheets["TileSheetDungeon"], chunk);

            batch = new SpriteBatch(manager.GraphicsDevice);
        }
        private RenderTarget2D ControlTexture;

        private int moveX, moveY;
        protected override void OnUpdate(GameTime gameTime)
        {
            base.OnUpdate(gameTime);

            moveX += xAdd;
            moveY += yAdd;
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
            manager.GraphicsDevice.Clear(ClearBufferMask.ColorBufferBit);//TODO not necessary with next engenious

            _chunkRenderer.Render(manager.GraphicsDevice, Matrix.Identity * Matrix.CreateScaling(new Vector3(2)), Matrix.CreateOrthographicOffCenter(0 - moveX, ControlTexture.Width-moveX, 0+moveY, ControlTexture.Height+moveY, -0.1f, 1));
            _wallRenderer.Render(manager.GraphicsDevice, Matrix.Identity * Matrix.CreateScaling(new Vector3(2)), Matrix.CreateOrthographicOffCenter(0 - moveX, ControlTexture.Width - moveX, 0 + moveY, ControlTexture.Height + moveY, -0.1f, 1));

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



            /*batch.Begin();

            var floor = world.Floors.First();

            var chunk = floor.GetChunk(0, 0);

            for (int x = 0; x < Chunk.CHUNKSIZE_X; x++)
            {
                for (int y = 0; y < Chunk.CHUNKSIZE_Y; y++)
                {
                    var index = new Index2(x, y);

                    var tile = chunk.GetTile(index);

                    var flag = chunk.GetFlags(index);

                    Rectangle dest = new Rectangle(x * 32, y * 32, 32, 32);


                    if (flag.HasFlag(TileFlags.Visible))
                    {
                        //Rectangle src = new Rectangle(13 * 17, 4 * 17, 16, 16);
                        //batch.Draw(texture, dest, src, Color.White);
                        batch.Draw(TileRectangle.StoneFloor.Texture, dest, TileRectangle.StoneFloor.Rectangle, Color.White);
                    }
                    /*else if(flag.HasFlag(TileFlags.PreVisible))
                    {
                        var down = chunk.GetFlags(new Index2(x, y + 1));

                        Rectangle destUp = new Rectangle(x * 32, (y-1) * 32, 32, 32);
                        Rectangle destUpUp = new Rectangle(x * 32, (y - 2) * 32, 32, 32);

                        if (down.HasFlag(TileFlags.Visible))
                        {
                            batch.Draw(TileRectangle.StoneFloor.Texture, dest, TileRectangle.StoneFloor.Rectangle, Color.White);
                            batch.Draw(TileRectangle.WallUpLower.Texture, dest, TileRectangle.WallUpLower.Rectangle, Color.White);

                            batch.Draw(TileRectangle.WallUpUpper.Texture, destUp, TileRectangle.WallUpUpper.Rectangle, Color.White);
                            batch.Draw(TileRectangle.WallTop.Texture, destUpUp, TileRectangle.WallTop.Rectangle, Color.White);
                        }
                        else
                        {
                            batch.Draw(TileRectangle.DirtFloor.Texture, dest, TileRectangle.DirtFloor.Rectangle, Color.Gray);
                        }

                        
                    }
                    else
                    {

                    }


                }
            }

            batch.End();*/
        }

        bool isDown = false;
        protected override void OnLeftMouseDown(MouseEventArgs args)
        {
            base.OnLeftMouseClick(args);

            isDown = true;

            int xPos = args.LocalPosition.X / 32;
            int yPos = args.LocalPosition.Y / 32;

            int chunkX = xPos / 64;
            int chunkY = yPos / 64;

            xPos = (xPos % 64) + 1;
            yPos = (yPos % 64) + 1;

            world.Floors.First().GetChunk((short)chunkX, (short)chunkY).SetVisible(new Index2(xPos, yPos));
            _chunkRenderer.ReloadChunk();
            _wallRenderer.ReloadChunk();
        }

        protected override void OnMouseMove(MouseEventArgs args)
        {
            base.OnMouseMove(args);

            if (isDown)
            {

                int xPos = args.LocalPosition.X / 32;
                int yPos = args.LocalPosition.Y / 32;

                int chunkX = xPos / 64;
                int chunkY = yPos / 64;

                xPos = (xPos % 64) + 1;
                yPos = (yPos % 64) + 1;

                world.Floors.First().GetChunk((short)chunkX, (short)chunkY).SetVisible(new Index2(xPos, yPos));
                _chunkRenderer.ReloadChunk();
                _wallRenderer.ReloadChunk();
            }
        }

        protected override void OnLeftMouseUp(MouseEventArgs args)
        {
            base.OnLeftMouseUp(args);

            isDown = false;

        }

        int xAdd = 0, yAdd = 0;

        protected override void OnKeyPress(KeyEventArgs args)
        {
            base.OnKeyDown(args);

            switch(args.Key)
            {
                case engenious.Input.Keys.A:
                    xAdd = -1; break;
                case engenious.Input.Keys.D:
                    xAdd = +1; break;
                case engenious.Input.Keys.W:
                    yAdd = +1; break;
                case engenious.Input.Keys.S:
                    yAdd = -1; break;
            }
        }

        protected override void OnKeyUp(KeyEventArgs args)
        {
            base.OnKeyUp(args);
            switch (args.Key)
            {
                case engenious.Input.Keys.A:
                    xAdd = 0; break;
                case engenious.Input.Keys.D:
                    xAdd = 0; break;
                case engenious.Input.Keys.W:
                    yAdd = 0; break;
                case engenious.Input.Keys.S:
                    yAdd = 0; break;
            }
        }
    }
}
