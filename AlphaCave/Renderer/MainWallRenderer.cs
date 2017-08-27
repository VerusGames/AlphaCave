using System;
using System.Collections.Generic;
using AlphaCave.Core.Maps;
using AlphaCave.Graphics;
using AlphaCave.Map;
using engenious;
using engenious.Graphics;

namespace AlphaCave.Renderer
{
    class BottomWallRenderer : IDisposable
    {
        private VertexBuffer vertexBuffer;
        private IndexBuffer indexBuffer;
        private Spritesheet spriteSheet;
        private readonly Effect effect;
        private Matrix world;
        private readonly int width;
        private readonly int height;
        private readonly Game game;
        private readonly MapVertex[] vertices;

        public Chunk Chunk { get; private set; }

        public BottomWallRenderer(Game game, Spritesheet spriteSheet, Chunk chunk, int width = Chunk.CHUNKSIZE_X,
            int height = Chunk.CHUNKSIZE_Y)
        {
            Chunk = chunk;
            this.spriteSheet = spriteSheet;
            effect = game.Content.Load<Effect>("simple");

            this.width = width;
            this.height = height;
            this.game = game;
            vertices = new MapVertex[this.width * this.height * 4];
            CreateIndexBuffer(game.GraphicsDevice, width * height);

            ReloadChunk();
        }

        public void ReloadChunk()
        {
            // TODO: Scanline Alg. zum entfernen überflüssiger Wände
            // http://www-lehre.informatik.uni-osnabrueck.de/~cg/2000/skript/4_2_Scan_Line_Verfahren_f_252_r.html

            world = Matrix.CreateTranslation(Chunk.Index.X, Chunk.Index.Y, 0) * Matrix.CreateScaling(16, 16, 16);

            int index = 0;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var tile = Chunk.GetTile(x, y);

                    uint tileIndex = 0;

                    var flags = Chunk.GetFlags(new Point(x, y));

                    if (!flags.HasFlag(TileFlags.PreVisible))
                        continue;

                    var wallType = WallTypeHelper.GetWallType(new Point(x, y), Chunk);
                    var tIndexs = WallTypeHelper.StoneWallLookupTable[wallType];

                    var endTile = 2 - tIndexs.Length+1;
                    
                    for (int dY = 2; dY >= endTile; dY--)
                    {
                        var tIndex = tIndexs[2-dY];

                        tileIndex = spriteSheet.GetIndex(tIndex.X, tIndex.Y);

                        vertices[index++] = new MapVertex(x + 0, y - dY + 0,y+1, tileIndex);
                        vertices[index++] = new MapVertex(x + 1, y - dY + 0,y+1, tileIndex);

                        vertices[index++] = new MapVertex(x + 0, y - dY + 1,y+1, tileIndex);
                        vertices[index++] = new MapVertex(x + 1, y - dY + 1,y+1, tileIndex);
                    }
                }
            }

            if (vertexBuffer == null)
                vertexBuffer = new VertexBuffer(game.GraphicsDevice, MapVertex.VertexDeclaration,
                    vertices.Length + 1);
            else if (vertexBuffer.VertexCount != vertices.Length)
                vertexBuffer.Resize(vertices.Length + 1);

            vertexBuffer.SetData(vertices);
        }

        public void SetChunk(Chunk chunk, Spritesheet sheet)
        {
            Chunk = chunk;
            spriteSheet = sheet;
            ReloadChunk();
        }

        private void CreateIndexBuffer(GraphicsDevice graphicsDevice, int quadCount)
        {
            List<ushort> indices = new List<ushort>(quadCount * 6);
            for (uint i = 0; i < quadCount * 4; i += 4)
            {
                indices.Add((ushort) (0 + i));
                indices.Add((ushort) (1 + i));
                indices.Add((ushort) (3 + i));

                indices.Add((ushort) (0 + i));
                indices.Add((ushort) (3 + i));
                indices.Add((ushort) (2 + i));
            }
            if (indexBuffer == null)
                indexBuffer = new IndexBuffer(graphicsDevice, DrawElementsType.UnsignedShort, indices.Count);
            else
                indexBuffer.Resize(indices.Count);
            indexBuffer.SetData(indices.ToArray());
        }

        static SamplerState NearestSampler = new SamplerState()
        {
            TextureFilter = TextureFilter.Nearest,
            AddressU = TextureWrapMode.Repeat,
            AddressV = TextureWrapMode.Repeat
        };

        public void Render(GraphicsDevice graphicsDevice, Matrix view, Matrix projection)
        {
            //_spriteSheets
            graphicsDevice.IndexBuffer = indexBuffer;
            graphicsDevice.VertexBuffer = vertexBuffer;

            spriteSheet.Textures.SamplerState = NearestSampler;
            effect.Parameters["TileTextures"].SetValue(spriteSheet.Textures);
            effect.Parameters["WorldViewProj"].SetValue(projection * view * world);

            foreach (var pass in effect.CurrentTechnique.Passes.PassesList)
            {
                pass.Apply();

                graphicsDevice.DrawIndexedPrimitives(PrimitiveType.Triangles, 0, 0, vertexBuffer.VertexCount, 0,
                    indexBuffer.IndexCount / 3);
            }
        }

        public void Dispose()
        {
            vertexBuffer.Dispose();
            indexBuffer.Dispose();
        }
    }
}