using System;
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
                MessageBox.Show("Não foi possível conectar ao banco de dados!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Login_Click(object sender, EventArgs e)
        {
            Login loginForm = new Login();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                Main main = new Main();
                main.Show();
                this.Hide();
            }
        }

        private void Registrar_Click(object sender, EventArgs e)
        {
            Registro registar = new Registro();
            registar.ShowDialog();
        }

        public static int GetUsuarioLogadoId()
        {
            return usuarioLogadoId;
        }

        public static string GetUsuarioLogadoNome()
        {
            return usuarioLogadoNome;
        }

        public static string GetUsuarioLogadoUsername()
        {
            return usuarioLogadoUsername;
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
            }
        }

        public static void Logout()
        {
            usuarioLogadoId = -1;
            usuarioLogadoNome = "";
            usuarioLogadoUsername = "";
        }

        internal static void SetUsuarioLogado(int id, string nome, string username)
        {
            usuarioLogadoId = id;
            usuarioLogadoNome = nome;
            usuarioLogadoUsername = username;
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

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}