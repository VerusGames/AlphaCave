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
using AlphaCave.Editor.Debugger;
using AlphaCave.Editor.Manager;

namespace AlphaCave.Editor.Controls.Editors
{
    public partial class StructureEditor : UserControl, IObjectEditor
    {
        public StructureObject Object { get; private set; }

        SpriteEditor nEditor, eEditor, sEditor, wEditor;
        LayerSelector layerSelector = new LayerSelector();

        public StructureEditor()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        public StructureEditor(IEditorObject editorObject) :this()
        {
            Object = editorObject as StructureObject;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var northMap = Object.NorthMap.CreateBitmap(SpritesheetManager.Instance.Spritesheets);
            var eastMap = Object.EastMap.CreateBitmap(SpritesheetManager.Instance.Spritesheets);
            var southMap = Object.SouthMap.CreateBitmap(SpritesheetManager.Instance.Spritesheets);
            var westMap = Object.WestMap.CreateBitmap(SpritesheetManager.Instance.Spritesheets);

            northMap.Save("north.png");
            southMap.Save("south.png");

            EngeniousGame game = new EngeniousGame();
            game.ShowStructure(Object.Size, Object.Height, northMap, eastMap, southMap, westMap);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            splitContainer2.Panel2.Controls.Add(layerSelector);

            nEditor = new SpriteEditor(spriteSelector1, Object.NorthMap, layerSelector);
            eEditor = new SpriteEditor(spriteSelector1, Object.EastMap, layerSelector);
            sEditor = new SpriteEditor(spriteSelector1, Object.SouthMap, layerSelector);
            wEditor = new SpriteEditor(spriteSelector1, Object.WestMap, layerSelector);
            tabPage_n.Controls.Add(nEditor);
            tabPage_e.Controls.Add(eEditor);
            tabPage_s.Controls.Add(sEditor);
            tabPage_w.Controls.Add(wEditor);

            layerSelector.TextureMap = Object.NorthMap;

            tabControl.SelectedIndexChanged += (s, _) =>
            {
                layerSelector.TextureMap = ((SpriteEditor)tabControl.SelectedTab.Controls[0]).TextureMap;
            };
        }

        public UserControl Control => this;


        IEditorObject IObjectEditor.Object { get => Object; set => Object = value as StructureObject; }
    }
}
