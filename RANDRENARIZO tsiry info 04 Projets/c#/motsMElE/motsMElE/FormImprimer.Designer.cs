namespace motsMElE
{
    partial class FormImprimer
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
            this.m_tbImprimer = new System.Windows.Forms.TextBox();
            this.m_btChemin = new System.Windows.Forms.Button();
            this.m_tbNomFichier = new System.Windows.Forms.TextBox();
            this.m_btImprimer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_tbImprimer
            // 
            this.m_tbImprimer.Location = new System.Drawing.Point(5, 0);
            this.m_tbImprimer.Multiline = true;
            this.m_tbImprimer.Name = "m_tbImprimer";
            this.m_tbImprimer.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.m_tbImprimer.Size = new System.Drawing.Size(993, 544);
            this.m_tbImprimer.TabIndex = 0;
            // 
            // m_btChemin
            // 
            this.m_btChemin.Location = new System.Drawing.Point(12, 550);
            this.m_btChemin.Name = "m_btChemin";
            this.m_btChemin.Size = new System.Drawing.Size(75, 23);
            this.m_btChemin.TabIndex = 1;
            this.m_btChemin.Text = "chemin :";
            this.m_btChemin.UseVisualStyleBackColor = true;
            this.m_btChemin.Click += new System.EventHandler(this.m_btImprimer_Click);
            // 
            // m_tbNomFichier
            // 
            this.m_tbNomFichier.Location = new System.Drawing.Point(103, 552);
            this.m_tbNomFichier.Name = "m_tbNomFichier";
            this.m_tbNomFichier.Size = new System.Drawing.Size(349, 20);
            this.m_tbNomFichier.TabIndex = 2;
            this.m_tbNomFichier.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // m_btImprimer
            // 
            this.m_btImprimer.Location = new System.Drawing.Point(473, 548);
            this.m_btImprimer.Name = "m_btImprimer";
            this.m_btImprimer.Size = new System.Drawing.Size(75, 24);
            this.m_btImprimer.TabIndex = 3;
            this.m_btImprimer.Text = "imprimer";
            this.m_btImprimer.UseVisualStyleBackColor = true;
            this.m_btImprimer.Click += new System.EventHandler(this.m_btImprimer_Click_1);
            // 
            // FormImprimer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1007, 597);
            this.Controls.Add(this.m_btImprimer);
            this.Controls.Add(this.m_tbNomFichier);
            this.Controls.Add(this.m_btChemin);
            this.Controls.Add(this.m_tbImprimer);
            this.Name = "FormImprimer";
            this.Text = "FormImprimer";
            this.Load += new System.EventHandler(this.FormImprimer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_tbImprimer;
        private System.Windows.Forms.Button m_btChemin;
        private System.Windows.Forms.TextBox m_tbNomFichier;
        private System.Windows.Forms.Button m_btImprimer;
    }
}