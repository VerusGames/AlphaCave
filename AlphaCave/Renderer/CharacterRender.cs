using System.Collections.Generic;
using AlphaCave.Core.Entities;
using AlphaCave.Core.Maps;
using AlphaCave.Graphics;
using AlphaCave.Map;
using engenious;
using engenious.Graphics;
using DrawElementsType = engenious.Graphics.DrawElementsType;
using PrimitiveType = engenious.Graphics.PrimitiveType;

namespace AlphaCave.Renderer
{
    public class CharacterRender
    {
        private readonly AlphaCaveGame game;
        private readonly Spritesheet spriteSheet;

        private readonly VertexBuffer vertexBuffer;
        private readonly IndexBuffer indexBuffer;
        private readonly Effect effect;
        
        
        
        public CharacterRender(AlphaCaveGame game, Spritesheet spriteSheet)
        {
            this.game = game;
            this.spriteSheet = spriteSheet;

            
            effect = game.Content.Load<Effect>("simple");
            uint tileIndex = 1;
            var vertices = new[]
            {
                new MapVertex(0, 0, 0, tileIndex),
                new MapVertex(1, 0, 0, tileIndex),
                new MapVertex(0, 1, 1, tileIndex),
                new MapVertex(1, 1, 1, tileIndex)
            };


            vertexBuffer = new VertexBuffer(game.GraphicsDevice,MapVertex.VertexDeclaration,vertices.Length);
            vertexBuffer.SetData(vertices);


            var indices = new ushort[]{0, 1, 3, 0, 3, 2};


            indexBuffer = new IndexBuffer(game.GraphicsDevice, DrawElementsType.UnsignedShort, indices.Length);
            indexBuffer.SetData(indices);
        }
        static SamplerState NearestSampler = new SamplerState() { TextureFilter = TextureFilter.Nearest, AddressU = TextureWrapMode.Repeat, AddressV = TextureWrapMode.Repeat };
        
        public void Render(GraphicsDevice graphicsDevice, Matrix view, Matrix projection,Character character)
        {
            graphicsDevice.VertexBuffer = vertexBuffer;
            graphicsDevice.IndexBuffer = indexBuffer;
            spriteSheet.Textures.SamplerState = NearestSampler;

            Vector3 translation =character.Position.GetPositionVector()* 16;
            
            var world = Matrix.CreateTranslation(translation)* Matrix.CreateScaling(16, 16, 16);
            
            effect.Parameters["TileTextures"].SetValue(spriteSheet.Textures);
            
            effect.Parameters["WorldViewProj"].SetValue(projection*view*world);

            foreach (var p in effect.CurrentTechnique.Passes.PassesList)
            {
                p.Apply();
                graphicsDevice.DrawIndexedPrimitives(PrimitiveType.Triangles, 0, 0, vertexBuffer.VertexCount, 0,
                    indexBuffer.IndexCount / 3);
            }
        }
    }
}