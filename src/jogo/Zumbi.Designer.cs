namespace jogo
{
    partial class Zumbi
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
            this.Municoes = new System.Windows.Forms.Label();
            this.PontosJogo = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.BarraDeVida = new System.Windows.Forms.ProgressBar();
            this.TempoJogo = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Jogador = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Jogador)).BeginInit();
            this.SuspendLayout();
            // 
            // Municoes
            // 
            this.Municoes.AutoSize = true;
            this.Municoes.Font = new System.Drawing.Font("Showcard Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Municoes.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.Municoes.Location = new System.Drawing.Point(2, -1);
            this.Municoes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Municoes.Name = "Municoes";
            this.Municoes.Size = new System.Drawing.Size(84, 17);
            this.Municoes.TabIndex = 1;
            this.Municoes.Text = "Munição: 0";
            this.Municoes.Click += new System.EventHandler(this.Municoes_Click);
            // 
            // PontosJogo
            // 
            this.PontosJogo.AutoSize = true;
            this.PontosJogo.Font = new System.Drawing.Font("Showcard Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PontosJogo.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.PontosJogo.Location = new System.Drawing.Point(482, -1);
            this.PontosJogo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PontosJogo.Name = "PontosJogo";
            this.PontosJogo.Size = new System.Drawing.Size(77, 17);
            this.PontosJogo.TabIndex = 2;
            this.PontosJogo.Text = "Pontos: 0";
            this.PontosJogo.Click += new System.EventHandler(this.Pontos_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Showcard Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(972, -1);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Saude:";
            // 
            // BarraDeVida
            // 
            this.BarraDeVida.Location = new System.Drawing.Point(1030, -1);
            this.BarraDeVida.Margin = new System.Windows.Forms.Padding(2);
            this.BarraDeVida.Name = "BarraDeVida";
            this.BarraDeVida.Size = new System.Drawing.Size(164, 19);
            this.BarraDeVida.TabIndex = 4;
            this.BarraDeVida.Value = 100;
            this.BarraDeVida.Click += new System.EventHandler(this.BarraDeVida_Click);
            // 
            // TempoJogo
            // 
            this.TempoJogo.Interval = 20;
            this.TempoJogo.Tick += new System.EventHandler(this.EventoTempo);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::jogo.Properties.Resources.Captura_de_ecrã_2026_04_23_145342;
            this.pictureBox1.Location = new System.Drawing.Point(5, 509);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(276, 121);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.Retornar_Click);
            // 
            // Jogador
            // 
            this.Jogador.Image = global::jogo.Properties.Resources.up;
            this.Jogador.Location = new System.Drawing.Point(210, 404);
            this.Jogador.Margin = new System.Windows.Forms.Padding(2);
            this.Jogador.Name = "Jogador";
            this.Jogador.Size = new System.Drawing.Size(71, 100);
            this.Jogador.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.Jogador.TabIndex = 5;
            this.Jogador.TabStop = false;
            this.Jogador.Click += new System.EventHandler(this.Jogador_Click);
            // 
            // Zumbi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1227, 657);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Jogador);
            this.Controls.Add(this.BarraDeVida);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PontosJogo);
            this.Controls.Add(this.Municoes);
            this.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Zumbi";
            this.Text = "Zumbi";
            this.Load += new System.EventHandler(this.Zumbi_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TeclaUsada);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TeclaNaoUsando);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Jogador)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Municoes;
        private System.Windows.Forms.Label PontosJogo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar BarraDeVida;
        private System.Windows.Forms.PictureBox Jogador;
        private System.Windows.Forms.Timer TempoJogo;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}