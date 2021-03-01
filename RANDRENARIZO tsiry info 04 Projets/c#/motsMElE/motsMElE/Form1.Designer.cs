namespace motsMElE
{
    partial class Form1
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
            this.m_tbCorrection = new System.Windows.Forms.TextBox();
            this.m_tblisteMotAChercer = new System.Windows.Forms.TextBox();
            this.m_btRemplir = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.m_lbNbMotAChercher = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_btGenerer = new System.Windows.Forms.Button();
            this.m_nUDCotee = new System.Windows.Forms.NumericUpDown();
            this.m_tbMotCaché = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_cbCorrection = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.m_tbGrilleMotTrouvE = new System.Windows.Forms.TextBox();
            this.m_btImprimer = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_nUDCotee)).BeginInit();
            this.SuspendLayout();
            // 
            // m_tbCorrection
            // 
            this.m_tbCorrection.Location = new System.Drawing.Point(587, 339);
            this.m_tbCorrection.Multiline = true;
            this.m_tbCorrection.Name = "m_tbCorrection";
            this.m_tbCorrection.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.m_tbCorrection.Size = new System.Drawing.Size(330, 282);
            this.m_tbCorrection.TabIndex = 1;
            // 
            // m_tblisteMotAChercer
            // 
            this.m_tblisteMotAChercer.Location = new System.Drawing.Point(12, 522);
            this.m_tblisteMotAChercer.Multiline = true;
            this.m_tblisteMotAChercer.Name = "m_tblisteMotAChercer";
            this.m_tblisteMotAChercer.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.m_tblisteMotAChercer.Size = new System.Drawing.Size(304, 73);
            this.m_tblisteMotAChercer.TabIndex = 4;
            // 
            // m_btRemplir
            // 
            this.m_btRemplir.Location = new System.Drawing.Point(6, 39);
            this.m_btRemplir.Name = "m_btRemplir";
            this.m_btRemplir.Size = new System.Drawing.Size(125, 23);
            this.m_btRemplir.TabIndex = 5;
            this.m_btRemplir.Text = "remplir";
            this.m_btRemplir.UseVisualStyleBackColor = true;
            this.m_btRemplir.Click += new System.EventHandler(this.m_btRemplir_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 506);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "mots à cherchés : ";
            // 
            // m_lbNbMotAChercher
            // 
            this.m_lbNbMotAChercher.AutoSize = true;
            this.m_lbNbMotAChercher.Location = new System.Drawing.Point(114, 506);
            this.m_lbNbMotAChercher.Name = "m_lbNbMotAChercher";
            this.m_lbNbMotAChercher.Size = new System.Drawing.Size(0, 13);
            this.m_lbNbMotAChercher.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_btGenerer);
            this.groupBox1.Controls.Add(this.m_nUDCotee);
            this.groupBox1.Controls.Add(this.m_btRemplir);
            this.groupBox1.Location = new System.Drawing.Point(436, 522);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(150, 82);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "remplissage";
            // 
            // m_btGenerer
            // 
            this.m_btGenerer.Location = new System.Drawing.Point(70, 10);
            this.m_btGenerer.Name = "m_btGenerer";
            this.m_btGenerer.Size = new System.Drawing.Size(75, 23);
            this.m_btGenerer.TabIndex = 12;
            this.m_btGenerer.Text = "générer";
            this.m_btGenerer.UseVisualStyleBackColor = true;
            this.m_btGenerer.Click += new System.EventHandler(this.m_generer_Click_1);
            // 
            // m_nUDCotee
            // 
            this.m_nUDCotee.Location = new System.Drawing.Point(6, 13);
            this.m_nUDCotee.Name = "m_nUDCotee";
            this.m_nUDCotee.Size = new System.Drawing.Size(58, 20);
            this.m_nUDCotee.TabIndex = 7;
            this.m_nUDCotee.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // m_tbMotCaché
            // 
            this.m_tbMotCaché.Location = new System.Drawing.Point(324, 538);
            this.m_tbMotCaché.Name = "m_tbMotCaché";
            this.m_tbMotCaché.Size = new System.Drawing.Size(100, 20);
            this.m_tbMotCaché.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(321, 522);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "mot caché : ";
            // 
            // m_cbCorrection
            // 
            this.m_cbCorrection.FormattingEnabled = true;
            this.m_cbCorrection.Location = new System.Drawing.Point(720, 312);
            this.m_cbCorrection.Name = "m_cbCorrection";
            this.m_cbCorrection.Size = new System.Drawing.Size(121, 21);
            this.m_cbCorrection.TabIndex = 17;
            this.m_cbCorrection.SelectedIndexChanged += new System.EventHandler(this.m_cbNotATrouver_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(651, 320);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "correction : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(619, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "les mots déjà trouvés : ";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // m_tbGrilleMotTrouvE
            // 
            this.m_tbGrilleMotTrouvE.Location = new System.Drawing.Point(592, 24);
            this.m_tbGrilleMotTrouvE.Multiline = true;
            this.m_tbGrilleMotTrouvE.Name = "m_tbGrilleMotTrouvE";
            this.m_tbGrilleMotTrouvE.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.m_tbGrilleMotTrouvE.Size = new System.Drawing.Size(330, 287);
            this.m_tbGrilleMotTrouvE.TabIndex = 21;
            // 
            // m_btImprimer
            // 
            this.m_btImprimer.Enabled = false;
            this.m_btImprimer.Location = new System.Drawing.Point(322, 572);
            this.m_btImprimer.Name = "m_btImprimer";
            this.m_btImprimer.Size = new System.Drawing.Size(75, 23);
            this.m_btImprimer.TabIndex = 13;
            this.m_btImprimer.Text = "imprimer";
            this.m_btImprimer.UseVisualStyleBackColor = true;
            this.m_btImprimer.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(934, 633);
            this.Controls.Add(this.m_btImprimer);
            this.Controls.Add(this.m_tbGrilleMotTrouvE);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_cbCorrection);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_tbMotCaché);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.m_lbNbMotAChercher);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_tblisteMotAChercer);
            this.Controls.Add(this.m_tbCorrection);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_nUDCotee)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_tbCorrection;
        private System.Windows.Forms.TextBox m_tblisteMotAChercer;
        private System.Windows.Forms.Button m_btRemplir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label m_lbNbMotAChercher;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown m_nUDCotee;
        private System.Windows.Forms.Button m_btGenerer;
        private System.Windows.Forms.TextBox m_tbMotCaché;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox m_cbCorrection;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox m_tbGrilleMotTrouvE;
        private System.Windows.Forms.Button m_btImprimer;


    }
}

