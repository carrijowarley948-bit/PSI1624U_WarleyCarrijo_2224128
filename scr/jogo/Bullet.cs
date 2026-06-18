using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace jogo
{
    internal class Bullet
    {
        public string direcao;
        public int TiroHorizontal;
        public int TiroVertical;

        private int velocidade = 20;
        private PictureBox Tiro = new PictureBox();
        private Timer TempoTiro = new Timer();
        private Zumbi JogoZumbi; 

        public void Disparo(Zumbi zumbi)
        {
            JogoZumbi = zumbi;

            Tiro.BackColor = Color.Yellow;
            Tiro.Size = new Size(8, 8);
            Tiro.Tag = "bullet";
            Tiro.Left = TiroHorizontal;
            Tiro.Top = TiroVertical;
            Tiro.BringToFront();

            zumbi.Controls.Add(Tiro);

            TempoTiro.Interval = 30;
            TempoTiro.Tick += new EventHandler(EventoDisparo);
            TempoTiro.Start();
        }

        private void EventoDisparo(object sender, EventArgs e)
        {
           
            if (direcao == "left")
            {
                Tiro.Left -= velocidade;
            }
            if (direcao == "right")
            {
                Tiro.Left += velocidade;
            }
            if (direcao == "up")
            {
                Tiro.Top -= velocidade;
            }
            if (direcao == "down")
            {
                Tiro.Top += velocidade;
            }

            
            if (JogoZumbi != null)
            {
                if (Tiro.Left < -50 || Tiro.Left > JogoZumbi.ClientSize.Width + 50 ||
                    Tiro.Top < -50 || Tiro.Top > JogoZumbi.ClientSize.Height + 50)
                {

                    TempoTiro.Stop();
                    TempoTiro.Dispose();

                    if (Tiro != null && JogoZumbi.Controls.Contains(Tiro))
                    {
                        JogoZumbi.Controls.Remove(Tiro);
                        Tiro.Dispose();
                    }

                    TempoTiro = null;
                    Tiro = null;
                }
            }
            else
            {
                
                if (Tiro.Left < -100 || Tiro.Left > 1000 || Tiro.Top < -100 || Tiro.Top > 800)
                {
                    TempoTiro.Stop();
                    TempoTiro.Dispose();

                    if (    Tiro != null)
                    {
                        var form = Tiro.FindForm();
                        if (form != null && form.Controls.Contains(Tiro))
                        {
                            form.Controls.Remove(Tiro);
                        }
                        Tiro.Dispose();
                    }

                    TempoTiro = null;
                    Tiro = null;
                }
            }
        }
    }
}