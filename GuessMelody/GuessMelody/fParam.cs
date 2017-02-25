using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GuessMelody
{
    public partial class fParam : Form
    {
        public fParam()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Buffer.AllDirectories = cbAllDirectory.Checked;
            try
            {
                Buffer.gameDuration = Convert.ToInt32(cbGameDuration.Text);
            }
            catch
            {
                Buffer.gameDuration = 60;
            }
            if (Buffer.gameDuration <= 10) Buffer.gameDuration = 60;
            try
            {
                Buffer.musicDuration = Convert.ToInt32(cbMusicDuration.Text);
            }
            catch
            {
                Buffer.musicDuration = 10;
            }
            if (Buffer.musicDuration <= 1) Buffer.musicDuration = 10;
            Buffer.randomStart = cbRandomStart.Checked;
            Buffer.WriteParam();
            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Set();
            this.Hide();
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            
            if (fbd.ShowDialog() == DialogResult.OK) 
            {
                try
                {
                    string[] music_list = Directory.GetFiles(fbd.SelectedPath, "*.mp3", cbAllDirectory.Checked ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
                    Buffer.lastFolder = fbd.SelectedPath;
                    listBox1.Items.Clear();
                    listBox1.Items.AddRange(music_list);
                    Buffer.list.Clear();
                    Buffer.list.AddRange(music_list);
                }
                catch
                {
                    MessageBox.Show("Доступ к запрашиваемым папкам запрещён!");
                }
            };
        }
        void Set()
        {
            cbAllDirectory.Checked = Buffer.AllDirectories;
            cbGameDuration.Text = Buffer.gameDuration.ToString();
            cbMusicDuration.Text = Buffer.musicDuration.ToString();
            cbRandomStart.Checked = Buffer.randomStart;
        }

        private void fParam_Load(object sender, EventArgs e)
        {
            Set();
            listBox1.Items.Clear();
            listBox1.Items.AddRange(Buffer.list.ToArray());
        }

        private void btnClearList_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }
    }
}
