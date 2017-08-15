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

namespace AlphaCave.UI.Controls
{
    public class GameControl : Control
    {
        private SpriteBatch batch;
        private World world;

        private Texture2D texture;

        private ScreenManager manager;

        public GameControl(ScreenManager manager, string style = "") : base(manager, style)
        {
            this.manager = manager;

            texture = AssetManager.Instance.Spritesheets["TileSheetOutdoor"];

            world = World.CreateDebugWorld();

            batch = new SpriteBatch(manager.GraphicsDevice);
        }

        protected override void OnDraw(SpriteBatch batch, engenious.Rectangle controlArea, GameTime gameTime)
        {
            base.OnDraw(batch, controlArea, gameTime);

            batch.Begin();

            var floor = world.Floors.First();

            var chunk = floor.GetChunk(0, 0);

            for (int x = 0; x < Chunk.CHUNKSIZE_X; x++)
            {
                for (int y = 0; y < Chunk.CHUNKSIZE_Y; y++)
                {
                    var index = new Index2(x, y);

                    var tile = chunk.GetTile(index);

                    var flag = chunk.GetFlags(index);

                    var color = engenious.Color.Red * 0.8f;

                    if (flag.HasFlag(TileFlags.Visible))
                        color = Color.White;

                    Rectangle dest = new Rectangle(x * 32, y * 32, 32, 32);
                    Rectangle src = new Rectangle(5 * 17, 0 * 17, 16, 16);

                    batch.Draw(texture, dest, src, color);
                }
            }

            batch.End();
        }
    }
}
