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
    public partial class fEnd : Form
    {
        public fEnd()
        {
            InitializeComponent();
        }

        private void fEnd_Load(object sender, EventArgs e)
        {
            lblPlayer1.Text = fGame.play1.ToString();
            lblPlayer2.Text = fGame.play2.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void fEnd_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
