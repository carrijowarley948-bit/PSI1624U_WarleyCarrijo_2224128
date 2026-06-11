using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace jogo
{
    internal class DataBase
    {
        private static string caminhoSql = "Server=(localdb)\\MSSQLLocalDB;Database=FliperamaDB;Integrated Security=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(caminhoSql);
        }

        public static bool TestarLigacao()
        {
            try
            {
                using (SqlConnection conectar = GetConnection())
                {
                    conectar.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro de conexão: {ex.Message}", "Erro");
                return false;
            }
        }

        public static void InicializarBancoDeDados()
        {
            try
            {
                using (SqlConnection conectar = GetConnection())
                {
                    conectar.Open();

                    string sqlUsuarios = @"
                        IF NOT EXISTS (SELECT * FROM sys.tables WHERE name='Usuarios')
                        BEGIN
                            CREATE TABLE Usuarios (
                                Id INT PRIMARY KEY IDENTITY(1,1),
                                Nome VARCHAR(100) NOT NULL,
                                Usuario VARCHAR(50) NOT NULL UNIQUE,
                                Senha VARCHAR(50) NOT NULL,
                                DataRegistro DATETIME DEFAULT GETDATE(),
                                IsAdmin BIT DEFAULT 0
                            )
                        END";

                    using (SqlCommand cmd = new SqlCommand(sqlUsuarios, conectar))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    string sqlPontuacoes = @"
                        IF NOT EXISTS (SELECT * FROM sys.tables WHERE name='Pontuacoes')
                        BEGIN
                            CREATE TABLE Pontuacoes (
                                Id INT PRIMARY KEY IDENTITY(1,1),
                                UsuarioId INT NOT NULL,
                                Jogo VARCHAR(50) NOT NULL,
                                Pontuacao INT NOT NULL,
                                DataHora DATETIME DEFAULT GETDATE(),
                                Venceu BIT DEFAULT 0,
                                DuracaoSegundos INT DEFAULT 0,
                                FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id)
                            )
                        END";

                    using (SqlCommand cmd = new SqlCommand(sqlPontuacoes, conectar))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    string sqlAdmin = @"
                        IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Usuario = 'admin')
                        BEGIN
                            INSERT INTO Usuarios (Nome, Usuario, Senha, IsAdmin) 
                            VALUES ('Administrador', 'admin', 'admin123', 1)
                        END";

                    using (SqlCommand cmd = new SqlCommand(sqlAdmin, conectar))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao inicializar banco: {ex.Message}", "Erro");
            }
        }

        public static int RegistrarUsuario(string nome, string usuario, string senha)
        {
            try
            {
                using (SqlConnection conectar = GetConnection())
                {
                    conectar.Open();

                    string verificarSql = "SELECT COUNT(*) FROM Usuarios WHERE Usuario = @Usuario";
                    using (SqlCommand verificarCmd = new SqlCommand(verificarSql, conectar))
                    {
                        verificarCmd.Parameters.AddWithValue("@Usuario", usuario);
                        int existe = (int)verificarCmd.ExecuteScalar();
                        if (existe > 0)
                        {
                            return -1;
                        }
                    }

                    string inserirSql = "INSERT INTO Usuarios (Nome, Usuario, Senha) VALUES (@Nome, @Usuario, @Senha); SELECT SCOPE_IDENTITY();";
                    using (SqlCommand cmd = new SqlCommand(inserirSql, conectar))
                    {
                        cmd.Parameters.AddWithValue("@Nome", nome);
                        cmd.Parameters.AddWithValue("@Usuario", usuario);
                        cmd.Parameters.AddWithValue("@Senha", senha);
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao registrar: {ex.Message}", "Erro");
                return -1;
            }
        }

        public static DataTable Login(string usuario, string senha)
        {
            try
            {
                using (SqlConnection conectar = GetConnection())
                {
                    conectar.Open();
                    string sql = "SELECT Id, Nome, Usuario, IsAdmin FROM Usuarios WHERE Usuario = @Usuario AND Senha = @Senha";
                    using (SqlCommand cmd = new SqlCommand(sql, conectar))
                    {
                        cmd.Parameters.AddWithValue("@Usuario", usuario);
                        cmd.Parameters.AddWithValue("@Senha", senha);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro no login: {ex.Message}", "Erro");
                return null;
            }
        }

        public static int RegistrarPontuacao(int usuarioId, string jogo, int pontuacao, bool venceu = false)
        {
            try
            {
                using (SqlConnection conectar = GetConnection())
                {
                    conectar.Open();
                    string sql = "INSERT INTO Pontuacoes (UsuarioId, Jogo, Pontuacao, Venceu) VALUES (@UsuarioId, @Jogo, @Pontuacao, @Venceu); SELECT SCOPE_IDENTITY();";
                    using (SqlCommand cmd = new SqlCommand(sql, conectar))
                    {
                        cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
                        cmd.Parameters.AddWithValue("@Jogo", jogo);
                        cmd.Parameters.AddWithValue("@Pontuacao", pontuacao);
                        cmd.Parameters.AddWithValue("@Venceu", venceu);
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao registrar pontuação: {ex.Message}", "Erro");
                return -1;
            }
        }

        public static DataTable ObterRankingPorJogo(string jogo)
        {
            try
            {
                using (SqlConnection conectar = GetConnection())
                {
                    conectar.Open();

                    string sql = @"
                        SELECT TOP 10 
                            u.Nome,
                            u.Usuario,
                            MAX(p.Pontuacao) AS MelhorPontuacao,
                            COUNT(p.Id) AS TotalPartidas,
                            SUM(CASE WHEN p.Venceu = 1 THEN 1 ELSE 0 END) AS Vitorias
                        FROM Pontuacoes p
                        INNER JOIN Usuarios u ON p.UsuarioId = u.Id
                        WHERE p.Jogo = @Jogo
                        GROUP BY u.Nome, u.Usuario
                        ORDER BY MAX(p.Pontuacao) DESC";

                    using (SqlCommand cmd = new SqlCommand(sql, conectar))
                    {
                        cmd.Parameters.AddWithValue("@Jogo", jogo);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao obter ranking: {ex.Message}", "Erro");
                return null;
            }
        }

        public static DataTable ObterRankingGeralMelhorPontuacao()
        {
            try
            {
                using (SqlConnection conectar = GetConnection())
                {
                    conectar.Open();

                    string sql = @"
                        SELECT TOP 10 
                            u.Nome,
                            u.Usuario,
                            COALESCE(MAX(p.Pontuacao), 0) AS MelhorPontuacao,
                            COUNT(p.Id) AS TotalPartidas,
                            SUM(CASE WHEN p.Venceu = 1 THEN 1 ELSE 0 END) AS Vitorias
                        FROM Usuarios u
                        LEFT JOIN Pontuacoes p ON u.Id = p.UsuarioId
                        GROUP BY u.Nome, u.Usuario
                        HAVING COALESCE(MAX(p.Pontuacao), 0) > 0
                        ORDER BY MAX(p.Pontuacao) DESC, SUM(CASE WHEN p.Venceu = 1 THEN 1 ELSE 0 END) DESC";

                    using (SqlCommand cmd = new SqlCommand(sql, conectar))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao obter ranking geral: {ex.Message}", "Erro");

                DataTable dt = new DataTable();
                dt.Columns.Add("Nome", typeof(string));
                dt.Columns.Add("Usuario", typeof(string));
                dt.Columns.Add("MelhorPontuacao", typeof(int));
                dt.Columns.Add("TotalPartidas", typeof(int));
                dt.Columns.Add("Vitorias", typeof(int));
                return dt;
            }
        }

        public static void LimparTodasAsContas()
        {
            try
            {
                using (SqlConnection conectar = GetConnection())
                {
                    conectar.Open();

                    string sqlPontuacoes = "DELETE FROM Pontuacoes";
                    using (SqlCommand cmd = new SqlCommand(sqlPontuacoes, conectar))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    string sqlUsuarios = "DELETE FROM Usuarios";
                    using (SqlCommand cmd = new SqlCommand(sqlUsuarios, conectar))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    string sqlResetUser = "DBCC CHECKIDENT ('Usuarios', RESEED, 0)";
                    using (SqlCommand cmd = new SqlCommand(sqlResetUser, conectar))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    string sqlResetPont = "DBCC CHECKIDENT ('Pontuacoes', RESEED, 0)";
                    using (SqlCommand cmd = new SqlCommand(sqlResetPont, conectar))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    string sqlAdmin = "INSERT INTO Usuarios (Nome, Usuario, Senha, IsAdmin) VALUES ('Administrador', 'admin', 'admin123', 1)";
                    using (SqlCommand cmd = new SqlCommand(sqlAdmin, conectar))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao limpar dados: {ex.Message}", "Erro");
            }
        }
    }
}