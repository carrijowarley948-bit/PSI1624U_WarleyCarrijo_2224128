using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace jogo
{
    public partial class Placar : Form
    {
        public Placar()
        {
            InitializeComponent();
            CarregarRanking();

            this.cmbJogo.SelectedIndexChanged += CmbJogo_SelectedIndexChanged;
            this.btnAtualizar.Click += BtnAtualizar_Click;
            this.btnRetornar.Click += BtnRetornar_Click;
        }

        private void CarregarRanking()
        {
            if (cmbJogo.SelectedItem == null) return;

            string jogoSelecionado = cmbJogo.SelectedItem.ToString();
            DataTable ranking = null;

            try
            {
                if (jogoSelecionado == "Todos os Jogos")
                {
                    ranking = DataBase.ObterRankingGeralMelhorPontuacao();
                }
                else
                {
                    ranking = DataBase.ObterRankingPorJogo(jogoSelecionado);
                }

                if (dgvPlacar != null)
                {
                    dgvPlacar.DataSource = null;
                    dgvPlacar.Rows.Clear();
                    dgvPlacar.Columns.Clear();
                }

                if (ranking == null)
                {
                    lblTotalJogadores.Text = "Total de Jogadores: 0";
                    lblMelhorPontuacao.Text = "Melhor Pontuação: 0";

                    if (dgvPlacar != null)
                    {
                        dgvPlacar.Columns.Add("Mensagem", "Informação");
                        dgvPlacar.Rows.Add("Erro ao carregar dados do banco de dados");
                        dgvPlacar.Columns[0].Width = dgvPlacar.Width - 20;
                    }
                    return;
                }

                if (ranking.Rows.Count == 0)
                {
                    lblTotalJogadores.Text = "Total de Jogadores: 0";
                    lblMelhorPontuacao.Text = "Melhor Pontuação: 0";

                    if (dgvPlacar != null)
                    {
                        dgvPlacar.Columns.Add("Mensagem", "Informação");
                        dgvPlacar.Rows.Add($"Nenhum registro encontrado para {(jogoSelecionado == "Todos os Jogos" ? "nenhum jogo" : jogoSelecionado)}");
                        dgvPlacar.Columns[0].Width = dgvPlacar.Width - 20;
                        dgvPlacar.Columns[0].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Italic);
                        dgvPlacar.Columns[0].DefaultCellStyle.ForeColor = Color.LightGray;
                    }
                    return;
                }

                if (dgvPlacar != null)
                {
                    dgvPlacar.AutoGenerateColumns = true;
                    dgvPlacar.DataSource = ranking;

                    if (dgvPlacar.Columns.Contains("Nome"))
                    {
                        dgvPlacar.Columns["Nome"].HeaderText = "Jogador";
                        dgvPlacar.Columns["Nome"].Width = 150;
                    }

                    if (dgvPlacar.Columns.Contains("Usuario"))
                    {
                        dgvPlacar.Columns["Usuario"].HeaderText = "Usuário";
                        dgvPlacar.Columns["Usuario"].Width = 120;
                    }

                    if (dgvPlacar.Columns.Contains("MelhorPontuacao"))
                    {
                        dgvPlacar.Columns["MelhorPontuacao"].HeaderText = "Melhor Pontuação";
                        dgvPlacar.Columns["MelhorPontuacao"].Width = 130;
                        dgvPlacar.Columns["MelhorPontuacao"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvPlacar.Columns["MelhorPontuacao"].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                        dgvPlacar.Columns["MelhorPontuacao"].DefaultCellStyle.ForeColor = Color.Gold;
                    }

                    if (dgvPlacar.Columns.Contains("TotalPartidas"))
                    {
                        dgvPlacar.Columns["TotalPartidas"].HeaderText = "Total de Partidas";
                        dgvPlacar.Columns["TotalPartidas"].Width = 120;
                        dgvPlacar.Columns["TotalPartidas"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }

                    if (dgvPlacar.Columns.Contains("Vitorias"))
                    {
                        dgvPlacar.Columns["Vitorias"].HeaderText = "Vitórias";
                        dgvPlacar.Columns["Vitorias"].Width = 80;
                        dgvPlacar.Columns["Vitorias"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }

                    dgvPlacar.AutoResizeRows();
                }

                // Atualizar estatísticas
                lblTotalJogadores.Text = $"Total de Jogadores: {ranking.Rows.Count}";

                if (ranking.Rows.Count > 0)
                {
                    if (ranking.Columns.Contains("MelhorPontuacao"))
                    {
                        try
                        {
                            object valorPontuacao = ranking.Rows[0]["MelhorPontuacao"];
                            if (valorPontuacao != DBNull.Value)
                            {
                                int melhorPontuacao = Convert.ToInt32(valorPontuacao);
                                lblMelhorPontuacao.Text = $"🏆 Melhor Pontuação: {melhorPontuacao}";
                            }
                            else
                            {
                                lblMelhorPontuacao.Text = "🏆 Melhor Pontuação: 0";
                            }
                        }
                        catch
                        {
                            lblMelhorPontuacao.Text = "🏆 Melhor Pontuação: 0";
                        }
                    }
                }
                else
                {
                    lblMelhorPontuacao.Text = "🏆 Melhor Pontuação: 0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar ranking: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (dgvPlacar != null)
                {
                    dgvPlacar.DataSource = null;
                    dgvPlacar.Rows.Clear();
                    dgvPlacar.Columns.Clear();
                    dgvPlacar.Columns.Add("Mensagem", "Erro");
                    dgvPlacar.Rows.Add($"Erro ao carregar dados: {ex.Message}");
                    dgvPlacar.Columns[0].Width = dgvPlacar.Width - 20;
                }

                lblTotalJogadores.Text = "Total de Jogadores: 0";
                lblMelhorPontuacao.Text = "Melhor Pontuação: 0";
            }
        }

        private void CmbJogo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarRanking();
        }

        private void BtnAtualizar_Click(object sender, EventArgs e)
        {
            CarregarRanking();
        }

        private void BtnRetornar_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            main.Show();
            this.Close();
        }

        private void dgvPlacar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}