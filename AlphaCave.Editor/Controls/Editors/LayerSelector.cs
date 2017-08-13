using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AlphaCave.Editor.Objects;

namespace AlphaCave.Editor.Controls.Editors
{
    public partial class LayerSelector : UserControl
    {
        public TextureMap TextureMap
        {
            get => textureMap;

            set
            {
                if (textureMap == value) return;

                textureMap = value;
                RecalculateList();
            }
        }

        public TextureLayer SelectedLayer
        {
            get
            {
                if (listView.SelectedItems.Count == 0)
                    listView.Items[0].Selected = true;

                return listView.SelectedItems[0].Tag as TextureLayer;
            }
        }

        private TextureMap textureMap;

        public LayerSelector()
        {
            InitializeComponent();

            Dock = DockStyle.Fill;

            listView.AfterLabelEdit += (s, e) =>
            {
                if (string.IsNullOrEmpty(e.Label))
                {
                    e.CancelEdit = true;
                    return;
                }
                ((TextureLayer)listView.Items[e.Item].Tag).Name = e.Label;
                RecalculateList();
            };
        }

        public void RecalculateList()
        {
            listView.Items.Clear();

            foreach (var layer in TextureMap.Layers)
            {
                listView.Items.Add(new ListViewItem(layer.Name) { Tag = layer });
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var layer = TextureMap.AddLayer();
            var item = new ListViewItem(layer.Name) { Tag = layer };
            listView.Items.Add(item);
            item.BeginEdit();
        }
    }
}
