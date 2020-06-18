USE db_locadora;

INSERT INTO tb_Cliente VALUES ('Ricardo Ribeiro', '123.564.465-10', '2000-10-11');

INSERT INTO tb_Filme VALUES ('Efeito Borboleta', '2004-03-12', 0),
							('Piratas do Caribe: No fim do mundo', '2007-04-11', 0),
							('Capitão Fantástico', '2016-08-22', 0),
							('Os fantasmas de Scrooge', '2009-05-05', 1);

-- Adicionando uma locação com data de entrega após dois dias da data atual
INSERT INTO tb_Locacao VALUES (4, 1, DATEADD(day, 2 ,GETDATE()));

-- Atualizando a data de entrega em 4 dias após a data atual
UPDATE tb_Locacao SET dtEntrega = DATEADD(day, 4 ,GETDATE()) WHERE idLocacao = 1;