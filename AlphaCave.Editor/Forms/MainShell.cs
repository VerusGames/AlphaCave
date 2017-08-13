using AlphaCave.Editor.Debugger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AlphaCave.Editor.Objects;
using AlphaCave.Editor.Controls.Editors;

namespace AlphaCave.Editor.Forms
{
    public partial class MainShell : Form, IMainShell
    {
        public MainShell()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            objectList1.OnObjectSelect += (s, _) => OnObjectSelected?.Invoke(objectList1.SelectedObject);
        }

        public void ShowEditor(IEditorObject editorObject)
        {
            IObjectEditor editor = null;

            if (editorObject is StructureObject)
                editor = new StructureEditor(editorObject);

            if (editor == null)
                return;

            splitContainer_main.Panel2.Controls.Clear();
            splitContainer_main.Panel2.Controls.Add(editor.Control);

        }

        public event Delegates.ObjectSelectHandler OnObjectSelected;
    }
}
