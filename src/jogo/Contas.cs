using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace jogo
{
    public partial class Contas : Form
    {
        private static int usuarioLogadoId = -1;
        private static string usuarioLogadoNome = "";
        private static string usuarioLogadoUsername = "";

        public Contas()
        {
            InitializeComponent();

            DataBase.InicializarBancoDeDados();

            if (!DataBase.TestarLigacao())
            {
                MessageBox.Show("Não foi possível conectar ao banco de dados!", "Erro",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Login_Click(object sender, EventArgs e)
        {
            Form loginForm = new Form();
            loginForm.Text = "Login";
            loginForm.Size = new Size(350, 280);
            loginForm.StartPosition = FormStartPosition.CenterParent;
            loginForm.BackColor = Color.FromArgb(30, 30, 40);
            loginForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            loginForm.MaximizeBox = false;

            Label lblUser = new Label();
            lblUser.Text = "Usuário:";
            lblUser.Location = new Point(30, 50);
            lblUser.Size = new Size(80, 25);
            lblUser.ForeColor = Color.White;

            TextBox txtUser = new TextBox();
            txtUser.Location = new Point(120, 50);
            txtUser.Size = new Size(180, 23);

            Label lblSenha = new Label();
            lblSenha.Text = "Senha:";
            lblSenha.Location = new Point(30, 90);
            lblSenha.Size = new Size(80, 25);
            lblSenha.ForeColor = Color.White;

            TextBox txtSenha = new TextBox();
            txtSenha.Location = new Point(120, 90);
            txtSenha.Size = new Size(180, 23);
            txtSenha.PasswordChar = '*';

            Button btnEntrar = new Button();
            btnEntrar.Text = "Entrar";
            btnEntrar.Location = new Point(110, 140);
            btnEntrar.Size = new Size(100, 35);
            btnEntrar.BackColor = Color.LightGreen;

            Label lblStatus = new Label();
            lblStatus.Text = "";
            lblStatus.Location = new Point(30, 190);
            lblStatus.Size = new Size(280, 40);
            lblStatus.ForeColor = Color.Yellow;

            loginForm.Controls.Add(lblUser);
            loginForm.Controls.Add(txtUser);
            loginForm.Controls.Add(lblSenha);
            loginForm.Controls.Add(txtSenha);
            loginForm.Controls.Add(btnEntrar);
            loginForm.Controls.Add(lblStatus);

            btnEntrar.Click += (s, ev) =>
            {
                string user = txtUser.Text.Trim();
                string senha = txtSenha.Text;

                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(senha))
                {
                    lblStatus.Text = "Preencha usuário e senha!";
                    return;
                }

                DataTable resultado = DataBase.Login(user, senha);

                if (resultado != null && resultado.Rows.Count > 0)
                {
                    usuarioLogadoId = Convert.ToInt32(resultado.Rows[0]["Id"]);
                    usuarioLogadoNome = resultado.Rows[0]["Nome"].ToString();
                    usuarioLogadoUsername = resultado.Rows[0]["Usuario"].ToString();

                    MessageBox.Show($"Bem-vindo, {usuarioLogadoNome}!", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loginForm.Close();

                    Main main = new Main();
                    main.Show();
                    this.Hide();
                }
                else
                {
                    lblStatus.Text = "Usuário ou senha incorretos!";
                    txtSenha.Clear();
                }
            };

            loginForm.ShowDialog();
        }

        private void Registrar_Click(object sender, EventArgs e)
        {
            Form formRegistro = new Form();
            formRegistro.Text = "Criar Conta";
            formRegistro.Size = new Size(400, 380);
            formRegistro.StartPosition = FormStartPosition.CenterParent;
            formRegistro.BackColor = Color.FromArgb(30, 30, 40);
            formRegistro.FormBorderStyle = FormBorderStyle.FixedDialog;
            formRegistro.MaximizeBox = false;

            int y = 30;

            Label lblNome = new Label();
            lblNome.Text = "Nome:";
            lblNome.Location = new Point(30, y);
            lblNome.Size = new Size(100, 25);
            lblNome.ForeColor = Color.White;

            TextBox txtNome = new TextBox();
            txtNome.Location = new Point(150, y);
            txtNome.Size = new Size(200, 23);

            y += 45;

            Label lblUser = new Label();
            lblUser.Text = "Usuário:";
            lblUser.Location = new Point(30, y);
            lblUser.Size = new Size(100, 25);
            lblUser.ForeColor = Color.White;

            TextBox txtUser = new TextBox();
            txtUser.Location = new Point(150, y);
            txtUser.Size = new Size(200, 23);

            y += 45;

            Label lblSenha = new Label();
            lblSenha.Text = "Senha:";
            lblSenha.Location = new Point(30, y);
            lblSenha.Size = new Size(100, 25);
            lblSenha.ForeColor = Color.White;

            TextBox txtSenha = new TextBox();
            txtSenha.Location = new Point(150, y);
            txtSenha.Size = new Size(200, 23);
            txtSenha.PasswordChar = '*';

            y += 45;

            Label lblConfirm = new Label();
            lblConfirm.Text = "Confirmar:";
            lblConfirm.Location = new Point(30, y);
            lblConfirm.Size = new Size(100, 25);
            lblConfirm.ForeColor = Color.White;

            TextBox txtConfirm = new TextBox();
            txtConfirm.Location = new Point(150, y);
            txtConfirm.Size = new Size(200, 23);
            txtConfirm.PasswordChar = '*';

            y += 55;

            Button btnCriar = new Button();
            btnCriar.Text = "Criar Conta";
            btnCriar.Location = new Point(120, y);
            btnCriar.Size = new Size(140, 40);
            btnCriar.BackColor = Color.LightGreen;

            y += 55;

            Label lblStatus = new Label();
            lblStatus.Text = "";
            lblStatus.Location = new Point(30, y);
            lblStatus.Size = new Size(330, 40);
            lblStatus.ForeColor = Color.Yellow;

            formRegistro.Controls.Add(lblNome);
            formRegistro.Controls.Add(txtNome);
            formRegistro.Controls.Add(lblUser);
            formRegistro.Controls.Add(txtUser);
            formRegistro.Controls.Add(lblSenha);
            formRegistro.Controls.Add(txtSenha);
            formRegistro.Controls.Add(lblConfirm);
            formRegistro.Controls.Add(txtConfirm);
            formRegistro.Controls.Add(btnCriar);
            formRegistro.Controls.Add(lblStatus);

            btnCriar.Click += (s, ev) =>
            {
                string nome = txtNome.Text.Trim();
                string user = txtUser.Text.Trim();
                string senha = txtSenha.Text;
                string confirm = txtConfirm.Text;

                if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(user) || string.IsNullOrEmpty(senha))
                {
                    lblStatus.Text = "Preencha todos os campos!";
                    return;
                }

                if (senha != confirm)
                {
                    lblStatus.Text = "Senhas não coincidem!";
                    return;
                }

                if (senha.Length < 4)
                {
                    lblStatus.Text = "Senha deve ter no mínimo 4 caracteres!";
                    return;
                }

                int resultado = DataBase.RegistrarUsuario(nome, user, senha);

                if (resultado > 0)
                {
                    MessageBox.Show($"Conta criada com sucesso, {nome}!\n\nUse seu usuário e senha para fazer login.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    formRegistro.Close();
                }
                else
                {
                    lblStatus.Text = "Usuário já existe! Escolha outro nome de usuário.";
                }
            };

            formRegistro.ShowDialog();
        }

        public static int GetUsuarioLogadoId()
        {
            return usuarioLogadoId;
        }

        public static string GetUsuarioLogadoNome()
        {
            return usuarioLogadoNome;
        }

        public static bool UsuarioLogado()
        {
            return usuarioLogadoId > 0;
        }

        public static void RegistrarPontuacao(string jogo, int pontuacao, bool venceu = false)
        {
            if (usuarioLogadoId > 0)
            {
                int resultado = DataBase.RegistrarPontuacao(usuarioLogadoId, jogo, pontuacao, venceu);
                if (resultado > 0)
                {
                   
                }
            }
        }

        public static void Logout()
        {
            usuarioLogadoId = -1;
            usuarioLogadoNome = "";
            usuarioLogadoUsername = "";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            main.Show();
            this.Close();
        }

        private void Contas_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
    }
}