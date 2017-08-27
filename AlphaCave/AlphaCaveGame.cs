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

        public SimulationComponent SimulationComponent { get; private set; }
        
        public AlphaCaveGame()
        {
            ScreenManager = new ScreenManager(this);
            
            SimulationComponent = new SimulationComponent(this);
            
            Components.Add(SimulationComponent);
            Components.Add(ScreenManager);
        }

        public override void LoadContent()
        {
            base.LoadContent();
            ScreenManager.NavigateToScreen(new LoadingScreen(ScreenManager));
        }

    }
}
