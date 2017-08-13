namespace AlphaCave.Editor.Controls.Editors
{
    partial class StructureEditor
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StructureEditor));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage_n = new System.Windows.Forms.TabPage();
            this.tabPage_e = new System.Windows.Forms.TabPage();
            this.tabPage_s = new System.Windows.Forms.TabPage();
            this.tabPage_w = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.spriteSelector1 = new AlphaCave.Editor.Controls.Editors.SpriteSelector();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(3, 28);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(950, 709);
            this.splitContainer1.SplitterDistance = 581;
            this.splitContainer1.TabIndex = 0;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage_n);
            this.tabControl.Controls.Add(this.tabPage_e);
            this.tabControl.Controls.Add(this.tabPage_s);
            this.tabControl.Controls.Add(this.tabPage_w);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(581, 709);
            this.tabControl.TabIndex = 0;
            // 
            // tabPage_n
            // 
            this.tabPage_n.Location = new System.Drawing.Point(4, 22);
            this.tabPage_n.Name = "tabPage_n";
            this.tabPage_n.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_n.Size = new System.Drawing.Size(573, 683);
            this.tabPage_n.TabIndex = 0;
            this.tabPage_n.Text = "North";
            this.tabPage_n.UseVisualStyleBackColor = true;
            // 
            // tabPage_e
            // 
            this.tabPage_e.Location = new System.Drawing.Point(4, 22);
            this.tabPage_e.Name = "tabPage_e";
            this.tabPage_e.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_e.Size = new System.Drawing.Size(577, 714);
            this.tabPage_e.TabIndex = 1;
            this.tabPage_e.Text = "East";
            this.tabPage_e.UseVisualStyleBackColor = true;
            // 
            // tabPage_s
            // 
            this.tabPage_s.Location = new System.Drawing.Point(4, 22);
            this.tabPage_s.Name = "tabPage_s";
            this.tabPage_s.Size = new System.Drawing.Size(577, 714);
            this.tabPage_s.TabIndex = 2;
            this.tabPage_s.Text = "South";
            this.tabPage_s.UseVisualStyleBackColor = true;
            // 
            // tabPage_w
            // 
            this.tabPage_w.Location = new System.Drawing.Point(4, 22);
            this.tabPage_w.Name = "tabPage_w";
            this.tabPage_w.Size = new System.Drawing.Size(577, 714);
            this.tabPage_w.TabIndex = 3;
            this.tabPage_w.Text = "West";
            this.tabPage_w.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.spriteSelector1);
            this.splitContainer2.Size = new System.Drawing.Size(365, 709);
            this.splitContainer2.SplitterDistance = 424;
            this.splitContainer2.TabIndex = 0;
            // 
            // spriteSelector1
            // 
            this.spriteSelector1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spriteSelector1.Location = new System.Drawing.Point(0, 0);
            this.spriteSelector1.Name = "spriteSelector1";
            this.spriteSelector1.Size = new System.Drawing.Size(365, 424);
            this.spriteSelector1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(956, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(46, 22);
            this.toolStripButton1.Text = "Debug";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // StructureEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Name = "StructureEditor";
            this.Size = new System.Drawing.Size(956, 740);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage_n;
        private System.Windows.Forms.TabPage tabPage_e;
        private System.Windows.Forms.TabPage tabPage_s;
        private System.Windows.Forms.TabPage tabPage_w;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private SpriteSelector spriteSelector1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}
