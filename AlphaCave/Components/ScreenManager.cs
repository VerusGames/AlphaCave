using EngeniousUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious;

namespace AlphaCave.Components
{
    public class ScreenManager : BaseScreenComponent
    {
        public AlphaCaveGame Game { get; private set; }

        public ScreenManager(AlphaCaveGame game) : base(game)
        {
            Game = game;
            TitlePrefix = "AlphaCave";
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            Frame.Background = new BorderBrush(Color.LightGray);
            NavigateFromTransition = new AlphaTransition(Frame, Transition.Linear, TimeSpan.FromMilliseconds(200), 0f);
            NavigateToTransition = new AlphaTransition(Frame, Transition.Linear, TimeSpan.FromMilliseconds(200), 1f);
        }
    }
}
