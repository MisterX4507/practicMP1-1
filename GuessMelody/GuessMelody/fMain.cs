using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuessMelody
{
    public partial class fMain : Form
    {
        fGame fg = new fGame();
        fParam fp = new fParam();
        fInstuction fi = new fInstuction();

        public fMain()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            Buffer.ReadParam();
            Buffer.ReadMusic();
        }

        private void btnParams_Click(object sender, EventArgs e)
        {
            fp.ShowDialog();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            fg.ShowDialog();
        }

        private void btnInstuction_Click(object sender, EventArgs e)
        {
            fi.ShowDialog();
        }
    }
}
