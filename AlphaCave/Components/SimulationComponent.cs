using AlphaCave.Core.Entities;
using AlphaCave.Core.Maps;
using engenious;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCave.Components
{
    public class SimulationComponent : GameComponent
    {
        public new AlphaCaveGame Game { get; }

        public World World { get; private set; }

        public Player Player { get; private set; }

        public SimulationState State { get; private set; }

        protected SimulationComponent(AlphaCaveGame game) : base(game)
        {
            Game = game;
        }

        public void NewGame()
        {
            State = SimulationState.Loading;
            World = World.CreateDebugWorld();
            Player = new Player();
        }

    }

    public enum SimulationState
    {
        Loading,
        Running,
        Paused,
        Stopped
    }
}
