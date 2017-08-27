using AlphaCave.Core.Entities;
using AlphaCave.Core.Maps;
using engenious;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaCave.Core;

namespace AlphaCave.Components
{
    public class SimulationComponent : GameComponent
    {
        public new AlphaCaveGame Game { get; }

        public World World { get; private set; }

        public Player Player { get; private set; }

        public SimulationState State { get; private set; }

        public Simulation Simulation { get; private set; }
        
        public SimulationComponent(AlphaCaveGame game) : base(game)
        {
            Game = game;
            NewGame();
        }

        public void NewGame()
        {
            State = SimulationState.Loading;
            World = World.CreateDebugWorld();
            
            Simulation = new Simulation(World);

            Player = Simulation.SpanwPLayer();
           
            Simulation.Start();


        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            Simulation?.Update(gameTime);
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
