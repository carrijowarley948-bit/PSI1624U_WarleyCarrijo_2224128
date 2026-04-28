using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace jogo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox4_Click(object sender, EventArgs e) // Dama
        {
            Dama dama = new Dama();
            dama.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e) // Zumbi
        {
            Zumbi zumbi = new Zumbi();
            zumbi.Show();
            this.Hide();
        }

        private void Nave_Click(object sender, EventArgs e) 
        {
            PongJogo pong = new PongJogo();
            pong.Show();
            this.Hide();
        }
    }
}
