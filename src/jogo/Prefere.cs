using System;
using System.IO;
using System.Windows.Forms;

namespace jogo
{
    public partial class Prefere : Form
    {
        public Prefere()
        {
            InitializeComponent();
        }

        private void Prefere_Load(object sender, EventArgs e)
        {
#if DEBUG
            VerificarArquivosMusica();
#endif
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string nomeMusica = "Grégoire_Lourme_-_The_Dark_Protector.mp3";

            string caminhoCompleto = Path.Combine(Application.StartupPath, nomeMusica);

            if (File.Exists(caminhoCompleto))
            {
                MusicManager.TocarMusica(nomeMusica);

                if (MusicManager.Ocupado())
                {
                    MessageBox.Show("A tocar: The Dark Protector", "Música", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show($"Arquivo não encontrado:\n{nomeMusica}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            string nomeMusica = "Aled_Edwards_-_All_In_My_Mind_-_Jazz_Dream_Pop.mp3";

            string caminhoCompleto = Path.Combine(Application.StartupPath, nomeMusica);

            if (File.Exists(caminhoCompleto))
            {
                MusicManager.TocarMusica(nomeMusica);

                if (MusicManager.Ocupado())
                {
                    MessageBox.Show("A tocar: All In My Mind", "Música", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show($"Arquivo não encontrado:\n{nomeMusica}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            string nomeMusica = "Alone_-_Color_Out.mp3";

            string caminhoCompleto = Path.Combine(Application.StartupPath, nomeMusica);

            if (File.Exists(caminhoCompleto))
            {
                MusicManager.TocarMusica(nomeMusica);

                if (MusicManager.Ocupado())
                {
                    MessageBox.Show("A tocar: Color Out", "Música", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show($"Arquivo não encontrado:\n{nomeMusica}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnPararMusica_Click(object sender, EventArgs e)
        {
            MusicManager.PararMusica();

            MessageBox.Show("Música parada!", "Música", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void NaoMusica_Click(object sender, EventArgs e)
        {
            MusicManager.PararMusica();

            MessageBox.Show("Música parada!", "Música", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Retornar_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            main.Show();
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
        }

        private void VerificarArquivosMusica()
        {
            string[] musicas =
            {
                "Grégoire_Lourme_-_The_Dark_Protector.mp3",
                "Aled_Edwards_-_All_In_My_Mind_-_Jazz_Dream_Pop.mp3",
                "Alone_-_Color_Out.mp3"
            };

            foreach (string musica in musicas)
            {
                string caminho = Path.Combine(Application.StartupPath, musica);

                if (File.Exists(caminho))
                {
                    System.Diagnostics.Debug.WriteLine($"Encontrado: {musica}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Não encontrado: {musica}");
                }
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void Prefere_Load_1(object sender, EventArgs e)
        {

        }
    }
}