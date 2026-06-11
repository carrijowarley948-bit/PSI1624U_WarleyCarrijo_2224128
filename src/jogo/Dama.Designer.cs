namespace jogo
{
    partial class Dama
    {
        /// <summary>
        /// Variável necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar recursos.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing &&
                (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Forms Designer

        private void InitializeComponent()
        {
            this.lblTurno = new System.Windows.Forms.Label();
            this.lblPontuacao = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnRetornar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTurno
            // 
            this.lblTurno.AutoSize = true;
            this.lblTurno.Font = new System.Drawing.Font(
                "Arial",
                12F,
                System.Drawing.FontStyle.Bold);

            this.lblTurno.Location =
                new System.Drawing.Point(40, 15);

            this.lblTurno.Name = "lblTurno";

            this.lblTurno.Size =
                new System.Drawing.Size(220, 19);

            this.lblTurno.TabIndex = 0;

            this.lblTurno.Text =
                "Turno: Jogador 1 (Brancas)";

            // 
            // lblPontuacao
            // 
            this.lblPontuacao.AutoSize = true;

            this.lblPontuacao.Font =
                new System.Drawing.Font(
                    "Arial",
                    10F,
                    System.Drawing.FontStyle.Bold);

            this.lblPontuacao.ForeColor =
                System.Drawing.Color.DarkBlue;

            this.lblPontuacao.Location =
                new System.Drawing.Point(350, 18);

            this.lblPontuacao.Name =
                "lblPontuacao";

            this.lblPontuacao.Size =
                new System.Drawing.Size(170, 16);

            this.lblPontuacao.TabIndex = 1;

            this.lblPontuacao.Text =
                "Brancas: 0 | Pretas: 0";

            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;

            this.lblStatus.Font =
                new System.Drawing.Font("Arial", 10F);

            this.lblStatus.Location =
                new System.Drawing.Point(40, 560);

            this.lblStatus.Name = "lblStatus";

            this.lblStatus.Size =
                new System.Drawing.Size(250, 16);

            this.lblStatus.TabIndex = 2;

            this.lblStatus.Text =
                "Clique em uma peça";

            // 
            // btnReset
            // 
            this.btnReset.Location =
                new System.Drawing.Point(50, 600);

            this.btnReset.Name = "btnReset";

            this.btnReset.Size =
                new System.Drawing.Size(100, 35);

            this.btnReset.TabIndex = 3;

            this.btnReset.Text = "Novo Jogo";

            this.btnReset.UseVisualStyleBackColor = true;

            this.btnReset.Click +=
                new System.EventHandler(this.BtnReset_Click);

            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor =
                System.Drawing.Color.LightCoral;

            this.btnCancelar.Enabled = false;

            this.btnCancelar.Location =
                new System.Drawing.Point(180, 600);

            this.btnCancelar.Name =
                "btnCancelar";

            this.btnCancelar.Size =
                new System.Drawing.Size(100, 35);

            this.btnCancelar.TabIndex = 4;

            this.btnCancelar.Text =
                "Cancelar";

            this.btnCancelar.UseVisualStyleBackColor = false;

            this.btnCancelar.Click +=
                new System.EventHandler(this.BtnCancelar_Click);

            // 
            // btnRetornar
            // 
            this.btnRetornar.BackColor =
                System.Drawing.Color.LightGreen;

            this.btnRetornar.Location =
                new System.Drawing.Point(450, 600);

            this.btnRetornar.Name =
                "btnRetornar";

            this.btnRetornar.Size =
                new System.Drawing.Size(100, 35);

            this.btnRetornar.TabIndex = 5;

            this.btnRetornar.Text =
                "Retornar";

            this.btnRetornar.UseVisualStyleBackColor = false;

            this.btnRetornar.Click +=
                new System.EventHandler(this.BtnRetornar_Click);

            // 
            // Dama
            // 
            this.AutoScaleDimensions =
                new System.Drawing.SizeF(6F, 13F);

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;

            this.BackColor =
                System.Drawing.Color.LightGray;

            this.ClientSize =
                new System.Drawing.Size(600, 700);

            this.Controls.Add(this.btnRetornar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblPontuacao);
            this.Controls.Add(this.lblTurno);

            this.Name = "Dama";

            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;

            this.Text = "Jogo de Damas";

            this.Load +=
                new System.EventHandler(this.Dama_Load);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTurno;
        private System.Windows.Forms.Label lblPontuacao;
        private System.Windows.Forms.Label lblStatus;

        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnRetornar;
    }
}