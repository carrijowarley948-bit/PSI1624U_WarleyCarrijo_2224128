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

        private void Dama_Click(object sender, EventArgs e) 
        {
            Dama dama = new Dama();
            dama.Show();
            this.Hide();
        }

        private void Zumbi_Click(object sender, EventArgs e) 
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            Main main = new Main();
            main.Show();
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
