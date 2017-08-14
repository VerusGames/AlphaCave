using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using AlphaCave.Editor.Objects;
using AlphaCave.Editor.Forms.Dialogs;

namespace AlphaCave.Editor.Controls
{
    public partial class ObjectList : UserControl
    {
        public ObservableCollection<IEditorObject> EditorObjects { get; private set; } = new ObservableCollection<IEditorObject>();

        public IEditorObject SelectedObject
        {
            get
            {
                if (listView?.SelectedItems.Count == 0)
                    return null;
                return listView?.SelectedItems?[0]?.Tag as IEditorObject;
            }
        }


        public event EventHandler OnObjectSelect;
        public event EventHandler SelectionChanged;

        public ObjectList()
        {
            InitializeComponent();

            
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            EditorObjects.CollectionChanged += (s, _) => RecalculateList();
            listView.ItemSelectionChanged += (s, _) =>
            {
                if(listView?.SelectedItems?.Count > 0)
                    OnObjectSelect?.Invoke(this, EventArgs.Empty);

                SelectionChanged?.Invoke(this, EventArgs.Empty);
            };
        }

        public void RecalculateList()
        {
            listView.Items.Clear();

            foreach (var eObject in EditorObjects)
            {
                listView.Items.Add(new ListViewItem(eObject.Name) { Tag = eObject });
            }
        }

        private void toolStripButton_add_Click(object sender, EventArgs e)
        {
            var sizeDialog = new SizeDialog();
            if (sizeDialog.ShowDialog() == DialogResult.OK)
            {
                var structureObject = new StructureObject("Struktur", new Core.Index2(sizeDialog.ValueSize.X, sizeDialog.ValueSize.Y), sizeDialog.ValueHeight);
                EditorObjects.Add(structureObject);
            }
        }
    }
}
