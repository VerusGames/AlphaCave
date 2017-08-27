using AlphaCave.Core.Maps.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious;

namespace AlphaCave.Core.Maps
{
    public class Floor
    {

        private readonly Dictionary<Point, Chunk> chunks = new Dictionary<Point, Chunk>();
        public IEnumerable<Chunk> Chunks => chunks.Values;


        public readonly int Id;

        private readonly IMapGenerator mapGenerator;

        public Floor(int id, IMapGenerator generator)
        {
            this.Id = id;
            mapGenerator = generator; 
        }

        public Chunk GetChunk(short x, short y)
        {
            return GetChunk(new Point(x, y));
        }

        public Chunk GetChunk(Point index)
        {
            if (chunks.ContainsKey(index))
                return chunks[index];

            var chunk = mapGenerator.GenerateChunk(Id, index,this);

            chunks.Add(index,chunk);

            return chunk;
        }

    }
}
