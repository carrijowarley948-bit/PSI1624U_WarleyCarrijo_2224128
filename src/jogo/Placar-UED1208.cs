using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace jogo
{
    public partial class Placar : Form
    {
        private DataGridView dgvPlacar;
        private ComboBox cmbJogo;
        private Button btnAtualizar;
        private Button btnRetornar;
        private Label lblTitulo;
        private Label lblTotalJogadores;
        private Label lblMelhorPontuacao;

        public Placar()
        {
            InitializeComponent();
            CriarInterface();
            CarregarPlacar();
        }

        private void CriarInterface()
        {
            this.Text = "Placar - Ranking do Fliperama";
            this.Size = new Size(900, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(30, 30, 40);

            // Título
            lblTitulo = new Label();
            lblTitulo.Text = "🏆 RANKING DE PONTUAÇÕES 🏆";
            lblTitulo.Location = new Point(250, 20);
            lblTitulo.Size = new Size(400, 40);
            lblTitulo.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitulo.ForeColor = Color.Gold;
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblTitulo);

            // Label para selecionar jogo
            Label lblSelecionar = new Label();
            lblSelecionar.Text = "Selecione o Jogo:";
            lblSelecionar.Location = new Point(50, 80);
            lblSelecionar.Size = new Size(120, 30);
            lblSelecionar.Font = new Font("Segoe UI", 10);
            lblSelecionar.ForeColor = Color.White;
            this.Controls.Add(lblSelecionar);

            // ComboBox para escolher o jogo
            cmbJogo = new ComboBox();
            cmbJogo.Location = new Point(180, 80);
            cmbJogo.Size = new Size(150, 30);
            cmbJogo.Font = new Font("Segoe UI", 10);
            cmbJogo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbJogo.Items.AddRange(new string[] { "Todos os Jogos", "Damas", "Pong", "Zumbi" });
            cmbJogo.SelectedIndex = 0;
            cmbJogo.SelectedIndexChanged += CmbJogo_SelectedIndexChanged;
            this.Controls.Add(cmbJogo);

            // Botão Atualizar
            btnAtualizar = new Button();
            btnAtualizar.Text = "🔄 Atualizar";
            btnAtualizar.Location = new Point(350, 78);
            btnAtualizar.Size = new Size(120, 35);
            btnAtualizar.BackColor = Color.SteelBlue;
            btnAtualizar.ForeColor = Color.White;
            btnAtualizar.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnAtualizar.Click += BtnAtualizar_Click;
            this.Controls.Add(btnAtualizar);

            // Labels para estatísticas
            lblTotalJogadores = new Label();
            lblTotalJogadores.Text = "Total de Jogadores: 0";
            lblTotalJogadores.Location = new Point(550, 80);
            lblTotalJogadores.Size = new Size(200, 30);
            lblTotalJogadores.Font = new Font("Segoe UI", 10);
            lblTotalJogadores.ForeColor = Color.LightGreen;
            this.Controls.Add(lblTotalJogadores);

            lblMelhorPontuacao = new Label();
            lblMelhorPontuacao.Text = "Melhor Pontuação: 0";
            lblMelhorPontuacao.Location = new Point(750, 80);
            lblMelhorPontuacao.Size = new Size(150, 30);
            lblMelhorPontuacao.Font = new Font("Segoe UI", 10);
            lblMelhorPontuacao.ForeColor = Color.Gold;
            this.Controls.Add(lblMelhorPontuacao);

            // DataGridView para mostrar o placar
            dgvPlacar = new DataGridView();
            dgvPlacar.Location = new Point(20, 130);
            dgvPlacar.Size = new Size(850, 400);
            dgvPlacar.BackgroundColor = Color.FromArgb(50, 50, 60);
            dgvPlacar.ForeColor = Color.White;
            dgvPlacar.GridColor = Color.Gray;
            dgvPlacar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPlacar.AllowUserToAddRows = false;
            dgvPlacar.AllowUserToDeleteRows = false;
            dgvPlacar.ReadOnly = true;
            dgvPlacar.RowHeadersVisible = false;
            dgvPlacar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.Controls.Add(dgvPlacar);

            // Botão Retornar ao Menu
            btnRetornar = new Button();
            btnRetornar.Text = "← RETORNAR AO MENU PRINCIPAL";
            btnRetornar.Location = new Point(300, 550);
            btnRetornar.Size = new Size(300, 45);
            btnRetornar.BackColor = Color.FromArgb(220, 53, 69);
            btnRetornar.ForeColor = Color.White;
            btnRetornar.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnRetornar.FlatStyle = FlatStyle.Flat;
            btnRetornar.Cursor = Cursors.Hand;
            btnRetornar.Click += BtnRetornar_Click;
            this.Controls.Add(btnRetornar);
        }

        private void CarregarPlacar()
        {
            string jogoSelecionado = cmbJogo.SelectedItem.ToString();
            DataTable ranking = new DataTable();

            if (jogoSelecionado == "Todos os Jogos")
            {
                ranking = DataBase.ObterTop10Geral();
                ConfigurarGridGeral();
            }
            else
            {
                ranking = DataBase.ObterRankingPorJogo(jogoSelecionado);
                ConfigurarGridPorJogo();
            }

            if (ranking != null && ranking.Rows.Count > 0)
            {
                dgvPlacar.DataSource = ranking;
                AtualizarEstatisticas(ranking);
            }
            else
            {
                dgvPlacar.DataSource = null;
                lblTotalJogadores.Text = "Total de Jogadores: 0";
                lblMelhorPontuacao.Text = "Melhor Pontuação: 0";

                if (ranking == null)
                {
                    MessageBox.Show("Erro ao carregar os dados. Verifique a conexão com o banco de dados.",
                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Nenhuma pontuação registrada ainda!\n\nJogue e registre suas pontuações para aparecer no ranking.",
                        "Sem Dados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ConfigurarGridGeral()
        {
            if (dgvPlacar.Columns.Count > 0)
            {
                if (dgvPlacar.Columns.Contains("Posicao"))
                    dgvPlacar.Columns["Posicao"].HeaderText = "🏆 Posição";
                if (dgvPlacar.Columns.Contains("Nome"))
                    dgvPlacar.Columns["Nome"].HeaderText = "👤 Nome";
                if (dgvPlacar.Columns.Contains("Usuario"))
                    dgvPlacar.Columns["Usuario"].HeaderText = "📝 Usuário";
                if (dgvPlacar.Columns.Contains("Jogo"))
                    dgvPlacar.Columns["Jogo"].HeaderText = "🎮 Jogo";
                if (dgvPlacar.Columns.Contains("Pontuacao"))
                    dgvPlacar.Columns["Pontuacao"].HeaderText = "⭐ Pontuação";
                if (dgvPlacar.Columns.Contains("DataHora"))
                    dgvPlacar.Columns["DataHora"].HeaderText = "📅 Data/Hora";
            }
        }

        private void ConfigurarGridPorJogo()
        {
            if (dgvPlacar.Columns.Count > 0)
            {
                if (dgvPlacar.Columns.Contains("Posicao"))
                    dgvPlacar.Columns["Posicao"].HeaderText = "🏆 Posição";
                if (dgvPlacar.Columns.Contains("Nome"))
                    dgvPlacar.Columns["Nome"].HeaderText = "👤 Nome";
                if (dgvPlacar.Columns.Contains("Usuario"))
                    dgvPlacar.Columns["Usuario"].HeaderText = "📝 Usuário";
                if (dgvPlacar.Columns.Contains("MelhorPontuacao"))
                    dgvPlacar.Columns["MelhorPontuacao"].HeaderText = "⭐ Melhor Pontuação";
                if (dgvPlacar.Columns.Contains("PontuacaoTotal"))
                    dgvPlacar.Columns["PontuacaoTotal"].HeaderText = "📊 Pontuação Total";
                if (dgvPlacar.Columns.Contains("TotalJogos"))
                    dgvPlacar.Columns["TotalJogos"].HeaderText = "🎮 Total de Jogos";
                if (dgvPlacar.Columns.Contains("MediaPontuacao"))
                    dgvPlacar.Columns["MediaPontuacao"].HeaderText = "📈 Média";
                if (dgvPlacar.Columns.Contains("TotalVitorias"))
                    dgvPlacar.Columns["TotalVitorias"].HeaderText = "🏅 Vitórias";
            }
        }

        private void AtualizarEstatisticas(DataTable ranking)
        {
            if (ranking != null && ranking.Rows.Count > 0)
            {
                lblTotalJogadores.Text = $"Total de Jogadores: {ranking.Rows.Count}";

                int melhorPontuacao = 0;
                foreach (DataRow row in ranking.Rows)
                {
                    int pontuacao = 0;
                    if (ranking.Columns.Contains("Pontuacao"))
                        pontuacao = Convert.ToInt32(row["Pontuacao"]);
                    else if (ranking.Columns.Contains("MelhorPontuacao"))
                        pontuacao = Convert.ToInt32(row["MelhorPontuacao"]);

                    if (pontuacao > melhorPontuacao)
                        melhorPontuacao = pontuacao;
                }
                lblMelhorPontuacao.Text = $"🏆 Melhor: {melhorPontuacao} pontos";
            }
        }

        private void CmbJogo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarPlacar();
        }

        private void BtnAtualizar_Click(object sender, EventArgs e)
        {
            CarregarPlacar();
        }

        private void BtnRetornar_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            main.Show();
            this.Close();
        }
    }
}