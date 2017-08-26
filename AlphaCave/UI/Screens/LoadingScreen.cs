using AlphaCave.Components;
using EngeniousUI;
using engenious;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCave.UI.Screens
{
    public class LoadingScreen : Screen
    {
        Label text;
        ProgressBar progressBar;

        ScreenManager manager;

        bool initialized = false;
        bool completed = false;

        public LoadingScreen(ScreenManager manager) : base(manager)
        {
            this.manager = manager;

            //Set UI up
            text = new Label(manager) { Text = "Loading...", HorizontalAlignment = HorizontalAlignment.Center, TextColor = Color.White};
            progressBar = new ProgressBar(manager)
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Height = 20,
                Background = new BorderBrush(Color.DarkGray),
                BarBrush = new BorderBrush(Color.White)
            };

            Background = new BorderBrush(Color.White * 0.5f);

            StackPanel sp = new StackPanel(manager);
            sp.MinWidth = 400;
            sp.Width = manager.Frame.Width / 2;
            sp.Controls.Add(text);
            sp.Controls.Add(progressBar);

            Controls.Add(sp);
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            base.OnUpdate(gameTime);
        }

        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            base.OnNavigatedTo(args);

            if (initialized)
                return;

            AssetManager.Initialize(manager.Game, () => Manager.NavigateToScreen(new MainScreen(manager)), 
            (current, max) =>
            {
                progressBar.Maximum = max;
                progressBar.Value = current;
            });
        }
    }
}
