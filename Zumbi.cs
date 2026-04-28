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
        bool goLeft, goRight, goUp, goDown, gameOver;
        string facing = "up";
        int playerHealth = 100;
        int speed = 15;
        int ammo = 10;
        int zombieSpeed = 3;
        Random randNum = new Random();
        int score;

        List<PictureBox> zombieList = new List<PictureBox>();

        public Zumbi()
        {
            InitializeComponent();

            // Conectar eventos do teclado
            this.KeyDown += KeyIsDown;
            this.KeyUp += KeyIsUp;
            this.KeyPreview = true;

            RestartGame();
        }

        private void Zumbi_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e) //pontos
        {

        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            if (playerHealth > 1)
            {
                HealtBar.Value = playerHealth;
            }
            else
            {
                gameOver = true;
                Player.Image = Properties.Resources.dead;
                GamerTimer.Stop();
            }

            label1.Text = "Munição: " + ammo;
            label2.Text = "Pontos: " + score;

            if (goLeft == true && Player.Left > 0)
            {
                Player.Left -= speed;
            }

            if (goRight == true && Player.Left + Player.Width < this.ClientSize.Width)
            {
                Player.Left += speed;
            }

            if (goUp == true && Player.Top > 45)
            {
                Player.Top -= speed;
            }

            if (goDown == true && Player.Top + Player.Height < this.ClientSize.Height)
            {
                Player.Top += speed;
            }

            foreach (Control x in this.Controls)
            {
                
                if (x is PictureBox pictureBox && pictureBox.Tag != null)
                {
                    if (pictureBox.Tag.ToString() == "ammo")
                    {
                        if (Player.Bounds.IntersectsWith(pictureBox.Bounds))
                        {
                            this.Controls.Remove(pictureBox);
                            pictureBox.Dispose();
                            ammo += 5;
                        }
                    }

                    if (pictureBox.Tag.ToString() == "zombie")
                    {
                        if (Player.Bounds.IntersectsWith(pictureBox.Bounds))
                        {
                            playerHealth -= 1;
                        }

                        // Movimento do zumbi
                        if (pictureBox.Left > Player.Left)
                        {
                            pictureBox.Left -= zombieSpeed;
                            pictureBox.Image = Properties.Resources.zleft;
                        }
                        else if (pictureBox.Left < Player.Left)
                        {
                            pictureBox.Left += zombieSpeed;
                            pictureBox.Image = Properties.Resources.zright;
                        }

                        if (pictureBox.Top > Player.Top)
                        {
                            pictureBox.Top -= zombieSpeed;
                            pictureBox.Image = Properties.Resources.zup;
                        }
                        else if (pictureBox.Top < Player.Top)
                        {
                            pictureBox.Top += zombieSpeed;
                            pictureBox.Image = Properties.Resources.zdown;
                        }

                        // Verificar colisão com balas
                        foreach (Control j in this.Controls)
                        {
                            if (j is PictureBox bulletBox && bulletBox.Tag != null && bulletBox.Tag.ToString() == "bullet")
                            {
                                if (pictureBox.Bounds.IntersectsWith(bulletBox.Bounds))
                                {
                                    score++;
                                    this.Controls.Remove(bulletBox);
                                    bulletBox.Dispose();
                                    this.Controls.Remove(pictureBox);
                                    pictureBox.Dispose();
                                    zombieList.Remove(pictureBox);
                                    MakeZombies();
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (gameOver == true)
            {
                return;
            }

            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
                facing = "left";
                Player.Image = Properties.Resources.left;
            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
                facing = "right";
                Player.Image = Properties.Resources.right;
            }

            if (e.KeyCode == Keys.Up)
            {
                goUp = true;
                facing = "up";
                Player.Image = Properties.Resources.up;
            }

            if (e.KeyCode == Keys.Down)
            {
                goDown = true;
                facing = "down";
                Player.Image = Properties.Resources.down;
            }
        }

        private void label1_Click(object sender, EventArgs e) //munição
        {

        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }

            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }

            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }

            if (e.KeyCode == Keys.Space && ammo > 0 && gameOver == false)
            {
                ammo--;
                ShootBullet(facing);

                if (ammo < 1)
                {
                    DropAmmo();
                }
            }
            if (e.KeyCode == Keys.Enter && gameOver == true)
            {
                RestartGame();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            main.Show();
            this.Close();
        }

        private void ShootBullet(string direction)
        {
            Bullet shotBullet = new Bullet();
            shotBullet.direction = direction;
            shotBullet.bulletLeft = Player.Left + (Player.Width / 2);
            shotBullet.bulletTop = Player.Top + (Player.Height / 2); 
            shotBullet.MakeBullet(this);
        }

        private void MakeZombies()
        {
            PictureBox zombie = new PictureBox();
            zombie.Tag = "zombie";
            zombie.Image = Properties.Resources.zdown;
            zombie.Left = randNum.Next(0, this.ClientSize.Width - 50); 
            zombie.Top = randNum.Next(0, this.ClientSize.Height - 50);  
            zombie.SizeMode = PictureBoxSizeMode.AutoSize;
            zombieList.Add(zombie);
            this.Controls.Add(zombie);
            Player.BringToFront();
        }

        private void DropAmmo()
        {
            PictureBox ammoPicture = new PictureBox();
            ammoPicture.Image = Properties.Resources.ammo_Image;
            ammoPicture.SizeMode = PictureBoxSizeMode.AutoSize;
            ammoPicture.Left = randNum.Next(10, this.ClientSize.Width - ammoPicture.Width);
            ammoPicture.Top = randNum.Next(60, this.ClientSize.Height - ammoPicture.Height);
            ammoPicture.Tag = "ammo";
            this.Controls.Add(ammoPicture);
            ammoPicture.BringToFront();
            Player.BringToFront();
        }

        private void RestartGame()
        {
            Player.Image = Properties.Resources.up;

            foreach (PictureBox i in zombieList)
            {
                this.Controls.Remove(i);
                i.Dispose();
            }

            zombieList.Clear();

            for (int i = 0; i < 3; i++)
            {
                MakeZombies();
            }

            goUp = false;
            goDown = false;
            goLeft = false;
            goRight = false;
            gameOver = false;

            playerHealth = 100;
            score = 0;
            ammo = 10;

            GamerTimer.Start();
        }
    }
}