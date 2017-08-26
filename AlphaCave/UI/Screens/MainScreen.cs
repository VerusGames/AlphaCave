using AlphaCave.Components;
using AlphaCave.UI.Controls;
using EngeniousUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCave.UI.Screens
{
    public class MainScreen : Screen
    {
        public MainScreen(ScreenManager manager) : base(manager)
        {
            var gameControl = new GameControl(manager);
            gameControl.HorizontalAlignment = HorizontalAlignment.Stretch;
            gameControl.VerticalAlignment = VerticalAlignment.Stretch;
            Controls.Add(gameControl);
        }
    }
}
