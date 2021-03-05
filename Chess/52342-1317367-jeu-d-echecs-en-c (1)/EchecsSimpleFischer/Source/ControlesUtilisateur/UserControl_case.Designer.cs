namespace simpleFischer
{
    partial class UserControl_case
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControl_case));
            this.imageList_case = new System.Windows.Forms.ImageList(this.components);
            this.pictureBox_case = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_case)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList_case
            // 
            this.imageList_case.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_case.ImageStream")));
            this.imageList_case.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList_case.Images.SetKeyName(0, "001.png");
            this.imageList_case.Images.SetKeyName(1, "002.png");
            this.imageList_case.Images.SetKeyName(2, "CB1.png");
            this.imageList_case.Images.SetKeyName(3, "CB2.png");
            this.imageList_case.Images.SetKeyName(4, "CN1.png");
            this.imageList_case.Images.SetKeyName(5, "CN2.png");
            this.imageList_case.Images.SetKeyName(6, "DB1.png");
            this.imageList_case.Images.SetKeyName(7, "DB2.png");
            this.imageList_case.Images.SetKeyName(8, "DN1.png");
            this.imageList_case.Images.SetKeyName(9, "DN2.png");
            this.imageList_case.Images.SetKeyName(10, "FB1.png");
            this.imageList_case.Images.SetKeyName(11, "FB2.png");
            this.imageList_case.Images.SetKeyName(12, "FN1.png");
            this.imageList_case.Images.SetKeyName(13, "FN2.png");
            this.imageList_case.Images.SetKeyName(14, "PB1.png");
            this.imageList_case.Images.SetKeyName(15, "PB2.png");
            this.imageList_case.Images.SetKeyName(16, "PN1.png");
            this.imageList_case.Images.SetKeyName(17, "PN2.png");
            this.imageList_case.Images.SetKeyName(18, "RB1.png");
            this.imageList_case.Images.SetKeyName(19, "RB2.png");
            this.imageList_case.Images.SetKeyName(20, "RN1.png");
            this.imageList_case.Images.SetKeyName(21, "RN2.png");
            this.imageList_case.Images.SetKeyName(22, "TB1.png");
            this.imageList_case.Images.SetKeyName(23, "TB2.png");
            this.imageList_case.Images.SetKeyName(24, "TN1.png");
            this.imageList_case.Images.SetKeyName(25, "TN2.png");
            this.imageList_case.Images.SetKeyName(26, "CONTOUR1.png");
            this.imageList_case.Images.SetKeyName(27, "CONTOUR3.png");
            this.imageList_case.Images.SetKeyName(28, "CONTOUR2.png");
            this.imageList_case.Images.SetKeyName(29, "CONTOUR5.png");
            this.imageList_case.Images.SetKeyName(30, "CONTOUR4.png");
            // 
            // pictureBox_case
            // 
            this.pictureBox_case.BackColor = System.Drawing.Color.White;
            this.pictureBox_case.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_case.Name = "pictureBox_case";
            this.pictureBox_case.Size = new System.Drawing.Size(50, 50);
            this.pictureBox_case.TabIndex = 0;
            this.pictureBox_case.TabStop = false;
            this.pictureBox_case.Click += new System.EventHandler(this.pictureBox_case_Click);
            this.pictureBox_case.MouseEnter += new System.EventHandler(this.pictureBox_case_MouseEnter);
            this.pictureBox_case.MouseLeave += new System.EventHandler(this.pictureBox_case_MouseLeave);
            // 
            // UserControl_case
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox_case);
            this.Name = "UserControl_case";
            this.Size = new System.Drawing.Size(50, 50);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_case)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList_case;
        private System.Windows.Forms.PictureBox pictureBox_case;
    }
}
