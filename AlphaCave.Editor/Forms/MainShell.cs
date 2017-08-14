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
using System.Reflection;
using System.IO;
using System.Collections.ObjectModel;

namespace AlphaCave.Editor.Forms
{
    public partial class MainShell : Form, IMainShell
    {
        public ObservableCollection<IEditorObject> OpenObjects => objectList.EditorObjects;

        public MainShell()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            saveFileToolStripMenuItem.Enabled = false;
            saveFileAsToolStripMenuItem.Enabled = false;

            objectList.OnObjectSelect += (s, _) => OnObjectSelected?.Invoke(objectList.SelectedObject);
            objectList.SelectionChanged += ObjectList_SelectionChanged;

            newFileToolStripMenuItem.Click += (s, _) => NewFileClicked?.Invoke(this);
            openFileToolStripMenuItem.Click += (s, _) => OpenFileClicked?.Invoke(this);
            saveFileAsToolStripMenuItem.Click += (s, _) => SaveFileAsClicked?.Invoke(objectList.SelectedObject);
            saveFileToolStripMenuItem.Click += (s, _) => SaveFileClicked?.Invoke(objectList.SelectedObject);
            openFolderToolStripMenuItem.Click += (s, _) => OpenFolderClicked?.Invoke(this);
            exitToolStripMenuItem.Click += (s, _) => ExitClicked?.Invoke(this);

            exportToolStripMenuItem.Click += (s, _) => ExportClick?.Invoke(objectList.SelectedObject);
            
        }

        private void ObjectList_SelectionChanged(object sender, EventArgs e)
        {
            if(objectList.SelectedObject == null)
            {
                saveFileToolStripMenuItem.Enabled = false;
                saveFileAsToolStripMenuItem.Enabled = false;
            }
            else
            {
                saveFileToolStripMenuItem.Enabled = true;
                saveFileAsToolStripMenuItem.Enabled = true;
            }
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

        public string[] ShowFileOpen()
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = Directory.GetCurrentDirectory();
                ofd.Multiselect = true;
                ofd.CheckFileExists = true;
                ofd.Filter = "ACEF|*.acef";

                if (ofd.ShowDialog() == DialogResult.OK)
                    return ofd.FileNames;
                else
                    return null;
            }
        }

        public string ShowFolderOpen()
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = Directory.GetCurrentDirectory();
                fbd.ShowNewFolderButton = false;

                if (fbd.ShowDialog() == DialogResult.OK)
                    return fbd.SelectedPath;
                else
                    return null;
            }
        }

        public string ShowFileSave(string filename = null)
        {

            using (var sfd = new SaveFileDialog())
            {
                sfd.InitialDirectory = Directory.GetCurrentDirectory();
                sfd.OverwritePrompt = true;
                sfd.RestoreDirectory = true;
                sfd.ValidateNames = true;
                sfd.AddExtension = true;
                sfd.DefaultExt = "acef";

                if (!String.IsNullOrEmpty(filename))
                    sfd.FileName = filename;

                if (sfd.ShowDialog() == DialogResult.OK)
                    return sfd.FileName;
                else
                    return null;
            }
        }

        public void ShowMessage(string title, string message, MessageType type)
        {
            MessageBoxIcon mbIcon;

            if (type == MessageType.Error)
                mbIcon = MessageBoxIcon.Error;
            else if (type == MessageType.Warning)
                mbIcon = MessageBoxIcon.Warning;
            else
                mbIcon = MessageBoxIcon.Information;

            MessageBox.Show(message, title, MessageBoxButtons.OK, mbIcon);
        }

        public event Delegates.ObjectSelectHandler OnObjectSelected;
        public event Delegates.SenderHandler NewFileClicked;
        public event Delegates.SenderHandler OpenFileClicked;
        public event Delegates.ObjectSelectHandler SaveFileClicked;
        public event Delegates.ObjectSelectHandler SaveFileAsClicked;
        public event Delegates.SenderHandler OpenFolderClicked;
        public event Delegates.SenderHandler ExitClicked;
        public event Delegates.ObjectSelectHandler ExportClick;
    }
}
