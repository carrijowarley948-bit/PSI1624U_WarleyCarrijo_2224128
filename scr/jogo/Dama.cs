using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace jogo
{
    public partial class Dama : Form
    {
        private Button[,] botoes = new Button[8, 8];
        private int[,] tabuleiro = new int[8, 8];

        private int turno = 1;

        private int LinhaSelecionada = -1;
        private int ColunaSelecionada = -1;

        private bool selecionado = false;
        private bool jogoFinalizado = false;

        private List<Point> movimentosPossiveis = new List<Point>();
        private List<Point> capturasPossiveis = new List<Point>();

        private int pontuacaoJogador1 = 0;
        private int pontuacaoJogador2 = 0;

        public Dama()
        {
            InitializeComponent();

            InicializarTabuleiro();
            DesenharTabuleiro();
        }

        private void InicializarTabuleiro()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    tabuleiro[i, j] = 0;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i + j) % 2 != 0)
                    {
                        tabuleiro[i, j] = 2;
                    }
                }
            }

            for (int i = 5; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i + j) % 2 != 0)
                    {
                        tabuleiro[i, j] = 1;
                    }
                }
            }

            pontuacaoJogador1 = 0;
            pontuacaoJogador2 = 0;

            AtualizarPontuacao();
        }

        private void AtualizarPontuacao()
        {
            lblPontuacao.Text = $"Brancas: {pontuacaoJogador1} | Pretas: {pontuacaoJogador2}";
        }

        private void DesenharTabuleiro()
        {
            int tamanho = 60;
            int offsetX = 50;
            int offsetY = 50;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Button casas = new Button();

                    casas.Size = new Size(tamanho, tamanho);

                    casas.Location = new Point(
                        offsetX + (j * tamanho),
                        offsetY + (i * tamanho));

                    casas.FlatStyle = FlatStyle.Flat;
                    casas.FlatAppearance.BorderSize = 1;

                    if ((i + j) % 2 == 0)
                    {
                        casas.BackColor = Color.Beige;
                    }
                    else
                    {
                        casas.BackColor = Color.SaddleBrown;
                    }

                    casas.Tag = new Point(i, j);

                    casas.Click += Btn_Click;

                    botoes[i, j] = casas;

                    AtualizarBotao(i, j);

                    this.Controls.Add(casas);
                }
            }
        }

        private void AtualizarBotao(int linha, int coluna)
        {
            Button btn = botoes[linha, coluna];

            btn.Text = "";

            if (tabuleiro[linha, coluna] == 1)
            {
                btn.Text = "●";
                btn.ForeColor = Color.White;
                btn.Font = new Font("Arial", 32, FontStyle.Bold);
            }
            else if (tabuleiro[linha, coluna] == 2)
            {
                btn.Text = "●";
                btn.ForeColor = Color.Black;
                btn.Font = new Font("Arial", 32, FontStyle.Bold);
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            if (jogoFinalizado)
                return;

            Button btn = (Button)sender;

            Point pos = (Point)btn.Tag;

            int linha = pos.X;
            int coluna = pos.Y;

            if (selecionado)
            {
                if (LinhaSelecionada == linha && ColunaSelecionada == coluna)
                {
                    CancelarSelecao();
                    return;
                }

                Point destino = new Point(linha, coluna);

                if (movimentosPossiveis.Contains(destino))
                {
                    bool moveu = MoverPeca(LinhaSelecionada, ColunaSelecionada,linha, coluna);

                    if (moveu)
                    {
                        CancelarSelecao();

                        if (TemCapturasObrigatorias(turno))
                        {
                            lblStatus.Text = "Você tem capturas obrigatórias!";
                        }
                        else
                        {
                            TrocarTurno();
                        }

                        if (VerificarFimJogo())
                        {
                            jogoFinalizado = true;

                            MessageBox.Show("Fim de jogo!", "Damas", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            Recomecar();
                        }
                    }
                }
                else if (capturasPossiveis.Contains(destino))
                {
                    bool capturou = CapturarPeca(LinhaSelecionada, ColunaSelecionada, linha, coluna);

                    if (capturou)
                    {
                        CancelarSelecao();

                        if (TemMaisCapturas(linha, coluna))
                        {
                            selecionado = true;
                            LinhaSelecionada = linha;
                            ColunaSelecionada = coluna;
                            DestacarCasa(linha, coluna);
                            CalcularCapturas(linha, coluna);
                            MostrarMovimentos();
                            lblStatus.Text = "Esta peça pode capturar novamente!";
                        }
                        else if (TemCapturasObrigatorias(turno))
                        {
                            lblStatus.Text = "Você tem capturas obrigatórias!";
                        }
                        else
                        {
                            TrocarTurno();
                        }

                        if (VerificarFimJogo())
                        {
                            jogoFinalizado = true;

                            MessageBox.Show("Fim de jogo!", "Damas", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            Recomecar();
                        }
                    }
                }
                else
                {
                    lblStatus.Text = "Movimento inválido!";
                }
            }
            else
            {
                bool jogador1 = tabuleiro[linha, coluna] == 1;
                bool jogador2 = tabuleiro[linha, coluna] == 2;

                if ((turno == 1 && jogador1) ||
                    (turno == 2 && jogador2))
                {
                    if (TemCapturasObrigatorias(turno))
                    {
                        List<Point> capturas = ObterTodasCapturas(turno);
                        Point posicao = new Point(linha, coluna);

                        if (!capturas.Contains(posicao))
                        {
                            lblStatus.Text = "Você precisa fazer uma captura primeiro!";
                            return;
                        }
                    }

                    selecionado = true;

                    LinhaSelecionada = linha;
                    ColunaSelecionada = coluna;

                    DestacarCasa(linha, coluna);

                    CalcularMovimentos(linha, coluna);

                    MostrarMovimentos();

                    btnCancelar.Enabled = true;

                    lblStatus.Text =
                        $"Peça selecionada ({linha},{coluna})";
                }
            }
        }

        private bool TemCapturasObrigatorias(int jogador)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int peca = tabuleiro[i, j];
                    if ((jogador == 1 && peca == 1) ||
                        (jogador == 2 && peca == 2))
                    {
                        if (TemCapturaDisponivel(i, j))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private List<Point> ObterTodasCapturas(int jogador)
        {
            List<Point> capturas = new List<Point>();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int peca = tabuleiro[i, j];
                    if ((jogador == 1 && peca == 1) ||
                        (jogador == 2 && peca == 2))
                    {
                        if (TemCapturaDisponivel(i, j))
                        {
                            capturas.Add(new Point(i, j));
                        }
                    }
                }
            }
            return capturas;
        }

        private bool TemCapturaDisponivel(int linha, int coluna)
        {
            int peca = tabuleiro[linha, coluna];
            int direcao = (peca == 1) ? -1 : 1;

            int[] direcaoLinha = { direcao, direcao, -direcao, -direcao };
            int[] direcaoColuna = { -1, 1, -1, 1 };

            for (int i = 0; i < 4; i++)
            {
                int numLinha = linha + direcaoLinha[i];
                int numColuna = coluna + direcaoColuna[i];

                if (Dentro(numLinha, numColuna) && tabuleiro[numLinha, numColuna] != 0)
                {
                    int inimigo = tabuleiro[numLinha, numColuna];
                    if ((peca == 1 && (inimigo == 2)) ||
                        (peca == 2 && (inimigo == 1)))
                    {
                        int nnr = numLinha + direcaoLinha[i];
                        int nnc = numColuna + direcaoColuna[i];

                        if (Dentro(nnr, nnc) && tabuleiro[nnr, nnc] == 0)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool TemMaisCapturas(int linha, int coluna)
        {
            return TemCapturaDisponivel(linha, coluna);
        }

        private void CalcularMovimentos(int linha, int coluna)
        {
            movimentosPossiveis.Clear();
            capturasPossiveis.Clear();

            int peca = tabuleiro[linha, coluna];

            if (peca == 1 || peca == 2)
            {
                int direcao = (peca == 1) ? -1 : 1;

                for (int direcaoColuna = -1; direcaoColuna <= 1; direcaoColuna += 2)
                {
                    int numLinha = linha + direcao;
                    int numColuna = coluna + direcaoColuna;

                    if (Dentro(numLinha, numColuna) && tabuleiro[numLinha, numColuna] == 0)
                    {
                        movimentosPossiveis.Add(new Point(numLinha, numColuna));
                    }
                }

                CalcularCapturas(linha, coluna);
            }
        }

        private void CalcularCapturas(int linha, int coluna)
        {
            int peca = tabuleiro[linha, coluna];
            int direcao = (peca == 1) ? -1 : 1;

            for (int direcaoColuna = -1; direcaoColuna <= 1; direcaoColuna += 2)
            {
                int numLinha = linha + direcao;
                int numColuna = coluna + direcaoColuna;

                if (Dentro(numLinha, numColuna) && tabuleiro[numLinha, numColuna] != 0)
                {
                    int inimigo = tabuleiro[numLinha, numColuna];
                    if ((peca == 1 && (inimigo == 2)) ||
                        (peca == 2 && (inimigo == 1)))
                    {
                        int nnr = numLinha + direcao;
                        int nnc = numColuna + direcaoColuna;

                        if (Dentro(nnr, nnc) && tabuleiro[nnr, nnc] == 0)
                        {
                            capturasPossiveis.Add(new Point(nnr, nnc));
                        }
                    }
                }
            }

            if (TemCapturasObrigatorias(turno))
            {
                for (int direcaoColuna = -1; direcaoColuna <= 1; direcaoColuna += 2)
                {
                    int numLinha = linha - direcao;
                    int numColuna = coluna + direcaoColuna;

                    if (Dentro(numLinha, numColuna) && tabuleiro[numLinha, numColuna] != 0)
                    {
                        int inimigo = tabuleiro[numLinha, numColuna];
                        if ((peca == 1 && (inimigo == 2)) ||
                            (peca == 2 && (inimigo == 1)))
                        {
                            int nnr = numLinha - direcao;
                            int nnc = numColuna + direcaoColuna;

                            if (Dentro(nnr, nnc) && tabuleiro[nnr, nnc] == 0)
                            {
                                capturasPossiveis.Add(new Point(nnr, nnc));
                            }
                        }
                    }
                }
            }
        }

        private bool CapturarPeca(int Linha, int Coluna, int paraLinha, int paraColuna)
        {
            int peca = tabuleiro[Linha, Coluna];
            int direcaoRow = Math.Sign(paraLinha - Linha);
            int direcaoCol = Math.Sign(paraColuna - Coluna);

            int pecaCapturadaLinha = Linha + direcaoRow;
            int pecaCapturadaColuna = Coluna + direcaoCol;

            int inimigo = tabuleiro[pecaCapturadaLinha, pecaCapturadaColuna];

            if ((peca == 1 && (inimigo == 2)) ||
                (peca == 2 && (inimigo == 1)))
            {
                tabuleiro[pecaCapturadaLinha, pecaCapturadaColuna] = 0;
                AtualizarBotao(pecaCapturadaLinha, pecaCapturadaColuna);

                if (turno == 1)
                {
                    pontuacaoJogador1++;
                }
                else
                {
                    pontuacaoJogador2++;
                }
                AtualizarPontuacao();

                tabuleiro[paraLinha, paraColuna] = tabuleiro[Linha, Coluna];
                tabuleiro[Linha, Coluna] = 0;

                AtualizarBotao(Linha, Coluna);
                AtualizarBotao(paraLinha, paraColuna);

                lblStatus.Text = $"Peça capturada!";

                return true;
            }

            return false;
        }

        private bool Dentro(int linha, int coluna)
        {
            return linha >= 0 && linha < 8 && coluna >= 0 && coluna < 8;
        }

        private void MostrarMovimentos()
        {
            foreach (Point p in movimentosPossiveis)
            {
                botoes[p.X, p.Y].BackColor = Color.LightGreen;
            }

            foreach (Point p in capturasPossiveis)
            {
                botoes[p.X, p.Y].BackColor = Color.LightCoral;
            }
        }

        private bool MoverPeca(
            int Linha,
            int Coluna,
            int paraLInha,
            int paraColuna)
        {
            if (tabuleiro[paraLInha, paraColuna] != 0)
                return false;

            int peca = tabuleiro[Linha, Coluna];

            int deltaLinha = paraLInha - Linha;

            if (peca == 1 && deltaLinha > 0)
                return false;

            if (peca == 2 && deltaLinha < 0)
                return false;

            tabuleiro[paraLInha, paraColuna] = tabuleiro[Linha, Coluna];

            tabuleiro[Linha, Coluna] = 0;

            AtualizarBotao(Linha, Coluna);
            AtualizarBotao(paraLInha, paraColuna);

            return true;
        }

        private void TrocarTurno()
        {
            turno = (turno == 1) ? 2 : 1;

            if (turno == 1)
            {
                lblTurno.Text =
                    "Turno: Jogador 1 (Brancas)";
            }
            else
            {
                lblTurno.Text =
                    "Turno: Jogador 2 (Pretas)";
            }

            lblStatus.Text =
                "Clique em uma peça";
        }

        private void DestacarCasa(int linha, int coluna)
        {
            botoes[linha, coluna].BackColor = Color.Gold;
        }

        private void LimparDestaques()
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

        private void CancelarSelecao()
        {
            selecionado = false;

            LinhaSelecionada = -1;
            ColunaSelecionada = -1;

            movimentosPossiveis.Clear();
            capturasPossiveis.Clear();

            LimparDestaques();

            btnCancelar.Enabled = false;

            lblStatus.Text = "Seleção cancelada";
        }

        private bool VerificarFimJogo()
        {
            bool branco = false;
            bool preto = false;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (tabuleiro[i, j] == 1)
                    {
                        branco = true;
                    }

                    if (tabuleiro[i, j] == 2)
                    {
                        preto = true;
                    }
                }
            }

            if (!branco)
            {
                MessageBox.Show("Jogador 2 (Pretas) venceu!", "Fim de Jogo");
            }
            else if (!preto)
            {
                MessageBox.Show("Jogador 1 (Brancas) venceu!", "Fim de Jogo");
            }

            return !(branco && preto);
        }

        private void Recomecar()
        {
            foreach (Button btn in botoes)
            {
                if (btn != null)
                {
                    this.Controls.Remove(btn);
                }
            }

            botoes = new Button[8, 8];

            InicializarTabuleiro();

            DesenharTabuleiro();

            turno = 1;
            selecionado = false;
            jogoFinalizado = false;
            movimentosPossiveis.Clear();
            capturasPossiveis.Clear();

            lblTurno.Text =
                "Turno: Jogador 1 (Brancas)";

            lblStatus.Text =
                "Clique em uma peça";
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            Recomecar();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            CancelarSelecao();
        }

        private void BtnRetornar_Click(object sender, EventArgs e)
        {
            Main tela = new Main();

            tela.Show();

            this.Close();
        }

        private void Dama_Load(object sender, EventArgs e)
        {

        }
    }
}