using AlphaCave.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPoint = engenious.Point;

namespace AlphaCave.Editor.Objects
{
    public abstract class EditorObject : IEditorObject
    {
        public abstract string Name { get; set; }
        public abstract string FilePath { get; set; }

        public static IEditorObject Deserialize(BinaryReader br)
        {
            var objectType = br.ReadString();

            if (objectType == typeof(StructureObject).Name)
                return new StructureObject("", new EPoint(0, 0), 0).InternalDeserialize(br);

            else
                return null;

        }

        public virtual void Serialize(BinaryWriter bw)
        {
            bw.Write(GetType().Name);
            InternalSerialize(bw);
        }

        public abstract IEditorObject InternalDeserialize(BinaryReader br);
        public abstract void InternalSerialize(BinaryWriter bw);
        public abstract void Export(string filename);
    }
}
