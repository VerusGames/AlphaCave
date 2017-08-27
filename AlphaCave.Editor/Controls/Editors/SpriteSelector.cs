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
using AlphaCave.Editor.Manager;
using EPoint = engenious.Point;

namespace AlphaCave.Editor.Controls.Editors
{
    public partial class SpriteSelector : UserControl
    {

        public EPoint SelectedSprite { get; private set; }
        public string SelectedSpriteSheet { get; private set; }

        public event EventHandler SelectedSpriteChanged;

        const string FolderPath = "Spritesheets";

        List<SpriteView> spriteViews = new List<SpriteView>();


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
            foreach(var sheet in SpritesheetManager.Instance.Spritesheets)
            {

                var v = new SpriteView(sheet.Value, 16, 1);
                spriteViews.Add(v);

                v.SelectedSpriteChanged += (s, e) =>
                {
                    foreach(var sheetView in spriteViews)
                    {
                        if (sheetView != v)
                            sheetView.SelectedSprite = null;
                    }

                    SelectedSpriteSheet = sheet.Key;
                    SelectedSprite = (EPoint)v.SelectedSprite;
                    SelectedSpriteChanged?.Invoke(this, EventArgs.Empty);
                };

                var page = new TabPage(sheet.Key);
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
