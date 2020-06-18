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
                                    IdLocacao = Convert.ToInt32(sqr["tl.idLocacao"]),
                                    IdClienteNavigation =
                                    {
                                        IdCliente = Convert.ToInt32(sqr["tc.idCliente"]),
                                        NomeCliente = sqr["tc.nomeCliente"].ToString()
                                    },
                                    IdFilmeNavigation =
                                    {
                                        IdFilme = Convert.ToInt32(sqr["tf.idFilme"]),
                                        NomeFilme = sqr["tf.nomeFilme"].ToString()
                                    },
                                    DtEntrega = Convert.ToDateTime(sqr["tl.dtEntrega"])
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
                string query = @"SELECT tl.idLocacao, tf.idFilme, tf.nomeFilme, tc.idCliente, tc.nomeCliente, tl.dtEntrega " +
                    "              FROM tb_Locacao tl INNER JOIN tb_Cliente tc ON tl.idCliente = tc.idCliente " +
                    "                                 INNER JOIN tb_Filme tf ON tl.idFilme = tf.idFilme ";

                List<Locacao> listLocacoes = new List<Locacao>();

                using (SqlConnection con = new SqlConnection(conexao.StringConexao))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        SqlDataReader sqr = cmd.ExecuteReader();
                        if (sqr.HasRows)
                        {
                            while (sqr.Read())
                            {
                                Locacao locacao = new Locacao()
                                {
                                    IdLocacao = Convert.ToInt32(sqr["tl.idLocacao"]),
                                    IdClienteNavigation =
                                    {
                                        IdCliente = Convert.ToInt32(sqr["tc.idCliente"]),
                                        NomeCliente = sqr["tc.nomeCliente"].ToString()
                                    },
                                    IdFilmeNavigation =
                                    {
                                        IdFilme = Convert.ToInt32(sqr["tf.idFilme"]),
                                        NomeFilme = sqr["tf.nomeFilme"].ToString()
                                    },
                                    DtEntrega = Convert.ToDateTime(sqr["tl.dtEntrega"])
                                };
                                listLocacoes.Add(locacao);
                            }
                        }
                    }
                }
                return listLocacoes;
            }
            catch (Exception ex) { throw ex; }
        }

        public void Post(Locacao locacao)
        {
            try
            {
                ClienteRepository clienteRepository = new ClienteRepository();
                FilmeRepository filmeRepository = new FilmeRepository();

                if (clienteRepository.GetClienteById(locacao.IdCliente) != null)
                {
                    Filme filme = filmeRepository.GetFilmeById(locacao.IdFilme);

                    if (filme != null && filme.Disponibilidade)
                    {
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
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

    }
}
