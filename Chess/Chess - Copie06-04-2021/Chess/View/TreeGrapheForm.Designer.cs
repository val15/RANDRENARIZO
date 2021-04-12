
namespace Chess.View
{
  partial class TreeGrapheForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.lblNodeText = new System.Windows.Forms.ToolStripStatusLabel();
      this.radHorizontal = new System.Windows.Forms.RadioButton();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.ctxNode = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ctxNodeAddChild = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxNodeDelete = new System.Windows.Forms.ToolStripMenuItem();
      this.radVertical = new System.Windows.Forms.RadioButton();
      this.picTree = new System.Windows.Forms.PictureBox();
      this.statusStrip1.SuspendLayout();
      this.ctxNode.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.picTree)).BeginInit();
      this.SuspendLayout();
      // 
      // lblNodeText
      // 
      this.lblNodeText.Name = "lblNodeText";
      this.lblNodeText.Size = new System.Drawing.Size(0, 17);
      // 
      // radHorizontal
      // 
      this.radHorizontal.AutoSize = true;
      this.radHorizontal.Checked = true;
      this.radHorizontal.Location = new System.Drawing.Point(3, 3);
      this.radHorizontal.Name = "radHorizontal";
      this.radHorizontal.Size = new System.Drawing.Size(72, 17);
      this.radHorizontal.TabIndex = 7;
      this.radHorizontal.TabStop = true;
      this.radHorizontal.Text = "Horizontal";
      this.radHorizontal.UseVisualStyleBackColor = true;
      this.radHorizontal.Click += new System.EventHandler(this.radHorizontal_Click);
      // 
      // statusStrip1
      // 
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblNodeText});
      this.statusStrip1.Location = new System.Drawing.Point(0, 610);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(475, 22);
      this.statusStrip1.TabIndex = 5;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // ctxNode
      // 
      this.ctxNode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxNodeAddChild,
            this.ctxNodeDelete});
      this.ctxNode.Name = "ctxNode";
      this.ctxNode.Size = new System.Drawing.Size(149, 48);
      // 
      // ctxNodeAddChild
      // 
      this.ctxNodeAddChild.Name = "ctxNodeAddChild";
      this.ctxNodeAddChild.Size = new System.Drawing.Size(148, 22);
      this.ctxNodeAddChild.Text = "&Add Child...";
      // 
      // ctxNodeDelete
      // 
      this.ctxNodeDelete.Name = "ctxNodeDelete";
      this.ctxNodeDelete.Size = new System.Drawing.Size(148, 22);
      this.ctxNodeDelete.Text = "&Delete Node...";
      this.ctxNodeDelete.Click += new System.EventHandler(this.ctxNodeDelete_Click);
      // 
      // radVertical
      // 
      this.radVertical.AutoSize = true;
      this.radVertical.Location = new System.Drawing.Point(90, 3);
      this.radVertical.Name = "radVertical";
      this.radVertical.Size = new System.Drawing.Size(60, 17);
      this.radVertical.TabIndex = 8;
      this.radVertical.Text = "Vertical";
      this.radVertical.UseVisualStyleBackColor = true;
      this.radVertical.CheckedChanged += new System.EventHandler(this.radVertical_CheckedChanged);
      this.radVertical.Click += new System.EventHandler(this.radVertical_Click);
      // 
      // picTree
      // 
      this.picTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.picTree.Location = new System.Drawing.Point(3, 26);
      this.picTree.Name = "picTree";
      this.picTree.Size = new System.Drawing.Size(469, 581);
      this.picTree.TabIndex = 6;
      this.picTree.TabStop = false;
      this.picTree.Paint += new System.Windows.Forms.PaintEventHandler(this.picTree_Paint);
      this.picTree.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picTree_MouseDown);
      this.picTree.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picTree_MouseMove);
      this.picTree.Resize += new System.EventHandler(this.picTree_Resize);
      // 
      // TreeGrapheForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(475, 632);
      this.Controls.Add(this.radHorizontal);
      this.Controls.Add(this.statusStrip1);
      this.Controls.Add(this.radVertical);
      this.Controls.Add(this.picTree);
      this.Name = "TreeGrapheForm";
      this.Text = "howto_treenode_orientations";
      this.Load += new System.EventHandler(this.TreeGrapheForm_Load);
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.ctxNode.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.picTree)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ToolStripStatusLabel lblNodeText;
    private System.Windows.Forms.RadioButton radHorizontal;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ContextMenuStrip ctxNode;
    private System.Windows.Forms.ToolStripMenuItem ctxNodeAddChild;
    private System.Windows.Forms.ToolStripMenuItem ctxNodeDelete;
    private System.Windows.Forms.RadioButton radVertical;
    private System.Windows.Forms.PictureBox picTree;
  }

}