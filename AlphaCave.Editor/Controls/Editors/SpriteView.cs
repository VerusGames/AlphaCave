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
using EPoint = engenious.Point;

namespace AlphaCave.Editor.Controls.Editors
{
    public partial class SpriteView : UserControl
    {
        private int scaling = 2;

        private Pen selectionPen = new Pen(Color.Red);

        public Bitmap SpriteSheet { get; set; }

        public EPoint? SelectedSprite { get; set; } = new EPoint(1, 1);

        public event EventHandler SelectedSpriteChanged;

        public int SpriteSize { get; private set; }

        public int SpriteSpacing { get; private set; }

        public int scrollX, scrollY;

        public SpriteView(Bitmap spriteSheet, int spriteSize, int spriteSpacing)
        {
            InitializeComponent();

            SpriteSheet = spriteSheet;
            SpriteSize = spriteSize;
            SpriteSpacing = spriteSpacing;

            DoubleBuffered = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Width = SpriteSheet.Width * scaling;
            Height = SpriteSheet.Height * scaling;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.DrawImage(SpriteSheet, new Rectangle(0, 0, SpriteSheet.Width*2, SpriteSheet.Height*2));

            if (SelectedSprite != null)
            {
                // Zeichnen des Auswahl-Rectangles
                e.Graphics.DrawRectangle(selectionPen, 
                    (SelectedSprite.Value.X * ((SpriteSize + SpriteSpacing)*scaling)) - (1 * scaling), (SelectedSprite.Value.Y * ((SpriteSize + SpriteSpacing) * scaling))-(1*scaling),
                    (SpriteSize + SpriteSpacing) * scaling, (SpriteSize + SpriteSpacing) * scaling);
            }
        }

        public event EventHandler InternalGotFocus;

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            InternalGotFocus?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            

            var x = e.X / ((SpriteSize + SpriteSpacing) * scaling + HorizontalScroll.Value);
            var y = e.Y / ((SpriteSize + SpriteSpacing) * scaling + VerticalScroll.Value);

            SelectedSprite = new EPoint(x, y);
            SelectedSpriteChanged?.Invoke(this, EventArgs.Empty);
            Invalidate();
        }
    }
}
