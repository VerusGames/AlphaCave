using AlphaCave.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AlphaCave.Editor.Objects
{
    public class StructureObject : IEditorObject
    {
        public string Name { get; set; }

        public string FilePath => "";

        public Index2 Size { get; set; }

        public int Height { get; set; }

        public TextureMap NorthMap { get; set; }
        public TextureMap EastMap { get; set; }
        public TextureMap SouthMap { get; set; }
        public TextureMap WestMap { get; set; }

        public StructureObject(string name, Index2 size, int height)
        {
            Name = name;
            Size = size;
            Height = height;

            NorthMap = new TextureMap(new Index2(size.X, height+size.Y));
            EastMap = new TextureMap(new Index2(size.Y, height + size.X));
            SouthMap = new TextureMap(new Index2(size.X, height + size.Y));
            WestMap = new TextureMap(new Index2(size.Y, height+size.X));
        }


    }
}
