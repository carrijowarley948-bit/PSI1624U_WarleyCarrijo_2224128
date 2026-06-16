using System;
using System.Windows.Forms;

namespace jogo
{
    public partial class Registro : Form
    {
        public Registro()
        {
            InitializeComponent();
        }

        private void btnCriar_Click(object sender, EventArgs e)
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
                txtSenha.Clear();
                txtConfirm.Clear();
                txtSenha.Focus();
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
                MessageBox.Show($"Conta criada com sucesso, {nome}!\n\nUse seu usuário e senha para fazer login.",
                    "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                lblStatus.Text = "Usuário já existe! Escolha outro nome de usuário.";
                txtUser.Clear();
                txtUser.Focus();
            }
        }

        private void Registro_Load(object sender, EventArgs e)
        {

        }

        private void lblStatus_Click(object sender, EventArgs e)
        {

        }
    }
}