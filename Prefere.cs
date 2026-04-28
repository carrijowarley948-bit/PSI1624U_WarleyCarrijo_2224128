using System;
using System.Windows.Forms;

namespace jogo
{
    public partial class Prefere : Form
    {
        public Prefere()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Tocar música usando o MusicManager global
            MusicManager.PlayMusic();

            // Opcional: Mostrar mensagem apenas se a música acabou de começar
            if (MusicManager.IsPlaying())
            {
                MessageBox.Show("Música a tocar!", "Música",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Botão para parar a música
        private void btnPararMusica_Click(object sender, EventArgs e)
        {
            MusicManager.StopMusic();
            MessageBox.Show("Música parada!", "Música",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Botão "Não" - para parar a música
        private void NaoMusica_Click(object sender, EventArgs e)
        {
            MusicManager.StopMusic();
            MessageBox.Show("Música parada!", "Música",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Botão para retornar ao Main (a música continua a tocar)
        private void Retornar_Click(object sender, EventArgs e)
        {
            // Não para a música! Apenas muda de formulário
            Main main = new Main();
            main.Show();
            this.Close();
        }

        // Quando fechar o formulário, NÃO para a música (mas não é necessário fazer nada)
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Não para a música quando fecha este formulário
            // A música continua a tocar em segundo plano
            base.OnFormClosing(e);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}