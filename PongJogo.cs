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
        int ballXspeed = 4;
        int ballYspeed = 4;
        int speed = 2;
        Random random = new Random();
        bool goDown, goUp;
        int computer_speed_change = 50;
        int playerScore = 0;
        int computerScore = 0;
        int playerSpeed = 8;
        int[] i = { 5, 6, 8, 9 };
        int[] j = { 10, 9, 8, 11, 12 };

        // NOVAS VARIÁVEIS PARA CONTROLE DE VELOCIDADE
        int maxSpeed = 20; // Velocidade máxima da bola
        int contadorRebatidas = 0; // Conta quantas vezes a bola foi rebatida

        public PongJogo()
        {
            InitializeComponent();
        }

        private void PongJogo_Load(object sender, EventArgs e)
        {

        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            ball.Top -= ballYspeed;
            ball.Left -= ballXspeed;

            // EXIBIR VELOCIDADE ATUAL NA BARRA DE TÍTULO
            this.Text = "Player: " + playerScore + " | Computer: " + computerScore +
                        " | Velocidade: " + Math.Abs(ballXspeed) +
                        " | Rebatidas: " + contadorRebatidas;

            // Colisão com topo e fundo
            if (ball.Top < 0 || ball.Bottom > this.ClientSize.Height)
            {
                ballYspeed = -ballYspeed;
            }

            // Bola saiu do lado esquerdo (ponto do computador)
            if (ball.Left < -2)
            {
                ball.Left = 300;
                ball.Top = 150;
                ballXspeed = -ballXspeed;
                computerScore++;
                computer_speed_change = 50;

                // Resetar contador e velocidade ao perder ponto
                ResetarVelocidade();
            }

            // Bola saiu do lado direito (ponto do jogador)
            if (ball.Right > this.ClientSize.Width + 2)
            {
                ball.Left = 300;
                ball.Top = 150;
                ballXspeed = -ballXspeed;
                playerScore++;
                computer_speed_change = 50;

                // Resetar contador e velocidade ao perder ponto
                ResetarVelocidade();
            }

            // Limitar movimento do computador
            if (computer.Top <= 1)
            {
                computer.Top = 0;
            }
            else if (computer.Bottom >= this.ClientSize.Height)
            {
                computer.Top = this.ClientSize.Height - computer.Height;
            }

            // Movimento da IA do computador
            if (ball.Top < computer.Top + (computer.Height / 2) && ball.Left > 300)
            {
                computer.Top -= speed;
            }
            if (ball.Top > computer.Top + (computer.Height / 2) && ball.Left > 300)
            {
                computer.Top += speed;
            }

            // Mudar velocidade da IA aleatoriamente
            computer_speed_change -= 1;
            if (computer_speed_change < 0)
            {
                speed = i[random.Next(i.Length)];
                computer_speed_change = 50;
            }

            // Movimento do jogador
            if (goDown && Player.Top + Player.Height < this.ClientSize.Height)
            {
                Player.Top += playerSpeed;
            }
            if (goUp && Player.Top > 0)
            {
                Player.Top -= playerSpeed;
            }

            // Verificar colisões (A VELOCIDADE AUMENTARÁ AQUI)
            CheckCollision(ball, Player);
            CheckCollision(ball, computer);

            // Verificar fim de jogo
            if (computerScore >= 5)
            {
                GameOver("Você perdeu!");
            }
            else if (playerScore >= 5)
            {
                GameOver("Você ganhou!");
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                goDown = true;
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
        }

        // NOVO MÉTODO PARA AUMENTAR A VELOCIDADE
        private void AumentarVelocidade()
        {
            // Aumenta o contador de rebatidas
            contadorRebatidas++;

            // Aumenta a velocidade baseado no número de rebatidas
            // Cada rebatida aumenta 0.5 de velocidade
            float novaVelocidade = 4 + (contadorRebatidas * 0.5f);

            // Limita à velocidade máxima
            if (novaVelocidade > maxSpeed)
            {
                novaVelocidade = maxSpeed;
            }

            // Aplica a nova velocidade mantendo a direção atual
            int velocidadeInteira = (int)novaVelocidade;

            // Mantém a direção atual (positivo ou negativo)
            ballXspeed = Math.Sign(ballXspeed) * velocidadeInteira;
            ballYspeed = Math.Sign(ballYspeed) * velocidadeInteira;
        }

        // NOVO MÉTODO PARA RESETAR VELOCIDADE
        private void ResetarVelocidade()
        {
            contadorRebatidas = 0;
            ballXspeed = 4;
            ballYspeed = 4;
        }

        private void CheckCollision(PictureBox PicOne, PictureBox PicTwo)
        {
            if (PicOne.Bounds.IntersectsWith(PicTwo.Bounds))
            {
                // AUMENTAR VELOCIDADE CADA VEZ QUE BATE NA RAQUETE
                AumentarVelocidade();

                // Ajustar posição da bola para não ficar presa
                if (PicOne.Left < PicTwo.Left)
                {
                    PicOne.Left = PicTwo.Left - PicOne.Width;
                }
                else
                {
                    PicOne.Left = PicTwo.Right;
                }

                // Inverter direção X
                ballXspeed = -ballXspeed;

                // Mudar direção Y aleatoriamente para dar imprevisibilidade
                int yChange = j[random.Next(j.Length)];
                if (ballYspeed < 0)
                {
                    ballYspeed = -yChange;
                }
                else
                {
                    ballYspeed = yChange;
                }
            }
        }

        private void GameOver(string message)
        {
            GameTimer.Stop();
            MessageBox.Show(message + "\n\nTotal de rebatidas: " + contadorRebatidas, "Fim de Jogo");

            // Resetar pontuação
            computerScore = 0;
            playerScore = 0;

            // Resetar posições e velocidade
            ResetarVelocidade();
            ball.Left = 300;
            ball.Top = 150;
            computer_speed_change = 50;
            speed = 2;

            GameTimer.Start();
        }
    }
}