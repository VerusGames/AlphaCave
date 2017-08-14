using AlphaCave.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AlphaCave.Editor.Manager;

namespace AlphaCave.Editor.Objects
{
    public class StructureObject : EditorObject
    {
        public override string Name { get; set; }

        public override string FilePath { get; set; }

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

        public override void InternalSerialize(BinaryWriter bw)
        {
            bw.Write(Name);
            bw.Write(Height);
            bw.Write(Size.X);
            bw.Write(Size.Y);
            NorthMap.Serialize(bw);
            EastMap.Serialize(bw);
            SouthMap.Serialize(bw);
            WestMap.Serialize(bw);
        }

        public override IEditorObject InternalDeserialize(BinaryReader br)
        {
            Name = br.ReadString();
            Height = br.ReadInt32();
            var sizeX = br.ReadInt16();
            var sizeY = br.ReadInt16();

            Size = new Index2(sizeX, sizeY);

            NorthMap = TextureMap.Deserialize(br);
            EastMap = TextureMap.Deserialize(br);
            SouthMap = TextureMap.Deserialize(br);
            WestMap = TextureMap.Deserialize(br);

            return this;
        }

        public override void Export(string filename)
        {
            var pathPart = Path.Combine(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename));
            var ext = ".png";

            NorthMap.CreateBitmap(SpritesheetManager.Instance.Spritesheets).Save(pathPart+"_n"+ext);
            EastMap.CreateBitmap(SpritesheetManager.Instance.Spritesheets).Save(pathPart + "_e" + ext);
            SouthMap.CreateBitmap(SpritesheetManager.Instance.Spritesheets).Save(pathPart + "_s" + ext);
            WestMap.CreateBitmap(SpritesheetManager.Instance.Spritesheets).Save(pathPart + "_w" + ext);
        }
    }
}
