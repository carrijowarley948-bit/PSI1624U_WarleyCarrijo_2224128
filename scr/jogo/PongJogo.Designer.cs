namespace jogo
{
    partial class PongJogo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tempoJogo = new System.Windows.Forms.Timer(this.components);
            this.computador = new System.Windows.Forms.PictureBox();
            this.bola = new System.Windows.Forms.PictureBox();
            this.jogador = new System.Windows.Forms.PictureBox();
            this.Retornar = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.computador)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bola)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jogador)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Retornar)).BeginInit();
            this.SuspendLayout();
            // 
            // GameTimer
            // 
            this.tempoJogo.Enabled = true;
            this.tempoJogo.Interval = 20;
            this.tempoJogo.Tick += new System.EventHandler(this.EventoTempoJogo);
            // 
            // computer
            // 
            this.computador.Image = global::jogo.Properties.Resources.computer;
            this.computador.Location = new System.Drawing.Point(758, 146);
            this.computador.Name = "computer";
            this.computador.Size = new System.Drawing.Size(30, 120);
            this.computador.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.computador.TabIndex = 2;
            this.computador.TabStop = false;
            // 
            // ball
            // 
            this.bola.Image = global::jogo.Properties.Resources.ball;
            this.bola.Location = new System.Drawing.Point(382, 197);
            this.bola.Name = "ball";
            this.bola.Size = new System.Drawing.Size(30, 30);
            this.bola.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.bola.TabIndex = 1;
            this.bola.TabStop = false;
            // 
            // Player
            // 
            this.jogador.Image = global::jogo.Properties.Resources.player;
            this.jogador.Location = new System.Drawing.Point(12, 146);
            this.jogador.Name = "Player";
            this.jogador.Size = new System.Drawing.Size(30, 120);
            this.jogador.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.jogador.TabIndex = 0;
            this.jogador.TabStop = false;
            // 
            // Retornar
            // 
            this.Retornar.Image = global::jogo.Properties.Resources.Captura_de_ecrã_2026_04_30_155820;
            this.Retornar.Location = new System.Drawing.Point(325, 12);
            this.Retornar.Name = "Retornar";
            this.Retornar.Size = new System.Drawing.Size(136, 32);
            this.Retornar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Retornar.TabIndex = 3;
            this.Retornar.TabStop = false;
            this.Retornar.Click += new System.EventHandler(this.Retornar_Click);
            // 
            // PongJogo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.computador);
            this.Controls.Add(this.bola);
            this.Controls.Add(this.jogador);
            this.Controls.Add(this.Retornar);
            this.DoubleBuffered = true;
            this.Name = "PongJogo";
            this.Text = "PongJogo";
            this.Load += new System.EventHandler(this.PongJogo_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyIsDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyIsUp);
            ((System.ComponentModel.ISupportInitialize)(this.computador)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bola)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jogador)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Retornar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox jogador;
        private System.Windows.Forms.PictureBox bola;
        private System.Windows.Forms.PictureBox computador;
        private System.Windows.Forms.Timer tempoJogo;
        private System.Windows.Forms.PictureBox Retornar;
    }
}