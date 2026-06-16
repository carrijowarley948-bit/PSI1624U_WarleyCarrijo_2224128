using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace jogo
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            string user = txtUsuario.Text.Trim();
            string senha = txtSenha.Text;

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(senha))
            {
                lblStatus.Text = "Preencha usuário e senha!";
                return;
            }

            DataTable resultado = DataBase.Login(user, senha);

            if (resultado != null && resultado.Rows.Count > 0)
            {
                typeof(Contas).GetMethod("SetUsuarioLogado",
                    System.Reflection.BindingFlags.NonPublic |
                    System.Reflection.BindingFlags.Static)?
                    .Invoke(null, new object[] {
                        Convert.ToInt32(resultado.Rows[0]["Id"]),
                        resultado.Rows[0]["Nome"].ToString(),
                        resultado.Rows[0]["Usuario"].ToString()
                    });

                MessageBox.Show($"Bem-vindo, {resultado.Rows[0]["Nome"]}!", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                lblStatus.Text = "Usuário ou senha incorretos!";
                txtSenha.Clear();
                txtSenha.Focus();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void lblStatus_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}