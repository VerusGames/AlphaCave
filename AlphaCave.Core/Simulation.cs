using System.Collections.Generic;
using AlphaCave.Core.Entities;
using AlphaCave.Core.Maps;
using engenious;

namespace AlphaCave.Core
{
    public class Simulation
    {
        public readonly World World;
        
        private readonly List<Character> charachters = new List<Character>();
        public IEnumerable<Character> Characters => charachters;   

        public Simulation(World word)
        {
            World = word;
        }

        public void Start()
        {
            
        }

        public void Stop()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public Player SpanwPLayer()
        {
            var player =  new Player();
            player.Position = new Coordinate()
            {
                FloorPosition = new Point(15,15),
                Floor = 0,
            };

            charachters.Add(player);
            
            return player;
        }
    }
}