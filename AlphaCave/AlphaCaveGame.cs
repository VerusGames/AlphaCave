using AlphaCave.Core;
using AlphaCave.Core.Maps;
using engenious;
using engenious.Graphics;
using System;
using System.Collections.Generic;
using Bitmap = System.Drawing.Bitmap;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCave
{
    public class AlphaCaveGame : Game
    {
        private SpriteBatch batch;
        private World world;

        private Texture2D texture;


        public override void LoadContent()
        {
            base.LoadContent();

            var bitMap = (Bitmap)Bitmap.FromFile("Spritesheets/TileSheetOutdoor.png");

            texture = Texture2D.FromBitmap(GraphicsDevice,bitMap);

            world = World.CreateDebugWorld();

            batch = new SpriteBatch(GraphicsDevice);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            GraphicsDevice.Clear(ClearBufferMask.ColorBufferBit);


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

                    var color = Color.Red * 0.8f;

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
