using AlphaCave.Editor.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlphaCave.Editor.Controls.Editors
{
    public interface IObjectEditor
    {
        UserControl Control { get; }

        IEditorObject Object { get; set; }
    }
}
