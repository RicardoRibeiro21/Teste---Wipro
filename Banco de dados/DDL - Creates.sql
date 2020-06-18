CREATE DATABASE db_locadora;

USE db_locadora;

CREATE TABLE tb_Cliente (
	idCliente INT IDENTITY PRIMARY KEY,
	nomeCliente VARCHAR(200) NOT NULL,
	CPF CHAR(14) UNIQUE NOT NULL,
	dtNascimento DATE
);

CREATE TABLE tb_Filme (
	idFilme INT IDENTITY PRIMARY KEY,
	nomeFilme VARCHAR(200) NOT NULL,
	dtLancamento DATE NOT NULL,
	disponibilidade BIT NOT NULL
);

CREATE TABLE tb_Locacao (
	idLocacao INT IDENTITY PRIMARY KEY,
	idFilme INT FOREIGN KEY REFERENCES tb_Filme (idFilme) NOT NULL,
	idCliente INT FOREIGN KEY REFERENCES tb_Cliente (idCliente) NOT NULL,
	dtEntrega DATE NOT NULL
);