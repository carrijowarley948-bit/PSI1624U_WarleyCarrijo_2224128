using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace jogo
{
    public partial class Zumbi : Form
    {
        bool Esquerda, Direita, Subir, Descer, Perdeu;
        string olhando = "up";
        int vida = 100;
        int velocidade = 15;
        int municao = 10;
        int veloZumbi = 3;
        Random aleatorio = new Random();
        int Pontos;

        bool podeAtirar = true;
        Timer TempoTiro = new Timer();

        List<PictureBox> zombieList = new List<PictureBox>();

        public Zumbi()
        {
            InitializeComponent();

            this.KeyDown += TeclaUsada;
            this.KeyUp += TeclaNaoUsando;
            this.KeyPreview = true;

            TempoTiro.Interval = 200;
            TempoTiro.Tick += TempoDeTiro_Tick;

            Recomecar();
        }

        private void Zumbi_Load(object sender, EventArgs e)
        {

        }

        private void Pontos_Click(object sender, EventArgs e) 
        {

        }

        private void TempoDeTiro_Tick(object sender, EventArgs e)
        {
            podeAtirar = true;
            TempoTiro.Stop();
        }

        private void RegistrarPontuacaoFinal()
        {
            if (Contas.UsuarioLogado())
            {
                Contas.RegistrarPontuacao("Zumbi", Pontos, false);

                MessageBox.Show($"Fim de jogo!\n\nSua pontuação: {Pontos}\n\nPontuação registrada com sucesso!", "Fim de Jogo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"Fim de jogo!\n\nSua pontuação: {Pontos}\n\nFaça login para registrar sua pontuação!", "Fim de Jogo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void EventoTempo(object sender, EventArgs e)
        {
            if (vida > 1)
            {
                BarraDeVida.Value = vida;
            }
            else
            {
                Perdeu = true;
                Jogador.Image = Properties.Resources.dead;
                TempoJogo.Stop();

                RegistrarPontuacaoFinal();
            }

            Municoes.Text = "Munição: " + municao;
            PontosJogo.Text = "Pontos: " + Pontos;

            if (Esquerda == true && Jogador.Left > 0)
            {
                Jogador.Left -= velocidade;
            }

            if (Direita == true && Jogador.Left + Jogador.Width < this.ClientSize.Width)
            {
                Jogador.Left += velocidade;
            }

            if (Subir == true && Jogador.Top > 45)
            {
                Jogador.Top -= velocidade;
            }

            if (Descer == true && Jogador.Top + Jogador.Height < this.ClientSize.Height)
            {
                Jogador.Top += velocidade;
            }

            // Criar uma lista temporária para evitar erros de modificação durante iteração
            List<Control> controlesParaRemover = new List<Control>();
            List<PictureBox> zumbisParaRemover = new List<PictureBox>();

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox pictureBox && pictureBox.Tag != null)
                {
                    if (pictureBox.Tag.ToString() == "ammo")
                    {
                        if (Jogador.Bounds.IntersectsWith(pictureBox.Bounds))
                        {
                            controlesParaRemover.Add(pictureBox);
                            municao += 5;
                        }
                    }

                    if (pictureBox.Tag.ToString() == "zombie")
                    {
                        if (Jogador.Bounds.IntersectsWith(pictureBox.Bounds))
                        {
                            vida -= 1;
                        }

                        if (pictureBox.Left > Jogador.Left)
                        {
                            pictureBox.Left -= veloZumbi;
                            pictureBox.Image = Properties.Resources.zleft;
                        }
                        else if (pictureBox.Left < Jogador.Left)
                        {
                            pictureBox.Left += veloZumbi;
                            pictureBox.Image = Properties.Resources.zright;
                        }

                        if (pictureBox.Top > Jogador.Top)
                        {
                            pictureBox.Top -= veloZumbi;
                            pictureBox.Image = Properties.Resources.zup;
                        }
                        else if (pictureBox.Top < Jogador.Top)
                        {
                            pictureBox.Top += veloZumbi;
                            pictureBox.Image = Properties.Resources.zdown;
                        }

                        foreach (Control j in this.Controls)
                        {
                            if (j is PictureBox bulletBox && bulletBox.Tag != null && bulletBox.Tag.ToString() == "bullet")
                            {
                                if (pictureBox.Bounds.IntersectsWith(bulletBox.Bounds))
                                {
                                    Pontos++;
                                    controlesParaRemover.Add(bulletBox);
                                    zumbisParaRemover.Add(pictureBox);
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            foreach (Control controle in controlesParaRemover)
            {
                if (this.Controls.Contains(controle))
                {
                    this.Controls.Remove(controle);
                    controle.Dispose();
                }
            }

            foreach (PictureBox zumbi in zumbisParaRemover)
            {
                if (zombieList.Contains(zumbi))
                {
                    zombieList.Remove(zumbi);
                }
                if (this.Controls.Contains(zumbi))
                {
                    this.Controls.Remove(zumbi);
                    zumbi.Dispose();
                }
                FazZumbi(); 
            }
        }

        private void TeclaUsada(object sender, KeyEventArgs e)
        {
            if (Perdeu == true)
            {
                return;
            }

            if (e.KeyCode == Keys.Left)
            {
                Esquerda = true;
                olhando = "left";
                Jogador.Image = Properties.Resources.left;
            }

            if (e.KeyCode == Keys.Right)
            {
                Direita = true;
                olhando = "right";
                Jogador.Image = Properties.Resources.right;
            }

            if (e.KeyCode == Keys.Up)
            {
                Subir = true;
                olhando = "up";
                Jogador.Image = Properties.Resources.up;
            }

            if (e.KeyCode == Keys.Down)
            {
                Descer = true;
                olhando = "down";
                Jogador.Image = Properties.Resources.down;
            }
        }

        private void Municoes_Click(object sender, EventArgs e) 
        {

        }

        private void TeclaNaoUsando(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                Esquerda = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                Direita = false;
            }

            if (e.KeyCode == Keys.Up)
            {
                Subir = false;
            }

            if (e.KeyCode == Keys.Down)
            {
                Descer = false;
            }

            if (e.KeyCode == Keys.Space && municao > 0 && Perdeu == false && podeAtirar)
            {
                municao--;
                ShootBullet(olhando);
                podeAtirar = false;
                TempoTiro.Start();

                if (municao < 1)
                {
                    DropAmmo();
                }
            }

            if (e.KeyCode == Keys.Enter && Perdeu == true)
            {
                Recomecar();
            }
        }

        private void Retornar_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            main.Show();
            this.Close();
        }

        private void BarraDeVida_Click(object sender, EventArgs e)
        {

        }

        private void Jogador_Click(object sender, EventArgs e)
        {

        }

        private void ShootBullet(string direction)
        {
            Bullet tiro = new Bullet();
            tiro.direcao = direction;
            tiro.TiroHorizontal = Jogador.Left + (Jogador.Width / 2);
            tiro.TiroVertical = Jogador.Top + (Jogador.Height / 2);
            tiro.Disparo(this);
        }

        private void FazZumbi()
        {
            PictureBox zumbie = new PictureBox();
            zumbie.Tag = "zombie";
            zumbie.Image = Properties.Resources.zdown;
            zumbie.Left = aleatorio.Next(0, this.ClientSize.Width - 50);
            zumbie.Top = aleatorio.Next(0, this.ClientSize.Height - 50);
            zumbie.SizeMode = PictureBoxSizeMode.AutoSize;
            zombieList.Add(zumbie);
            this.Controls.Add(zumbie);
            Jogador.BringToFront();
        }

        private void DropAmmo()
        {
            PictureBox caixaMunicao = new PictureBox();
            caixaMunicao.Image = Properties.Resources.ammo_Image;
            caixaMunicao.SizeMode = PictureBoxSizeMode.AutoSize;
            caixaMunicao.Left = aleatorio.Next(10, this.ClientSize.Width - caixaMunicao.Width);
            caixaMunicao.Top = aleatorio.Next(60, this.ClientSize.Height - caixaMunicao.Height);
            caixaMunicao.Tag = "ammo";
            this.Controls.Add(caixaMunicao);
            caixaMunicao.BringToFront();
            Jogador.BringToFront();
        }

        private void Recomecar()
        {
            Jogador.Image = Properties.Resources.up;

            foreach (PictureBox i in zombieList)
            {
                if (this.Controls.Contains(i))
                {
                    this.Controls.Remove(i);
                    i.Dispose();
                }
            }
            zombieList.Clear();

            List<Control> controlesParaLimpar = new List<Control>();
            foreach (Control c in this.Controls)
            {
                if (c is PictureBox pb && pb.Tag != null)
                {
                    if (pb.Tag.ToString() == "bullet" || pb.Tag.ToString() == "ammo")
                    {
                        controlesParaLimpar.Add(c);
                    }
                }
            }

            foreach (Control c in controlesParaLimpar)
            {
                if (this.Controls.Contains(c))
                {
                    this.Controls.Remove(c);
                    c.Dispose();
                }
            }

            for (int i = 0; i < 3; i++)
            {
                FazZumbi();
            }

            Subir = false;
            Descer = false;
            Esquerda = false;
            Direita = false;
            Perdeu = false;

            vida = 100;
            Pontos = 0;
            municao = 10;

            podeAtirar = true;
            TempoTiro.Stop();

            Jogador.Left = (this.ClientSize.Width - Jogador.Width) / 2;
            Jogador.Top = (this.ClientSize.Height - Jogador.Height) / 2;

            TempoJogo.Start();
        }
    }
}