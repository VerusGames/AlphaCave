﻿using AlphaCave.Core.Maps.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCave.Core.Maps
{
    public class World
    {
        private readonly List<Floor> floors = new List<Floor>();
        public IEnumerable<Floor> Floors => floors;

        private World()
        {

        }

        public static World CreateDebugWorld()
        {
            World world = new World();

            DebugGenerator debugGenerator = new DebugGenerator();

            for (int i = 0; i < 10; i++)
                world.floors.Add(new Floor(i,debugGenerator));


            return world;
        }
    }
}