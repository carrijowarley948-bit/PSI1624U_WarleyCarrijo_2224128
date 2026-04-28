using System;
using System.IO;
using System.Windows.Forms;
using NAudio.Wave;

namespace jogo
{
    public partial class Main : Form
    {
        
        public Main()
        {
            InitializeComponent();
        }

        private void Jogos1_Click(object sender, EventArgs e)
        {
            Form1 jogos = new Form1();
            jogos.Show();
            this.Hide();
        }

        private void Música_Click(object sender, EventArgs e)
        {
            Prefere mus = new Prefere();
            mus.Show();
            this.Hide();

        }
            

        private void PLacar1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }


        
    }
}