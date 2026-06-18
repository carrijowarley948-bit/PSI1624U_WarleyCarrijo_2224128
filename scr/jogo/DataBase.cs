using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace jogo
{
    internal class DataBase
    {
        private static string caminhoSql = "Server=(localdb)\\MSSQLLocalDB;Database=FliperamaDB;Integrated Security=True;";

        private static SqlConnection GetConnection() => new SqlConnection(caminhoSql);

        private static DataTable ExecutarConsulta(string storedProcedure, Action<SqlCommand> parametros = null)
        {
            try
            {
                using (SqlConnection conectar = GetConnection())
                using (SqlCommand cmd = new SqlCommand(storedProcedure, conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    parametros?.Invoke(cmd);

                    conectar.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro");
                return null;
            }
        }

        private static int ExecutarComando(string storedProcedure, Action<SqlCommand> parametros = null)
        {
            try
            {
                using (SqlConnection conectar = GetConnection())
                using (SqlCommand cmd = new SqlCommand(storedProcedure, conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    parametros?.Invoke(cmd);

                    conectar.Open();
                    object resultado = cmd.ExecuteScalar();
                    return resultado != null ? Convert.ToInt32(resultado) : -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro");
                return -1;
            }
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
            catch
            {
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

                    string sql = @"
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
                        END
                        
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
                        END
                        
                        IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Usuario = 'admin')
                        BEGIN
                            INSERT INTO Usuarios (Nome, Usuario, Senha, IsAdmin) 
                            VALUES ('Administrador', 'admin', 'admin123', 1)
                        END";

                    using (SqlCommand cmd = new SqlCommand(sql, conectar))
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
            return ExecutarComando("sp_RegistrarUsuario", cmd =>
            {
                cmd.Parameters.AddWithValue("@Nome", nome);
                cmd.Parameters.AddWithValue("@Usuario", usuario);
                cmd.Parameters.AddWithValue("@Senha", senha);
            });
        }

        public static DataTable Login(string usuario, string senha)
        {
            return ExecutarConsulta("sp_Login", cmd =>
            {
                cmd.Parameters.AddWithValue("@Usuario", usuario);
                cmd.Parameters.AddWithValue("@Senha", senha);
            });
        }

        public static int RegistrarPontuacao(int usuarioId, string jogo, int pontuacao, bool venceu = false)
        {
            return ExecutarComando("sp_RegistrarPontuacao", cmd =>
            {
                cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
                cmd.Parameters.AddWithValue("@Jogo", jogo);
                cmd.Parameters.AddWithValue("@Pontuacao", pontuacao);
                cmd.Parameters.AddWithValue("@Venceu", venceu);
            });
        }

        public static DataTable ObterRankingPorJogo(string jogo)
        {
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

            try
            {
                using (SqlConnection conectar = GetConnection())
                using (SqlCommand cmd = new SqlCommand(sql, conectar))
                {
                    cmd.Parameters.AddWithValue("@Jogo", jogo);
                    conectar.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro");
                return null;
            }
        }

        public static DataTable ObterRankingGeralMelhorPontuacao()
        {
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

            try
            {
                using (SqlConnection conectar = GetConnection())
                using (SqlCommand cmd = new SqlCommand(sql, conectar))
                {
                    conectar.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro");
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

                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Pontuacoes; DELETE FROM Usuarios; DBCC CHECKIDENT ('Usuarios', RESEED, 0); DBCC CHECKIDENT ('Pontuacoes', RESEED, 0);", conectar))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Usuarios (Nome, Usuario, Senha, IsAdmin) VALUES ('Administrador', 'admin', 'admin123', 1)", conectar))
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