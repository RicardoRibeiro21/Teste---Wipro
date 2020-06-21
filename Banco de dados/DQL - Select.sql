SELECT idCliente, nomeCliente, CPF, dtNascimento FROM tb_Cliente;

SELECT idFilme, nomeFilme, dtLancamento, disponibilidade FROM tb_Filme;

SELECT idLocacao, idFilme, idCliente, dtEntrega FROM tb_Locacao;

SELECT tl.idLocacao, tf.nomeFilme, tc.nomeCliente, tl.dtEntrega
  FROM tb_Locacao tl INNER JOIN tb_Cliente tc ON tl.idCliente = tc.idCliente
					 INNER JOIN tb_Filme tf ON tl.idFilme= tf.idFilme;

SELECT tf.idFilme ,tf.nomeFilme tl.dtEntrega, FROM tb_Locacao tl INNER JOIN tb_Filme tf ON tl.idFilme = tf.idFilme
