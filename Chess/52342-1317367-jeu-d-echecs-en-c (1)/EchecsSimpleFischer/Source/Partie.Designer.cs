namespace simpleFischer
{
    partial class Partie
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

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.userControl_feuilleDeJeu1 = new simpleFischer.UserControl_feuilleDeJeu();
            this.userControl_pendule1 = new simpleFischer.UserControl_pendule();
            this.userControl_echiquier1 = new simpleFischer.UserControl_echiquier();
            this.SuspendLayout();
            // 
            // userControl_feuilleDeJeu1
            // 
            this.userControl_feuilleDeJeu1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.userControl_feuilleDeJeu1.Location = new System.Drawing.Point(420, 3);
            this.userControl_feuilleDeJeu1.Name = "userControl_feuilleDeJeu1";
            this.userControl_feuilleDeJeu1.Size = new System.Drawing.Size(126, 482);
            this.userControl_feuilleDeJeu1.TabIndex = 2;
            // 
            // userControl_pendule1
            // 
            this.userControl_pendule1.BackColor = System.Drawing.Color.Silver;
            this.userControl_pendule1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.userControl_pendule1.Location = new System.Drawing.Point(12, 3);
            this.userControl_pendule1.Name = "userControl_pendule1";
            this.userControl_pendule1.Size = new System.Drawing.Size(402, 74);
            this.userControl_pendule1.TabIndex = 1;
            // 
            // userControl_echiquier1
            // 
            this.userControl_echiquier1.BackColor = System.Drawing.Color.Black;
            this.userControl_echiquier1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.userControl_echiquier1.Location = new System.Drawing.Point(12, 83);
            this.userControl_echiquier1.Name = "userControl_echiquier1";
            this.userControl_echiquier1.Size = new System.Drawing.Size(402, 402);
            this.userControl_echiquier1.TabIndex = 0;
            // 
            // Partie
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 492);
            this.Controls.Add(this.userControl_feuilleDeJeu1);
            this.Controls.Add(this.userControl_pendule1);
            this.Controls.Add(this.userControl_echiquier1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Partie";
            this.Text = "Simple Fischer - Partie en cours";
            this.Load += new System.EventHandler(this.Partie_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl_echiquier userControl_echiquier1;
        private UserControl_pendule userControl_pendule1;
        private UserControl_feuilleDeJeu userControl_feuilleDeJeu1;
    }
}