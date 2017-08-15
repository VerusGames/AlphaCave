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
using AlphaCave.Components;
using AlphaCave.UI.Screens;

namespace AlphaCave
{
    public class AlphaCaveGame : Game
    {
        public ScreenManager ScreenManager { get; private set; }

        public AlphaCaveGame()
        {
            ScreenManager = new ScreenManager(this);
            Components.Add(ScreenManager);
        }

        public override void LoadContent()
        {
            base.LoadContent();
            ScreenManager.NavigateToScreen(new MainScreen(ScreenManager));
        }

    }
}
