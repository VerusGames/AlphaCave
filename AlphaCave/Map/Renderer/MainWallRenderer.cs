using AlphaCave.Core.Maps;
using AlphaCave.Graphics;
using engenious;
using engenious.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCave.Map
{
    class BottomWallRenderer : IDisposable
    {
        private VertexBuffer _vertexBuffer;
        private IndexBuffer _indexBuffer;
        private Spritesheet _spriteSheet;
        private Effect _effect;
        private Matrix _world;
        private int _width, _height;
        private Game _game;
        private readonly MapVertex[] _vertices;

        public Chunk Chunk { get; private set; }

        public BottomWallRenderer(Game game, Spritesheet spriteSheet, Chunk chunk, int width = Chunk.CHUNKSIZE_X, int height = Chunk.CHUNKSIZE_Y)
        {
            Chunk = chunk;
            _spriteSheet = spriteSheet;
            _effect = game.Content.Load<Effect>("simple");

            _width = width;
            _height = height;
            _game = game;
            _vertices = new MapVertex[_width * _height * 4];
            CreateIndexBuffer(game.GraphicsDevice, width * height);

            ReloadChunk();
        }

        public void ReloadChunk()
        {
            _world = Matrix.CreateTranslation(Chunk.Index.X, Chunk.Index.Y, 0) * Matrix.CreateScaling(16, 16, 16);

            int index = 0;
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    var tile = Chunk.GetTile(x, y);

                    uint tileIndex = 0;

                    var flags = Chunk.GetFlags(new Core.Index2(x, y));

                    if (!flags.HasFlag(TileFlags.PreVisible))
                        continue;

                    var wallType = WallHelper.GetWallType(new Core.Index2(x, y), Chunk);
                    var tIndex = WallHelper.StoneWallLookupTable[wallType];

                    tileIndex = _spriteSheet.GetIndex(tIndex.X, tIndex.Y);

                    _vertices[index++] = new MapVertex(x + 0, y + 0, tileIndex);
                    _vertices[index++] = new MapVertex(x + 1, y + 0, tileIndex);

                    _vertices[index++] = new MapVertex(x + 0, y + 1, tileIndex);
                    _vertices[index++] = new MapVertex(x + 1, y + 1, tileIndex);
                }
            }

            if (_vertexBuffer == null)
                _vertexBuffer = new VertexBuffer(_game.GraphicsDevice, MapVertex.VertexDeclaration, _vertices.Length + 1);
            else if (_vertexBuffer.VertexCount != _vertices.Length)
                _vertexBuffer.Resize(_vertices.Length + 1);

            _vertexBuffer.SetData(_vertices);
        }

        public void SetChunk(Chunk chunk, Spritesheet sheet)
        {
            Chunk = chunk;
            _spriteSheet = sheet;
            ReloadChunk();
        }

        private void CreateIndexBuffer(GraphicsDevice graphicsDevice, int quadCount)
        {
            List<ushort> indices = new List<ushort>(quadCount * 6);
            for (uint i = 0; i < quadCount * 4; i += 4)
            {
                indices.Add((ushort)(0 + i));
                indices.Add((ushort)(1 + i));
                indices.Add((ushort)(3 + i));

                indices.Add((ushort)(0 + i));
                indices.Add((ushort)(3 + i));
                indices.Add((ushort)(2 + i));
            }
            if (_indexBuffer == null)
                _indexBuffer = new IndexBuffer(graphicsDevice, DrawElementsType.UnsignedShort, indices.Count);
            else
                _indexBuffer.Resize(indices.Count);
            _indexBuffer.SetData(indices.ToArray());
        }
        static SamplerState NearestSampler = new SamplerState() { TextureFilter = TextureFilter.Nearest, AddressU = TextureWrapMode.Repeat, AddressV = TextureWrapMode.Repeat };
        public void Render(GraphicsDevice graphicsDevice, Matrix view, Matrix projection)
        {
            //_spriteSheets
            graphicsDevice.IndexBuffer = _indexBuffer;
            graphicsDevice.VertexBuffer = _vertexBuffer;

            _spriteSheet.Textures.SamplerState = NearestSampler;
            _effect.Parameters["TileTextures"].SetValue(_spriteSheet.Textures);
            _effect.Parameters["WorldViewProj"].SetValue(projection * view * _world);

            foreach (var pass in _effect.CurrentTechnique.Passes.PassesList)
            {
                pass.Apply();

                graphicsDevice.DrawIndexedPrimitives(PrimitiveType.Triangles, 0, 0, _vertexBuffer.VertexCount, 0, _indexBuffer.IndexCount / 3);
            }
        }

        public void Dispose()
        {
            _vertexBuffer.Dispose();
            _indexBuffer.Dispose();
        }
    }
}