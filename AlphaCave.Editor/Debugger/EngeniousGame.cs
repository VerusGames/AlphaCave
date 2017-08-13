using AlphaCave.Core;
using engenious;
using engenious.Graphics;
using engenious.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bitmap = System.Drawing.Bitmap;

namespace AlphaCave.Editor.Debugger
{
    public class EngeniousGame : Game
    {

        Texture2D[] sideTextures;


        SpriteBatch batch;

        Index2 size;
        int height;

        int side = 0;

        public void ShowStructure(Index2 size, int height, Bitmap northSide, Bitmap eastSide, Bitmap southSide, Bitmap westSide)
        {
            batch = new SpriteBatch(GraphicsDevice);
            this.size = size;
            this.height = height;

            sideTextures = new Texture2D[4];

            sideTextures[0] = Texture2D.FromBitmap(GraphicsDevice, northSide);
            sideTextures[1] = Texture2D.FromBitmap(GraphicsDevice, eastSide);
            sideTextures[2] = Texture2D.FromBitmap(GraphicsDevice, southSide);
            sideTextures[3] = Texture2D.FromBitmap(GraphicsDevice, westSide);



            Run();
        }

        public override void Update(GameTime gameTime)
        {
            var keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.D))
                side++;

            if (keyboard.IsKeyDown(Keys.A))
                side--;

            if (side < 0)
                side = 3;

            side %= 4;
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGray);

            batch.Begin();

            var texture = sideTextures[side];
            batch.Draw(texture, new Rectangle(100,100, texture.Width, texture.Height), Color.White);

            batch.End();
        }

    }
}
