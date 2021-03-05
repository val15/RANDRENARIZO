namespace simpleFischer
{
    partial class UserControl_cadran
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControl_cadran));
            this.imageList_digital = new System.Windows.Forms.ImageList(this.components);
            this.pictureBox_dizainesminutes = new System.Windows.Forms.PictureBox();
            this.pictureBox_minutes = new System.Windows.Forms.PictureBox();
            this.pictureBox_point = new System.Windows.Forms.PictureBox();
            this.pictureBox_dizainessecondes = new System.Windows.Forms.PictureBox();
            this.pictureBox_secondes = new System.Windows.Forms.PictureBox();
            this.pictureBox_play = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_dizainesminutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_minutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_point)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_dizainessecondes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_secondes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_play)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList_digital
            // 
            this.imageList_digital.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_digital.ImageStream")));
            this.imageList_digital.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_digital.Images.SetKeyName(0, "clockvide.bmp");
            this.imageList_digital.Images.SetKeyName(1, "clock0.bmp");
            this.imageList_digital.Images.SetKeyName(2, "clock1.bmp");
            this.imageList_digital.Images.SetKeyName(3, "clock2.bmp");
            this.imageList_digital.Images.SetKeyName(4, "clock3.bmp");
            this.imageList_digital.Images.SetKeyName(5, "clock4.bmp");
            this.imageList_digital.Images.SetKeyName(6, "clock5.bmp");
            this.imageList_digital.Images.SetKeyName(7, "clock6.bmp");
            this.imageList_digital.Images.SetKeyName(8, "clock7.bmp");
            this.imageList_digital.Images.SetKeyName(9, "clock8.bmp");
            this.imageList_digital.Images.SetKeyName(10, "clock9.bmp");
            this.imageList_digital.Images.SetKeyName(11, "clockplay.bmp");
            this.imageList_digital.Images.SetKeyName(12, "clockplayvide.bmp");
            this.imageList_digital.Images.SetKeyName(13, "clockpoint.bmp");
            this.imageList_digital.Images.SetKeyName(14, "clockpointvide.bmp");
            // 
            // pictureBox_dizainesminutes
            // 
            this.pictureBox_dizainesminutes.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_dizainesminutes.Name = "pictureBox_dizainesminutes";
            this.pictureBox_dizainesminutes.Size = new System.Drawing.Size(30, 50);
            this.pictureBox_dizainesminutes.TabIndex = 0;
            this.pictureBox_dizainesminutes.TabStop = false;
            // 
            // pictureBox_minutes
            // 
            this.pictureBox_minutes.Location = new System.Drawing.Point(27, 0);
            this.pictureBox_minutes.Name = "pictureBox_minutes";
            this.pictureBox_minutes.Size = new System.Drawing.Size(30, 50);
            this.pictureBox_minutes.TabIndex = 1;
            this.pictureBox_minutes.TabStop = false;
            // 
            // pictureBox_point
            // 
            this.pictureBox_point.Location = new System.Drawing.Point(36, 0);
            this.pictureBox_point.Name = "pictureBox_point";
            this.pictureBox_point.Size = new System.Drawing.Size(30, 50);
            this.pictureBox_point.TabIndex = 2;
            this.pictureBox_point.TabStop = false;
            // 
            // pictureBox_dizainessecondes
            // 
            this.pictureBox_dizainessecondes.Location = new System.Drawing.Point(63, 0);
            this.pictureBox_dizainessecondes.Name = "pictureBox_dizainessecondes";
            this.pictureBox_dizainessecondes.Size = new System.Drawing.Size(30, 50);
            this.pictureBox_dizainessecondes.TabIndex = 3;
            this.pictureBox_dizainessecondes.TabStop = false;
            // 
            // pictureBox_secondes
            // 
            this.pictureBox_secondes.Location = new System.Drawing.Point(88, 0);
            this.pictureBox_secondes.Name = "pictureBox_secondes";
            this.pictureBox_secondes.Size = new System.Drawing.Size(30, 50);
            this.pictureBox_secondes.TabIndex = 4;
            this.pictureBox_secondes.TabStop = false;
            // 
            // pictureBox_play
            // 
            this.pictureBox_play.Location = new System.Drawing.Point(124, 0);
            this.pictureBox_play.Name = "pictureBox_play";
            this.pictureBox_play.Size = new System.Drawing.Size(30, 50);
            this.pictureBox_play.TabIndex = 5;
            this.pictureBox_play.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // UserControl_cadran
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(153)))), ((int)(((byte)(133)))));
            this.Controls.Add(this.pictureBox_play);
            this.Controls.Add(this.pictureBox_secondes);
            this.Controls.Add(this.pictureBox_minutes);
            this.Controls.Add(this.pictureBox_dizainesminutes);
            this.Controls.Add(this.pictureBox_dizainessecondes);
            this.Controls.Add(this.pictureBox_point);
            this.Name = "UserControl_cadran";
            this.Size = new System.Drawing.Size(177, 79);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_dizainesminutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_minutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_point)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_dizainessecondes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_secondes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_play)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList_digital;
        private System.Windows.Forms.PictureBox pictureBox_dizainesminutes;
        private System.Windows.Forms.PictureBox pictureBox_minutes;
        private System.Windows.Forms.PictureBox pictureBox_point;
        private System.Windows.Forms.PictureBox pictureBox_dizainessecondes;
        private System.Windows.Forms.PictureBox pictureBox_secondes;
        private System.Windows.Forms.PictureBox pictureBox_play;
        private System.Windows.Forms.Timer timer1;
    }
}
