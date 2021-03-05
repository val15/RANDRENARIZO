using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace simpleFischer
{
    /// <summary>
    /// Fenêtre de dialogue d'affichage du résultat de partie. </summary>
    /// <remarks>
    /// La gestion de cette fenêtre se fait au niveau de la classe Partie
    /// </remarks>
    public partial class Form_resultat : Form
    {
        /// <summary>
        /// Constructeur de la classe. </summary>
        public Form_resultat(String texte)
        {
            InitializeComponent();
            this.label1.Text = texte;

        }

    }
}
