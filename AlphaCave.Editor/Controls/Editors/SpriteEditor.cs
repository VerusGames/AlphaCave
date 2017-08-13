using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AlphaCave.Core;
using AlphaCave.Editor.Objects;
using System.IO;

namespace AlphaCave.Editor.Controls.Editors
{
    public partial class SpriteEditor : UserControl
    {
        const int spriteElementSize = 16;
        const string FolderPath = "Spritesheets";
        int scaling = 2;

        public Index2 SpriteSize { get; private set; }

        SpriteSelector selector;

        public TextureMap TextureMap { get; private set; }

        Dictionary<string, Bitmap> SpriteSheets { get => selector.SpriteSheets; }

        LayerSelector layerSelector;

        public SpriteEditor(SpriteSelector selector, TextureMap textureMap, LayerSelector layerSelector) //TODO: LayerSelector
        {
            SpriteSize = textureMap.Size;
            this.selector = selector;
            this.layerSelector = layerSelector;

            TextureMap = textureMap;

            InitializeComponent();

            Dock = DockStyle.Fill;

            DoubleBuffered = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //LoadSpritesheets();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Pen linePen = new Pen(Color.LightGray);
            for(int x = 0; x <= SpriteSize.X; x++)
                e.Graphics.DrawLine(linePen, x * 16 * scaling, 0, x * 16 * scaling, SpriteSize.Y * 16 * scaling);

            for (int y = 0; y <= SpriteSize.Y; y++)
                e.Graphics.DrawLine(linePen,0, y*16 * scaling, SpriteSize.X * 16 * scaling, y * 16 * scaling);

            for(int x = 0; x < TextureMap.Size.X; x++)
            {
                for(int y = 0; y < TextureMap.Size.Y; y++)
                {
                    for (int l = 0; l < TextureMap.Layers.Count; l++)
                    {
                        var sObject = TextureMap.Layers[l].Sprites[x, y];

                        if (sObject == null || sObject.SpriteSheet == null)
                            continue;

                        var sheet = SpriteSheets[sObject.SpriteSheet];

                        e.Graphics.DrawImage(sheet, new Rectangle(x * 16 * scaling, y * 16 * scaling, 16 * scaling, 16 * scaling), new Rectangle(sObject.SpriteID.X * 17, sObject.SpriteID.Y * 17, 16, 16), GraphicsUnit.Pixel);
                    }
                }
            }

            if(isPainting)
            {
                foreach(var tile in selectedTiles)
                {
                    var pen = new Pen(Color.FromArgb(100, Color.Blue));
                    e.Graphics.DrawRectangle(pen, new Rectangle(tile.X * 16 * scaling, tile.Y * 16 * scaling, 16 * scaling, 16 * scaling));
                }
            }

        }

        List<Index2> selectedTiles = new List<Index2>();
        bool isPainting = false;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Right)
                return;

            isPainting = true;

            var xPos = e.X / (16 * scaling);
            var yPos = e.Y / (16 * scaling);

            var sprites = layerSelector.SelectedLayer.Sprites;

            if (xPos >= sprites.GetLength(0) || yPos >= sprites.GetLength(1))
                return;

            selectedTiles.Add(new Index2(xPos, yPos));
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (!isPainting)
                return;

            var xPos = e.X / (16 * scaling);
            var yPos = e.Y / (16 * scaling);

            var sprites = layerSelector.SelectedLayer.Sprites;

            if (xPos >= sprites.GetLength(0) || yPos >= sprites.GetLength(1))
                return;

            selectedTiles.Add(new Index2(xPos, yPos));

            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (!isPainting)
                return;

            foreach(var index in selectedTiles)
            {
                layerSelector.SelectedLayer.Sprites[index.X, index.Y] = new SpriteObject(selector.SelectedSprite, selector.SelectedSpriteSheet);
            }
            Invalidate();

            isPainting = false;
            selectedTiles.Clear();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            var xPos = e.X / (16 * scaling);
            var yPos = e.Y / (16 * scaling);



            

            Invalidate();
        }

        private void LoadSpritesheets()
        {
            foreach (var file in Directory.EnumerateFiles(FolderPath, "*.png"))
            {
                var sheet = (Bitmap)Image.FromFile(file);
                var name = Path.GetFileNameWithoutExtension(file);
              
                SpriteSheets.Add(name,sheet);
            }
        }
    }
}
