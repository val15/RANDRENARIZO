namespace simpleFischer
{
    partial class UserControl_feuilleDeJeu
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
            this.dataGridView_coups = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_coups)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_coups
            // 
            this.dataGridView_coups.AllowUserToAddRows = false;
            this.dataGridView_coups.AllowUserToDeleteRows = false;
            this.dataGridView_coups.AllowUserToResizeColumns = false;
            this.dataGridView_coups.AllowUserToResizeRows = false;
            this.dataGridView_coups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_coups.ColumnHeadersVisible = false;
            this.dataGridView_coups.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_coups.Name = "dataGridView_coups";
            this.dataGridView_coups.ReadOnly = true;
            this.dataGridView_coups.RowHeadersVisible = false;
            this.dataGridView_coups.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView_coups.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_coups.Size = new System.Drawing.Size(90, 600);
            this.dataGridView_coups.TabIndex = 0;
            // 
            // UserControl_feuilleDeJeu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView_coups);
            this.Name = "UserControl_feuilleDeJeu";
            this.Size = new System.Drawing.Size(272, 600);
            this.Resize += new System.EventHandler(this.UserControl_feuilleDeJeu_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_coups)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_coups;
    }
}
