using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

// Note: At design time, I set Modifiers = Public for the
// textbox so the main program can read its value.
namespace howto_treenode_orientations
{
    public partial class NodeTextDialog : Form
    {
        public NodeTextDialog()
        {
            InitializeComponent();
        }

        // Replace Show so the program cannot use it.
        private new void Show()
        {
            throw new InvalidOperationException(
                "Use ShowDialog not Show to display this dialog");
        }
    }
}
