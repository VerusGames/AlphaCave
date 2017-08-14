using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCave.Editor.Objects
{
    public interface IEditorObject
    {
        string Name { get; set; }
        string FilePath { get; set; }

        void InternalSerialize(BinaryWriter bw);
        void Serialize(BinaryWriter bw);

        void Export(string filename);

        IEditorObject InternalDeserialize(BinaryReader br);
    }
}
