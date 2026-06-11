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
    public partial class PongJogo : Form
    {
        int velocidadeBolaX = 4;
        int velocidadeBolaY = 4;
        int velocidade = 2;
        Random aleatorio = new Random();
        bool descer, subir;
        int velocidadeComputador = 50;
        int jogadorPontos = 0;
        int computadorPontos = 0;
        int jogadorVelocidade = 8;
        int[] i = { 5, 6, 8, 9 };
        int[] j = { 10, 9, 8, 11, 12 };

        int velocidadeMaxima = 20; 
        int contadorRebatidas = 0; 

        private bool jogoFinalizado = false;

        public PongJogo()
        {
            InitializeComponent();
        }

        private void PongJogo_Load(object sender, EventArgs e)
        {

        }

        private void EventoTempoJogo(object sender, EventArgs e)
        {
            bola.Top -= velocidadeBolaY;
            bola.Left -= velocidadeBolaX;

            this.Text = "Player: " + jogadorPontos + " | Computer: " + computadorPontos +
                        " | Velocidade: " + Math.Abs(velocidadeBolaX) +
                        " | Rebatidas: " + contadorRebatidas;

            if (bola.Top < 0 || bola.Bottom > this.ClientSize.Height)
            {
                velocidadeBolaY = -velocidadeBolaY;
            }

            if (bola.Left < -2)
            {
                bola.Left = 300;
                bola.Top = 150;
                velocidadeBolaX = -velocidadeBolaX;
                computadorPontos++;
                velocidadeComputador = 50;

                ResetarVelocidade();
            }

            if (bola.Right > this.ClientSize.Width + 2)
            {
                bola.Left = 300;
                bola.Top = 150;
                velocidadeBolaX = -velocidadeBolaX;
                jogadorPontos++;
                velocidadeComputador = 50;

                ResetarVelocidade();
            }

            if (computador.Top <= 1)
            {
                computador.Top = 0;
            }
            else if (computador.Bottom >= this.ClientSize.Height)
            {
                computador.Top = this.ClientSize.Height - computador.Height;
            }

            // Movimento da IA do computador
            if (bola.Top < computador.Top + (computador.Height / 2) && bola.Left > 300)
            {
                computador.Top -= velocidade;
            }
            if (bola.Top > computador.Top + (computador.Height / 2) && bola.Left > 300)
            {
                computador.Top += velocidade;
            }

            // Mudar velocidade da IA aleatoriamente
            velocidadeComputador -= 1;
            if (velocidadeComputador < 0)
            {
                velocidade = i[aleatorio.Next(i.Length)];
                velocidadeComputador = 50;
            }

            if (descer && jogador.Top + jogador.Height < this.ClientSize.Height)
            {
                jogador.Top += jogadorVelocidade;
            }
            if (subir && jogador.Top > 0)
            {
                jogador.Top -= jogadorVelocidade;
            }

            Colisao(bola, jogador);
            Colisao(bola, computador);

            if (!jogoFinalizado)
            {
                if (computadorPontos >= 5)
                {
                    jogoFinalizado = true;
                    Perdeu("Você perdeu!", jogadorPontos, false);
                }
                else if (jogadorPontos >= 5)
                {
                    jogoFinalizado = true;
                    Perdeu("Você ganhou!", jogadorPontos, true);
                }
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                descer = true;
            }
            if (e.KeyCode == Keys.Up)
            {
                subir = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                descer = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                subir = false;
            }
        }

        private void AumentarVelocidade()
        {
            contadorRebatidas++;

            float novaVelocidade = 4 + (contadorRebatidas * 0.5f);

            if (novaVelocidade > velocidadeMaxima)
            {
                novaVelocidade = velocidadeMaxima;
            }

            int velocidadeInteira = (int)novaVelocidade;

            velocidadeBolaX = Math.Sign(velocidadeBolaX) * velocidadeInteira;
            velocidadeBolaY = Math.Sign(velocidadeBolaY) * velocidadeInteira;
        }

        private void ResetarVelocidade()
        {
            contadorRebatidas = 0;
            velocidadeBolaX = 4;
            velocidadeBolaY = 4;
        }

        private void Colisao(PictureBox PicOne, PictureBox PicTwo)
        {
            if (PicOne.Bounds.IntersectsWith(PicTwo.Bounds))
            {
                AumentarVelocidade();

                if (PicOne.Left < PicTwo.Left)
                {
                    PicOne.Left = PicTwo.Left - PicOne.Width;
                }
                else
                {
                    PicOne.Left = PicTwo.Right;
                }

                velocidadeBolaX = -velocidadeBolaX ;

                int mudarDirecaoY = j[aleatorio.Next(j.Length)];
                if (velocidadeBolaY < 0)
                {
                    velocidadeBolaY = -mudarDirecaoY;
                }
                else
                {
                    velocidadeBolaY = mudarDirecaoY;
                }
            }
        }

        private void Retornar_Click(object sender, EventArgs e)
        {
            if (!jogoFinalizado && Contas.UsuarioLogado())
            {
                DialogResult Saida = MessageBox.Show("Você está saindo antes do fim do jogo. Isso será registrado como derrota. Confirmar saída?", "Sair do Jogo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (Saida == DialogResult.Yes)
                {
                    Contas.RegistrarPontuacao("Pong", jogadorPontos, false);
                    tempoJogo.Stop();
                    Main main = new Main();
                    main.Show();
                    this.Close();
                }
            }
            else
            {
                tempoJogo.Stop();
                Main main = new Main();
                main.Show();
                this.Close();
            }
        }

        private void RegistrarPontuacaoFinal(string mensagem, int pontuacaoFinal, bool venceu)
        {
            if (Contas.UsuarioLogado())
            {
                Contas.RegistrarPontuacao("Pong", pontuacaoFinal, venceu);

                MessageBox.Show($"{mensagem}\n\nTotal de rebatidas: {contadorRebatidas}\nSua pontuação: {pontuacaoFinal}\n\nPontuação registrada com sucesso!", "Fim de Jogo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"{mensagem}\n\nTotal de rebatidas: {contadorRebatidas}\nSua pontuação: {pontuacaoFinal}\n\nFaça login para registrar sua pontuação!", "Fim de Jogo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Perdeu(string message, int pontuacaoFinal, bool venceu)
        {
            tempoJogo.Stop();

            RegistrarPontuacaoFinal(message, pontuacaoFinal, venceu);

            computadorPontos = 0;
            jogadorPontos = 0;
            jogoFinalizado = false;

            ResetarVelocidade();
            bola.Left = 300;
            bola.Top = 150;
            velocidadeComputador = 50;
            velocidade = 2;

            tempoJogo.Start();
        }
    }
}