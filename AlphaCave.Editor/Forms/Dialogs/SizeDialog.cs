using AlphaCave.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlphaCave.Editor.Forms.Dialogs
{
    public partial class SizeDialog : Form
    {
        public Index2 ValueSize { get => valueSize;
            set
            {
                if (valueSize.Equals(value))
                    return;

                valueSize = value;
                numericUpDown_x.Value = valueSize.X;
                numericUpDown_y.Value = valueSize.Y;
            }
        }

        public int ValueHeight { get => valueHeight;
            set
            {
                if (valueHeight == value)
                    return;

                valueHeight = value;
                numericUpDown_height.Value = valueHeight;
            }
        }

        private Index2 valueSize = new Index2(2,2);
        private int valueHeight = 2;

        public SizeDialog()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            numericUpDown_height.ValueChanged += (s, _) => valueHeight = (int)numericUpDown_height.Value;
            numericUpDown_x.ValueChanged += (s, _) => valueSize.X = (int)numericUpDown_x.Value;
            numericUpDown_y.ValueChanged += (s, _) => valueSize.Y = (int)numericUpDown_y.Value;
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
