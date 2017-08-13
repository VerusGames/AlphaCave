using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using AlphaCave.Core;

namespace AlphaCave.Editor.Controls.Editors
{
    public partial class SpriteSelector : UserControl
    {

        public Index2 SelectedSprite { get; private set; }
        public string SelectedSpriteSheet { get; private set; }

        public event EventHandler SelectedSpriteChanged;

        const string FolderPath = "Spritesheets";

        List<SpriteView> spriteViews = new List<SpriteView>();

        public Dictionary<string, Bitmap> SpriteSheets { get; set; } = new Dictionary<string, Bitmap>();

        public SpriteSelector()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if(!DesignMode)
                LoadSpritesheets();
        }

        private void LoadSpritesheets()
        {
            foreach(var file in Directory.EnumerateFiles(FolderPath, "*.png"))
            {
                var sheet = (Bitmap)Image.FromFile(file);
                var sheetName = Path.GetFileNameWithoutExtension(file);
                SpriteSheets.Add(sheetName, sheet);

                var v = new SpriteView(sheet, 16, 1);
                spriteViews.Add(v);

                v.SelectedSpriteChanged += (s, e) =>
                {
                    foreach(var sheetView in spriteViews)
                    {
                        if (sheetView != v)
                            sheetView.SelectedSprite = null;
                    }

                    SelectedSpriteSheet = sheetName;
                    SelectedSprite = (Index2)v.SelectedSprite;
                    SelectedSpriteChanged?.Invoke(this, EventArgs.Empty);
                };

                var page = new TabPage(sheetName);
                page.AutoScroll = true;
                page.Controls.Add(v);

                v.InternalGotFocus += (s, e) =>
                {
                    page.HorizontalScroll.Value = v.scrollX;
                    page.VerticalScroll.Value = v.scrollY;
                    page.AutoScrollPosition = new Point(v.scrollX, v.scrollY);
                };

                page.MouseWheel += (s, e) =>
                {
                    v.scrollY = page.VerticalScroll.Value;
                };

                page.Scroll += (s, e) =>
                {
                    v.scrollX = page.HorizontalScroll.Value;
                    v.scrollY = page.VerticalScroll.Value;
                };

                tabControl.TabPages.Add(page);
            }
        }
    }
}
