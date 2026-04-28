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
    public partial class Dama : Form
    {
        private Button[,] botoes = new Button[8, 8];
        private int[,] tabuleiro = new int[8, 8];
        private int turno = 1; // 1 = jogador1 (brancas), 2 = jogador2 (pretas)
        private int selectedRow = -1;
        private int selectedCol = -1;
        private bool selecionado = false;
        private Label lblTurno;
        private Label lblStatus;
        private List<Point> movimentosPossiveis = new List<Point>();
        private Button btnCancelar;

        public Dama()
        {
            InitializeComponent();
            InicializarComponentesAdicionais();
            InicializarTabuleiro();
            DesenharTabuleiro();
        }

        private void InicializarComponentesAdicionais()
        {
            this.Size = new Size(600, 700);
            this.Text = "Jogo de Damas";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.LightGray;

            lblTurno = new Label();
            lblTurno.Size = new Size(300, 30);
            lblTurno.Location = new Point(150, 10);
            lblTurno.Font = new Font("Arial", 12, FontStyle.Bold);
            lblTurno.Text = "Turno: Jogador 1 (Brancas)";

            lblStatus = new Label();
            lblStatus.Size = new Size(400, 30);
            lblStatus.Location = new Point(100, 560);
            lblStatus.Font = new Font("Arial", 10, FontStyle.Regular);
            lblStatus.Text = "Clique em uma peça para selecionar";

            Button btnReset = new Button();
            btnReset.Size = new Size(100, 30);
            btnReset.Location = new Point(150, 600);
            btnReset.Text = "Novo Jogo";
            btnReset.Click += BtnReset_Click;

            btnCancelar = new Button();
            btnCancelar.Size = new Size(100, 30);
            btnCancelar.Location = new Point(350, 600);
            btnCancelar.Text = "Cancelar";
            btnCancelar.BackColor = Color.LightCoral;
            btnCancelar.Click += BtnCancelar_Click;
            btnCancelar.Enabled = false;

            this.Controls.Add(lblTurno);
            this.Controls.Add(lblStatus);
            this.Controls.Add(btnReset);
            this.Controls.Add(btnCancelar);
        }

        private void InicializarTabuleiro()
        {
            // Inicializar tabuleiro vazio
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    tabuleiro[i, j] = 0;
                }
            }

            // Posicionar peças pretas (jogador 2)
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i + j) % 2 != 0)
                    {
                        tabuleiro[i, j] = 2; // 2 = peça preta
                    }
                }
            }

            // Posicionar peças brancas (jogador 1)
            for (int i = 5; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i + j) % 2 != 0)
                    {
                        tabuleiro[i, j] = 1; // 1 = peça branca
                    }
                }
            }
        }

        private void DesenharTabuleiro()
        {
            int tamanhoCelula = 60;
            int offsetX = 50;
            int offsetY = 50;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Button btn = new Button();
                    btn.Size = new Size(tamanhoCelula, tamanhoCelula);
                    btn.Location = new Point(offsetX + j * tamanhoCelula, offsetY + i * tamanhoCelula);

                    // Cor da casa
                    if ((i + j) % 2 == 0)
                    {
                        btn.BackColor = Color.Beige;
                    }
                    else
                    {
                        btn.BackColor = Color.SaddleBrown;
                    }

                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 1;
                    btn.FlatAppearance.BorderColor = Color.Black;

                    // Adicionar peça se houver
                    if (tabuleiro[i, j] == 1)
                    {
                        btn.Text = "●";
                        btn.ForeColor = Color.White;
                        btn.Font = new Font("Arial", 32, FontStyle.Bold);
                        btn.TextAlign = ContentAlignment.MiddleCenter;
                    }
                    else if (tabuleiro[i, j] == 2)
                    {
                        btn.Text = "●";
                        btn.ForeColor = Color.Black;
                        btn.Font = new Font("Arial", 32, FontStyle.Bold);
                        btn.TextAlign = ContentAlignment.MiddleCenter;
                    }
                    else if (tabuleiro[i, j] == 3) // Dama branca
                    {
                        btn.Text = "♕";
                        btn.ForeColor = Color.White;
                        btn.Font = new Font("Arial", 32, FontStyle.Bold);
                        btn.TextAlign = ContentAlignment.MiddleCenter;
                    }
                    else if (tabuleiro[i, j] == 4) // Dama preta
                    {
                        btn.Text = "♕";
                        btn.ForeColor = Color.Black;
                        btn.Font = new Font("Arial", 32, FontStyle.Bold);
                        btn.TextAlign = ContentAlignment.MiddleCenter;
                    }
                    else
                    {
                        btn.Text = "";
                    }

                    btn.Tag = new Point(i, j);
                    btn.Click += Btn_Click;

                    botoes[i, j] = btn;
                    this.Controls.Add(btn);
                }
            }
        }

        private void CancelarSelecao()
        {
            selecionado = false;
            selectedRow = -1;
            selectedCol = -1;
            LimparMovimentos();
            LimparDestaque();
            btnCancelar.Enabled = false;
            lblStatus.Text = "Seleção cancelada. Clique em uma peça para selecionar";
        }

        private void CalcularMovimentosPossiveis(int row, int col)
        {
            movimentosPossiveis.Clear();
            int peca = tabuleiro[row, col];
            bool isDama = (peca == 3 || peca == 4);
            bool temCaptura = false;

            // Primeiro verificar movimentos de captura
            for (int dRow = -2; dRow <= 2; dRow += 4)
            {
                for (int dCol = -2; dCol <= 2; dCol += 4)
                {
                    int newRow = row + dRow;
                    int newCol = col + dCol;
                    int capturedRow = row + dRow / 2;
                    int capturedCol = col + dCol / 2;

                    if (newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8)
                    {
                        if (tabuleiro[newRow, newCol] == 0)
                        {
                            if (isDama)
                            {
                                if ((peca == 3 && (tabuleiro[capturedRow, capturedCol] == 2 || tabuleiro[capturedRow, capturedCol] == 4)) ||
                                    (peca == 4 && (tabuleiro[capturedRow, capturedCol] == 1 || tabuleiro[capturedRow, capturedCol] == 3)))
                                {
                                    movimentosPossiveis.Add(new Point(newRow, newCol));
                                    temCaptura = true;
                                }
                            }
                            else
                            {
                                if ((peca == 1 && tabuleiro[capturedRow, capturedCol] == 2) ||
                                    (peca == 2 && tabuleiro[capturedRow, capturedCol] == 1))
                                {
                                    movimentosPossiveis.Add(new Point(newRow, newCol));
                                    temCaptura = true;
                                }
                            }
                        }
                    }
                }
            }

            // Se não houver capturas, verificar movimentos normais
            if (!temCaptura)
            {
                if (isDama)
                {
                    // Movimentos da dama em todas as direções
                    for (int dRow = -1; dRow <= 1; dRow += 2)
                    {
                        for (int dCol = -1; dCol <= 1; dCol += 2)
                        {
                            int newRow = row + dRow;
                            int newCol = col + dCol;

                            if (newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8)
                            {
                                if (tabuleiro[newRow, newCol] == 0)
                                {
                                    movimentosPossiveis.Add(new Point(newRow, newCol));
                                }
                            }
                        }
                    }
                }
                else
                {
                    // Movimentos normais
                    int direcao = (peca == 1) ? -1 : 1;

                    for (int dCol = -1; dCol <= 1; dCol += 2)
                    {
                        int newRow = row + direcao;
                        int newCol = col + dCol;

                        if (newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8)
                        {
                            if (tabuleiro[newRow, newCol] == 0)
                            {
                                movimentosPossiveis.Add(new Point(newRow, newCol));
                            }
                        }
                    }
                }
            }
        }

        private void MostrarMovimentosPossiveis()
        {
            foreach (Point pos in movimentosPossiveis)
            {
                botoes[pos.X, pos.Y].BackColor = Color.LightYellow;
            }
        }

        private void LimparMovimentos()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        if (botoes[i, j].BackColor != Color.Gold)
                            botoes[i, j].BackColor = Color.Beige;
                    }
                    else
                    {
                        if (botoes[i, j].BackColor != Color.Gold)
                            botoes[i, j].BackColor = Color.SaddleBrown;
                    }
                }
            }
            movimentosPossiveis.Clear();
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Point pos = (Point)btn.Tag;
            int row = pos.X;
            int col = pos.Y;

            // Se já tem uma peça selecionada
            if (selecionado)
            {
                // Verificar se clicou na mesma peça (cancelar seleção)
                if (selectedRow == row && selectedCol == col)
                {
                    CancelarSelecao();
                    return;
                }

                // Verificar se clicou em outra peça do mesmo jogador (trocar seleção)
                if (tabuleiro[row, col] == turno || (turno == 1 && tabuleiro[row, col] == 3) ||
                    (turno == 2 && tabuleiro[row, col] == 4))
                {
                    // Trocar para a nova peça
                    CancelarSelecao(); // Limpa a seleção anterior

                    // Selecionar a nova peça
                    selecionado = true;
                    selectedRow = row;
                    selectedCol = col;
                    DestacarCelula(row, col);

                    // Calcular e mostrar movimentos possíveis
                    CalcularMovimentosPossiveis(row, col);
                    MostrarMovimentosPossiveis();

                    btnCancelar.Enabled = true;
                    lblStatus.Text = $"Peça selecionada na posição ({row}, {col})";
                    return;
                }

                // Tentar mover a peça selecionada
                Point destino = new Point(row, col);
                if (movimentosPossiveis.Contains(destino))
                {
                    if (MoverPeca(selectedRow, selectedCol, row, col))
                    {
                        CancelarSelecao();
                        Turno();

                        // Verificar fim de jogo
                        if (VerificarFimDeJogo())
                        {
                            string vencedor = turno == 1 ? "Jogador 2 (Pretas)" : "Jogador 1 (Brancas)";
                            MessageBox.Show($"Fim de jogo! {vencedor} venceu!", "Fim de Jogo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetGame();
                        }
                    }
                    else
                    {
                        lblStatus.Text = "Movimento inválido!";
                    }
                }
                else
                {
                    lblStatus.Text = "Movimento inválido! Clique em uma casa amarela para mover ou em outra peça sua para trocar";
                }
            }
            else
            {
                // Nenhuma peça selecionada - selecionar peça
                if (tabuleiro[row, col] == turno || (turno == 1 && tabuleiro[row, col] == 3) ||
                    (turno == 2 && tabuleiro[row, col] == 4))
                {
                    selecionado = true;
                    selectedRow = row;
                    selectedCol = col;
                    DestacarCelula(row, col);

                    // Calcular e mostrar movimentos possíveis
                    CalcularMovimentosPossiveis(row, col);
                    MostrarMovimentosPossiveis();

                    btnCancelar.Enabled = true;
                    lblStatus.Text = $"Peça selecionada na posição ({row}, {col})";
                }
                else
                {
                    lblStatus.Text = "Esta não é sua peça!";
                }
            }
        }

        private bool MoverPeca(int fromRow, int fromCol, int toRow, int toCol)
        {
            // Verificar se destino é uma casa escura válida
            if ((toRow + toCol) % 2 == 0)
            {
                return false;
            }

            // Verificar se destino está vazio
            if (tabuleiro[toRow, toCol] != 0)
            {
                return false;
            }

            int peca = tabuleiro[fromRow, fromCol];
            int deltaRow = toRow - fromRow;
            int deltaCol = toCol - fromCol;
            int absDeltaRow = Math.Abs(deltaRow);
            int absDeltaCol = Math.Abs(deltaCol);

            // Movimento normal (1 casa)
            if (absDeltaRow == 1 && absDeltaCol == 1)
            {
                // Verificar direção para peças normais
                if (peca == 1 && deltaRow > 0) // Branca só pode andar para cima
                {
                    return false;
                }
                if (peca == 2 && deltaRow < 0) // Preta só pode andar para baixo
                {
                    return false;
                }

                tabuleiro[toRow, toCol] = peca;
                tabuleiro[fromRow, fromCol] = 0;
                AtualizarBotao(fromRow, fromCol);
                AtualizarBotao(toRow, toCol);

                // Verificar promoção a dama
                VerificarPromocao(toRow, toCol);
                return true;
            }

            // Movimento de captura (2 casas)
            if (absDeltaRow == 2 && absDeltaCol == 2)
            {
                int capturedRow = (fromRow + toRow) / 2;
                int capturedCol = (fromCol + toCol) / 2;
                int pecaCapturada = tabuleiro[capturedRow, capturedCol];

                // Verificar se tem peça inimiga para capturar
                if ((peca == 1 || peca == 3) && (pecaCapturada == 2 || pecaCapturada == 4))
                {
                    // Capturar peça
                    tabuleiro[toRow, toCol] = peca;
                    tabuleiro[fromRow, fromCol] = 0;
                    tabuleiro[capturedRow, capturedCol] = 0;
                    AtualizarBotao(fromRow, fromCol);
                    AtualizarBotao(capturedRow, capturedCol);
                    AtualizarBotao(toRow, toCol);

                    // Verificar promoção a dama
                    VerificarPromocao(toRow, toCol);

                    // Verificar se pode capturar novamente
                    if (PodeCapturarNovamente(toRow, toCol))
                    {
                        lblStatus.Text = "Você pode capturar novamente!";
                        selecionado = true;
                        selectedRow = toRow;
                        selectedCol = toCol;
                        DestacarCelula(toRow, toCol);

                        // Recalcular movimentos para a nova posição
                        CalcularMovimentosPossiveis(toRow, toCol);
                        MostrarMovimentosPossiveis();

                        btnCancelar.Enabled = true;
                        return false; // Não muda o turno
                    }
                    return true;
                }
                else if ((peca == 2 || peca == 4) && (pecaCapturada == 1 || pecaCapturada == 3))
                {
                    // Capturar peça
                    tabuleiro[toRow, toCol] = peca;
                    tabuleiro[fromRow, fromCol] = 0;
                    tabuleiro[capturedRow, capturedCol] = 0;
                    AtualizarBotao(fromRow, fromCol);
                    AtualizarBotao(capturedRow, capturedCol);
                    AtualizarBotao(toRow, toCol);

                    // Verificar promoção a dama
                    VerificarPromocao(toRow, toCol);

                    // Verificar se pode capturar novamente
                    if (PodeCapturarNovamente(toRow, toCol))
                    {
                        lblStatus.Text = "Você pode capturar novamente!";
                        selecionado = true;
                        selectedRow = toRow;
                        selectedCol = toCol;
                        DestacarCelula(toRow, toCol);

                        // Recalcular movimentos para a nova posição
                        CalcularMovimentosPossiveis(toRow, toCol);
                        MostrarMovimentosPossiveis();

                        btnCancelar.Enabled = true;
                        return false; // Não muda o turno
                    }
                    return true;
                }
            }

            return false;
        }

        private bool PodeCapturarNovamente(int row, int col)
        {
            int peca = tabuleiro[row, col];
            bool isDama = (peca == 3 || peca == 4);

            for (int dRow = -2; dRow <= 2; dRow += 4)
            {
                for (int dCol = -2; dCol <= 2; dCol += 4)
                {
                    int newRow = row + dRow;
                    int newCol = col + dCol;
                    int capturedRow = row + dRow / 2;
                    int capturedCol = col + dCol / 2;

                    if (newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8)
                    {
                        if (tabuleiro[newRow, newCol] == 0)
                        {
                            if (isDama)
                            {
                                if ((peca == 3 && (tabuleiro[capturedRow, capturedCol] == 2 || tabuleiro[capturedRow, capturedCol] == 4)) ||
                                    (peca == 4 && (tabuleiro[capturedRow, capturedCol] == 1 || tabuleiro[capturedRow, capturedCol] == 3)))
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                if ((peca == 1 && tabuleiro[capturedRow, capturedCol] == 2) ||
                                    (peca == 2 && tabuleiro[capturedRow, capturedCol] == 1))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        private void VerificarPromocao(int row, int col)
        {
            if (tabuleiro[row, col] == 1 && row == 0)
            {
                tabuleiro[row, col] = 3; // Dama branca
                AtualizarBotao(row, col);
                lblStatus.Text = "Peça promovida a Dama!";
            }
            else if (tabuleiro[row, col] == 2 && row == 7)
            {
                tabuleiro[row, col] = 4; // Dama preta
                AtualizarBotao(row, col);
                lblStatus.Text = "Peça promovida a Dama!";
            }
        }

        private void AtualizarBotao(int row, int col)
        {
            Button btn = botoes[row, col];

            if (tabuleiro[row, col] == 1)
            {
                btn.Text = "●";
                btn.ForeColor = Color.White;
                btn.Font = new Font("Arial", 32, FontStyle.Bold);
            }
            else if (tabuleiro[row, col] == 2)
            {
                btn.Text = "●";
                btn.ForeColor = Color.Black;
                btn.Font = new Font("Arial", 32, FontStyle.Bold);
            }
            else if (tabuleiro[row, col] == 3)
            {
                btn.Text = "♕";
                btn.ForeColor = Color.White;
                btn.Font = new Font("Arial", 32, FontStyle.Bold);
            }
            else if (tabuleiro[row, col] == 4)
            {
                btn.Text = "♕";
                btn.ForeColor = Color.Black;
                btn.Font = new Font("Arial", 32, FontStyle.Bold);
            }
            else
            {
                btn.Text = "";
            }
        }

        private void DestacarCelula(int row, int col)
        {
            botoes[row, col].BackColor = Color.Gold;
        }

        private void LimparDestaque()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        botoes[i, j].BackColor = Color.Beige;
                    }
                    else
                    {
                        botoes[i, j].BackColor = Color.SaddleBrown;
                    }
                }
            }
        }

        private void Turno()
        {
            turno = turno == 1 ? 2 : 1;
            if (turno == 1)
            {
                lblTurno.Text = "Turno: Jogador 1 (Brancas)";
                lblStatus.Text = "Clique em uma peça para selecionar";
            }
            else
            {
                lblTurno.Text = "Turno: Jogador 2 (Pretas)";
                lblStatus.Text = "Clique em uma peça para selecionar";
            }
        }

        private bool VerificarFimDeJogo()
        {
            bool temPecasBrancas = false;
            bool temPecasPretas = false;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (tabuleiro[i, j] == 1 || tabuleiro[i, j] == 3)
                        temPecasBrancas = true;
                    if (tabuleiro[i, j] == 2 || tabuleiro[i, j] == 4)
                        temPecasPretas = true;
                }
            }

            return !temPecasBrancas || !temPecasPretas;
        }

        private void ResetGame()
        {
            foreach (Button btn in botoes)
            {
                this.Controls.Remove(btn);
            }

            InicializarTabuleiro();
            DesenharTabuleiro();
            CancelarSelecao();
            turno = 1;
            lblTurno.Text = "Turno: Jogador 1 (Brancas)";
            lblStatus.Text = "Clique em uma peça para selecionar";
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            ResetGame();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            CancelarSelecao();
        }

        private void Dama_Load(object sender, EventArgs e)
        {
            
        }
    }
}