using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Service_Import_and_Export
{
    public partial class Form1 : Form
    {
        private Form frm;
        public Form1()
        {
            InitializeComponent();
        }

        private void importationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm = new Import();
            frm.MdiParent = this;
            frm.Show();
        }

        private void exportationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm = new Export();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
