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
    public partial class fGame : Form
    {
        Random rnd = new Random();
        int musicDuration = Buffer.musicDuration;
        bool[] players = new bool[2];
        fEnd fe = new fEnd();
        static public string play1 = "0";
        static public string play2 = "0";

        public fGame()
        {
            InitializeComponent();
        }

        void MakeMusic()
        {
            if (Buffer.list.Count == 0) EndGame();
            else
            {
                musicDuration = Buffer.musicDuration;
            int n = rnd.Next(0, Buffer.list.Count);
            WMP.URL = Buffer.list[n];
            Buffer.answer = System.IO.Path.GetFileNameWithoutExtension(WMP.URL);
            WMP.Ctlcontrols.play();
            Buffer.list.RemoveAt(n);
            lblMelodyCount.Text = Buffer.list.Count.ToString();
            players[0] = false;
            players[1] = false;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            timer1.Start();
            MakeMusic();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void fGame_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
            WMP.Ctlcontrols.stop();
        }

        private void fGame_Load(object sender, EventArgs e)
        {
            lblMelodyCount.Text = Buffer.list.Count.ToString();
            progressBar1.Value = 0;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = Buffer.gameDuration;
            lblMusicDuration.Text = musicDuration.ToString();
            int n = rnd.Next(0, Buffer.list.Count);
        }

        void EndGame()
        {
            timer1.Stop();
            WMP.Ctlcontrols.stop();
            fe.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value++;
            musicDuration--;
            lblMusicDuration.Text = musicDuration.ToString();
            if (progressBar1.Value == progressBar1.Maximum)
            {
                EndGame();
                return;
            }
            if (musicDuration == 0) MakeMusic();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            GamePause();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            if (Buffer.list.Count == 0) EndGame();
            GamePlay();
        }

        void GamePause()
        {
            timer1.Stop();
            WMP.Ctlcontrols.pause();
        }
        void GamePlay()
        {
             timer1.Start();
             WMP.Ctlcontrols.play();
        }

        private void fGame_KeyDown(object sender, KeyEventArgs e)
        {
            if (!timer1.Enabled) return;
                if (players[0] == false && e.KeyData == Keys.A)
                {
                    GamePause();
                    fMessage fm = new fMessage();
                    fm.lblMessage.Text = "Игрок 1 угадал?";
                    players[0] = true;
                    if (fm.ShowDialog() == DialogResult.Yes)
                    {
                        lblCounter1.Text = Convert.ToString(Convert.ToInt64(lblCounter1.Text) + 1);
                        if (Buffer.list.Count == 0) play1 = Convert.ToString(Convert.ToInt64(play1) + 1);
                        MakeMusic();
                    }
                    GamePlay();
                }
                if (players[1] == false && e.KeyData == Keys.L)
                {
                    GamePause();
                    fMessage fm = new fMessage();
                    fm.lblMessage.Text = "Игрок 2 угадал?";
                    players[1] = true;
                    if (fm.ShowDialog() == DialogResult.Yes)
                    {
                        lblCounter2.Text = Convert.ToString(Convert.ToInt64(lblCounter2.Text) + 1);
                        if (Buffer.list.Count == 0) play2 = Convert.ToString(Convert.ToInt64(play2) + 1);
                        MakeMusic();
                    }
                    GamePlay();
                }
                play1 = lblCounter1.Text;
                play2 = lblCounter2.Text;
        }

        private void WMP_OpenStateChange(object sender, AxWMPLib._WMPOCXEvents_OpenStateChangeEvent e)
        {
            if (Buffer.randomStart == true)
                if (WMP.openState == WMPLib.WMPOpenState.wmposMediaOpen)
                    WMP.Ctlcontrols.currentPosition = rnd.Next(0, (int)WMP.currentMedia.duration / 2);
        } 

        private void lblCounter1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) (sender as Label).Text = Convert.ToString(Convert.ToInt64((sender as Label).Text) + 1);
            if (e.Button == MouseButtons.Right) (sender as Label).Text = Convert.ToString(Convert.ToInt64((sender as Label).Text) - 1);
            play1 = lblCounter1.Text;
            play2 = lblCounter2.Text;
        }
    }
}
