namespace jogo
{
    partial class Placar
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSelecionar = new System.Windows.Forms.Label();
            this.cmbJogo = new System.Windows.Forms.ComboBox();
            this.btnAtualizar = new System.Windows.Forms.Button();
            this.lblTotalJogadores = new System.Windows.Forms.Label();
            this.lblMelhorPontuacao = new System.Windows.Forms.Label();
            this.dgvPlacar = new System.Windows.Forms.DataGridView();
            this.btnRetornar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlacar)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.Gold;
            this.lblTitulo.Location = new System.Drawing.Point(214, 17);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(343, 35);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "🏆 RANKING DE PONTUAÇÕES 🏆";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSelecionar
            // 
            this.lblSelecionar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSelecionar.ForeColor = System.Drawing.Color.White;
            this.lblSelecionar.Location = new System.Drawing.Point(43, 69);
            this.lblSelecionar.Name = "lblSelecionar";
            this.lblSelecionar.Size = new System.Drawing.Size(103, 26);
            this.lblSelecionar.TabIndex = 1;
            this.lblSelecionar.Text = "Selecione o Jogo:";
            // 
            // cmbJogo
            // 
            this.cmbJogo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbJogo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbJogo.Items.AddRange(new object[] {
            "Todos os Jogos",
            "Damas",
            "Pong",
            "Zumbi"});
            this.cmbJogo.Location = new System.Drawing.Point(154, 69);
            this.cmbJogo.Name = "cmbJogo";
            this.cmbJogo.Size = new System.Drawing.Size(129, 25);
            this.cmbJogo.TabIndex = 2;
            // 
            // btnAtualizar
            // 
            this.btnAtualizar.BackColor = System.Drawing.Color.SteelBlue;
            this.btnAtualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAtualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAtualizar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAtualizar.ForeColor = System.Drawing.Color.White;
            this.btnAtualizar.Location = new System.Drawing.Point(300, 68);
            this.btnAtualizar.Name = "btnAtualizar";
            this.btnAtualizar.Size = new System.Drawing.Size(103, 30);
            this.btnAtualizar.TabIndex = 3;
            this.btnAtualizar.Text = "🔄 Atualizar";
            this.btnAtualizar.UseVisualStyleBackColor = false;
            // 
            // lblTotalJogadores
            // 
            this.lblTotalJogadores.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTotalJogadores.ForeColor = System.Drawing.Color.LightGreen;
            this.lblTotalJogadores.Location = new System.Drawing.Point(425, 69);
            this.lblTotalJogadores.Name = "lblTotalJogadores";
            this.lblTotalJogadores.Size = new System.Drawing.Size(171, 26);
            this.lblTotalJogadores.TabIndex = 4;
            this.lblTotalJogadores.Text = "Total de Jogadores: 0";
            // 
            // lblMelhorPontuacao
            // 
            this.lblMelhorPontuacao.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMelhorPontuacao.ForeColor = System.Drawing.Color.Gold;
            this.lblMelhorPontuacao.Location = new System.Drawing.Point(592, 68);
            this.lblMelhorPontuacao.Name = "lblMelhorPontuacao";
            this.lblMelhorPontuacao.Size = new System.Drawing.Size(154, 26);
            this.lblMelhorPontuacao.TabIndex = 5;
            this.lblMelhorPontuacao.Text = "Melhor Pontuação: 0";
            // 
            // dgvPlacar
            // 
            this.dgvPlacar.AllowUserToAddRows = false;
            this.dgvPlacar.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(70)))));
            this.dgvPlacar.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPlacar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPlacar.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(60)))));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Gold;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPlacar.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(60)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPlacar.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPlacar.GridColor = System.Drawing.Color.Gray;
            this.dgvPlacar.Location = new System.Drawing.Point(17, 113);
            this.dgvPlacar.Name = "dgvPlacar";
            this.dgvPlacar.ReadOnly = true;
            this.dgvPlacar.RowHeadersVisible = false;
            this.dgvPlacar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPlacar.Size = new System.Drawing.Size(729, 347);
            this.dgvPlacar.TabIndex = 6;
            this.dgvPlacar.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPlacar_CellContentClick);
            // 
            // btnRetornar
            // 
            this.btnRetornar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnRetornar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRetornar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRetornar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnRetornar.ForeColor = System.Drawing.Color.White;
            this.btnRetornar.Location = new System.Drawing.Point(257, 472);
            this.btnRetornar.Name = "btnRetornar";
            this.btnRetornar.Size = new System.Drawing.Size(257, 39);
            this.btnRetornar.TabIndex = 7;
            this.btnRetornar.Text = "← RETORNAR AO MENU PRINCIPAL";
            this.btnRetornar.UseVisualStyleBackColor = false;
            // 
            // Placar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(771, 537);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblSelecionar);
            this.Controls.Add(this.cmbJogo);
            this.Controls.Add(this.btnAtualizar);
            this.Controls.Add(this.lblTotalJogadores);
            this.Controls.Add(this.lblMelhorPontuacao);
            this.Controls.Add(this.dgvPlacar);
            this.Controls.Add(this.btnRetornar);
            this.Name = "Placar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Placar - Ranking do Fliperama";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlacar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSelecionar;
        private System.Windows.Forms.ComboBox cmbJogo;
        private System.Windows.Forms.Button btnAtualizar;
        private System.Windows.Forms.Label lblTotalJogadores;
        private System.Windows.Forms.Label lblMelhorPontuacao;
        private System.Windows.Forms.DataGridView dgvPlacar;
        private System.Windows.Forms.Button btnRetornar;
    }
}