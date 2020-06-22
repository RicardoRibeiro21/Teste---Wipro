using Locadora.Wipro.Domains;
using Locadora.Wipro.Interfaces;
using Locadora.Wipro.Services;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locadora.Wipro.Repositories
{
    public class LocacaoRepository : ILocacaoRepository
    {
        Conexao conexao = new Conexao();
        public Locacao GetLocacaoById(int idLocacao)
        {
            try
            {
                string query = @"SELECT tl.idLocacao, tf.idFilme, tf.nomeFilme, tc.idCliente, tc.nomeCliente, tl.dtEntrega " +
                    "              FROM tb_Locacao tl INNER JOIN tb_Cliente tc ON tl.idCliente = tc.idCliente " +
                    "                                 INNER JOIN tb_Filme tf ON tl.idFilme = tf.idFilme " +
                    "             WHERE idLocacao = @idLocacao";

                using (SqlConnection con = new SqlConnection(conexao.StringConexao))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@idLocacao", idLocacao);

                        SqlDataReader sqr = cmd.ExecuteReader();
                        if (sqr.HasRows)
                        {
                            while (sqr.Read())
                            {
                                Locacao locacao = new Locacao()
                                {
                                    IdLocacao = Convert.ToInt32(sqr["idLocacao"]),
                                    IdClienteNavigation = new Cliente()
                                    {
                                        IdCliente = Convert.ToInt32(sqr["idCliente"]),
                                        NomeCliente = sqr["nomeCliente"].ToString()
                                    },
                                    IdCliente = Convert.ToInt32(sqr["idCliente"]),
                                    IdFilmeNavigation = new Filme()
                                    {
                                        IdFilme = Convert.ToInt32(sqr["idFilme"]),
                                        NomeFilme = sqr["nomeFilme"].ToString()
                                    },
                                    IdFilme = Convert.ToInt32(sqr["idFilme"]),
                                    DtEntrega = Convert.ToDateTime(sqr["dtEntrega"])
                                };
                                return locacao;
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex) { throw ex; }
        }

        public List<Locacao> GetLocacoes()
        {
            try
            {
                string query = @"SELECT tl.idLocacao, tf.idFilme, tf.nomeFilme, tf.dtLancamento, tc.idCliente, tc.nomeCliente, tl.dtEntrega " +
                    "              FROM tb_Locacao tl INNER JOIN tb_Cliente tc ON tl.idCliente = tc.idCliente " +
                    "                                 INNER JOIN tb_Filme tf ON tl.idFilme = tf.idFilme ";

                List<Locacao> listLocacoes = new List<Locacao>();

                using (SqlConnection con = new SqlConnection(conexao.StringConexao))
                {
                    // Abrindo a conexão com o banco
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Executando a leitura
                        SqlDataReader sqr = cmd.ExecuteReader();
                        if (sqr.HasRows)
                        {
                            while (sqr.Read())
                            {
                                Locacao locacao = new Locacao()
                                {
                                    IdLocacao = Convert.ToInt32(sqr["idLocacao"]),
                                    IdClienteNavigation = new Cliente()
                                    {
                                        IdCliente = Convert.ToInt32(sqr["idCliente"]),
                                        NomeCliente = sqr["nomeCliente"].ToString()
                                    },
                                    IdFilmeNavigation = new Filme()
                                    {
                                        IdFilme = Convert.ToInt32(sqr["idFilme"]),
                                        NomeFilme = sqr["nomeFilme"].ToString(),
                                        DtLancamento = Convert.ToDateTime(sqr["dtLancamento"])
                                    },
                                    DtEntrega = Convert.ToDateTime(sqr["dtEntrega"])
                                };
                                // Adicionando a locação na lista
                                listLocacoes.Add(locacao);
                            }
                        }
                    }
                }
                return listLocacoes;
            }
            catch (Exception ex) { throw ex; }
        }

        public string Post(Locacao locacao)
        {
            string mRetorno = String.Empty;
            try
            {
                ClienteRepository clienteRepository = new ClienteRepository();
                FilmeRepository filmeRepository = new FilmeRepository();

                // Verificando se o cliente existe
                if (clienteRepository.GetClienteById(locacao.IdCliente) != null)
                {
                    Filme filme = filmeRepository.GetFilmeById(locacao.IdFilme);

                    // Verificando se o filme existe e se está disponível para alugar
                    if (filme != null && filme.Disponibilidade)
                    {
                        // Adicionando os valores da locação
                        string query = @"INSERT INTO tb_Locacao (idCliente, idFilme, dtEntrega) VALUES (@idCliente, @idFilme, @dtEntrega);";

                        using (SqlConnection con = new SqlConnection(conexao.StringConexao))
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand(query, con);
                            cmd.Parameters.AddWithValue("@idCliente", locacao.IdCliente);
                            cmd.Parameters.AddWithValue("@idFilme", locacao.IdFilme);
                            cmd.Parameters.AddWithValue("@dtEntrega", locacao.DtEntrega);
                            cmd.ExecuteNonQuery();
                        }

                        // Atualizando a disponibilidade do filme
                        filme.Disponibilidade = false;
                        filmeRepository.Put(filme);

                        mRetorno = "Locação Realizada com sucesso!";
                    }
                    else mRetorno = "O Filme já está alugado.";
                }
            }
            catch (Exception ex) { throw ex; }
            return mRetorno;
        }

        public string PutRealizarEntrega(int idLocacao)
        {
            string mRetorno = "Filme entregue com sucesso!";
            try
            {
                string query = @"UPDATE tb_Locacao 
                                    SET dtEntrega = @dtEntrega
                                  WHERE idLocacao = @idLocacao;";

                // Buscando a locacao através do Id
                Locacao locacao = new Locacao();
                locacao = GetLocacaoById(idLocacao);

                if (locacao != null)
                {
                    // Verificando a data de entrega
                    if (locacao.DtEntrega < DateTime.Now) mRetorno = "Filme com atraso na entrega!";

                    using (SqlConnection con = new SqlConnection(conexao.StringConexao))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@idLocacao", idLocacao);
                        cmd.Parameters.AddWithValue("@dtEntrega", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }

                    // Atualizando a disponibilidade do filme após a entrega
                    FilmeRepository filmeRepository = new FilmeRepository();
                    filmeRepository.PutDisponibilidade(locacao.IdFilmeNavigation.IdFilme, true);
                }
            }
            catch (Exception ex) { throw ex; }
            return mRetorno;
        }
    }
}
