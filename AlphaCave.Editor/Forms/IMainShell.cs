using AlphaCave.Editor.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AlphaCave.Editor.Delegates;

namespace AlphaCave.Editor.Forms
{
    public interface IMainShell
    {
        event ObjectSelectHandler OnObjectSelected;

        void ShowEditor(IEditorObject editorObject);
    }
}
