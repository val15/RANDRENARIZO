namespace motsMElE
{
    partial class BoutonLettre
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_bt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_bt
            // 
            this.m_bt.Location = new System.Drawing.Point(0, 0);
            this.m_bt.Name = "m_bt";
            this.m_bt.Size = new System.Drawing.Size(25, 25);
            this.m_bt.TabIndex = 0;
            this.m_bt.Text = "button1";
            this.m_bt.UseVisualStyleBackColor = true;
            this.m_bt.Click += new System.EventHandler(this.m_bt_Click);
            // 
            // BoutonLettre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_bt);
            this.Name = "BoutonLettre";
            this.Size = new System.Drawing.Size(30, 30);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button m_bt;
    }
}
