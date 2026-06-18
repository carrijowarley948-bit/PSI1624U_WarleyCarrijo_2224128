
USE FliperamaDB;
GO

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'FliperamaDB')
BEGIN
    CREATE DATABASE FliperamaDB;
END
GO

USE FliperamaDB;
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Usuarios' and xtype='U')
BEGIN
    CREATE TABLE Usuarios (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Nome VARCHAR(100) NOT NULL,
        Usuario VARCHAR(50) NOT NULL UNIQUE,
        Senha VARCHAR(50) NOT NULL,
        DataRegistro DATETIME DEFAULT GETDATE(),
        IsAdmin BIT DEFAULT 0
    );
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Pontuacoes' and xtype='U')
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
    );
END
GO

CREATE OR ALTER PROCEDURE sp_RegistrarUsuario
    @Nome VARCHAR(100),
    @Usuario VARCHAR(50),
    @Senha VARCHAR(50)
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Usuario = @Usuario)
    BEGIN
        INSERT INTO Usuarios (Nome, Usuario, Senha, DataRegistro)
        VALUES (@Nome, @Usuario, @Senha, GETDATE());
        
        SELECT SCOPE_IDENTITY() AS Id;
    END
    ELSE
    BEGIN
        SELECT -1 AS Id;
    END
END;
GO

CREATE OR ALTER PROCEDURE sp_Login
    @Usuario VARCHAR(50),
    @Senha VARCHAR(50)
AS
BEGIN
    SELECT Id, Nome, Usuario, IsAdmin 
    FROM Usuarios 
    WHERE Usuario = @Usuario AND Senha = @Senha;
END;
GO

CREATE OR ALTER PROCEDURE sp_RegistrarPontuacao
    @UsuarioId INT,
    @Jogo VARCHAR(50),
    @Pontuacao INT,
    @Venceu BIT = 0,
    @DuracaoSegundos INT = 0
AS
BEGIN
    INSERT INTO Pontuacoes (UsuarioId, Jogo, Pontuacao, DataHora, Venceu, DuracaoSegundos)
    VALUES (@UsuarioId, @Jogo, @Pontuacao, GETDATE(), @Venceu, @DuracaoSegundos);
    
    SELECT SCOPE_IDENTITY() AS Id;
END;
GO

CREATE OR ALTER VIEW vwPontuacoes AS
SELECT 
    p.Id,
    u.Nome,
    u.Usuario,
    p.Jogo,
    p.Pontuacao,
    p.DataHora,
    CASE WHEN p.Venceu = 1 THEN 'Sim' ELSE 'N�o' END AS Venceu,
    p.DuracaoSegundos AS Duracao
FROM Pontuacoes p
INNER JOIN Usuarios u ON p.UsuarioId = u.Id;
GO

CREATE OR ALTER VIEW vwRankingPorJogo AS
SELECT 
    ROW_NUMBER() OVER (PARTITION BY Jogo ORDER BY MAX(p.Pontuacao) DESC) AS Posicao,
    u.Nome,
    u.Usuario,
    p.Jogo,
    MAX(p.Pontuacao) AS MelhorPontuacao,
    SUM(p.Pontuacao) AS PontuacaoTotal,
    COUNT(*) AS TotalJogos,
    CAST(AVG(CAST(p.Pontuacao AS DECIMAL(10,2))) AS DECIMAL(10,2)) AS MediaPontuacao,
    SUM(CASE WHEN p.Venceu = 1 THEN 1 ELSE 0 END) AS TotalVitorias
FROM Pontuacoes p
INNER JOIN Usuarios u ON p.UsuarioId = u.Id
GROUP BY u.Nome, u.Usuario, p.Jogo;
GO

CREATE OR ALTER VIEW vwTop10Geral AS
SELECT 
    ROW_NUMBER() OVER (ORDER BY p.Pontuacao DESC) AS Posicao,
    u.Nome,
    u.Usuario,
    p.Jogo,
    p.Pontuacao,
    p.DataHora
FROM Pontuacoes p
INNER JOIN Usuarios u ON p.UsuarioId = u.Id;
GO

INSERT INTO Usuarios (Nome, Usuario, Senha, IsAdmin) 
SELECT 'Administrador', 'admin', 'admin123', 1
WHERE NOT EXISTS (SELECT 1 FROM Usuarios WHERE Usuario = 'admin');

GO