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
    public class FilmeRepository : IFilmeRepository
    {
        Conexao conexao = new Conexao();
        public Filme GetFilmeById(int idFilme)
        {
            try
            {
                string query = "SELECT idFilme, nomeFilme, dtLancamento, disponibilidade " +
                    "             FROM tb_Filme " +
                    "            WHERE idFilme = @idFilme";

                using (SqlConnection con = new SqlConnection(conexao.StringConexao))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@idFilme", idFilme);

                        SqlDataReader sqr = cmd.ExecuteReader();
                        if (sqr.HasRows)
                        {
                            while (sqr.Read())
                            {
                                Filme filme = new Filme()
                                {
                                    IdFilme = Convert.ToInt32(sqr["idFilme"]),
                                    NomeFilme = sqr["nomeFilme"].ToString(),
                                    Disponibilidade = Convert.ToBoolean(sqr["disponibilidade"]),
                                    DtLancamento = Convert.ToDateTime(sqr["dtLancamento"])
                                };
                                return filme;
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex) { throw ex; }
        }

        public List<Filme> GetFilmes()
        {
            try
            {
                string query = @"SELECT idFilme, nomeFilme, dtLancamento, disponibilidade FROM tb_Filme;";

                List<Filme> listFilmes = new List<Filme>();

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
                                Filme filme = new Filme()
                                {
                                    IdFilme = Convert.ToInt32(sqr["idFilme"]),
                                    NomeFilme = sqr["nomeFilme"].ToString(),
                                    Disponibilidade = Convert.ToBoolean(sqr["disponibilidade"]),
                                    DtLancamento = Convert.ToDateTime(sqr["dtLancamento"])
                                };
                                listFilmes.Add(filme);
                            }
                        }
                    }
                }
                return listFilmes;
            }
            catch (Exception ex) { throw ex; }
        }

        public void Post(Filme filme)
        {
            try
            {
                string query = @"INSERT INTO tb_Filme (nomeFilme, dtLancamento, disponibilidade) VALUES (@nomeFilme, @dtLancamento, @disponibilidade);";

                using (SqlConnection con = new SqlConnection(conexao.StringConexao))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@nomeFilme", filme.NomeFilme);
                    cmd.Parameters.AddWithValue("@dtLancamento", filme.DtLancamento);
                    cmd.Parameters.AddWithValue("@disponibilidade", filme.Disponibilidade);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public void Put(Filme filme)
        {
            try
            {
                string query = @"UPDATE tb_Filme 
                                    SET disponibilidade = @disponibilidade,
                                        nomeFilme = @nomeFilme,
                                        dtLancamento = @dtLancamento
                                  WHERE idFilme = @idFilme;";

                GetFilmeById(filme.IdFilme);

                if (filme != null)
                {
                    using (SqlConnection con = new SqlConnection(conexao.StringConexao))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@idFilme", filme.IdFilme);
                        cmd.Parameters.AddWithValue("@nomeFilme", filme.NomeFilme);
                        cmd.Parameters.AddWithValue("@dtLancamento", filme.DtLancamento);
                        cmd.Parameters.AddWithValue("@disponibilidade", filme.Disponibilidade);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public void PutDisponibilidade(int idFilme, bool disponibilidade)
        {
            try
            {
                string query = @"UPDATE tb_Filme 
                                    SET disponibilidade = @disponibilidade                                        
                                  WHERE idFilme = @idFilme;";                

                if (GetFilmeById(idFilme) != null)
                {
                    using (SqlConnection con = new SqlConnection(conexao.StringConexao))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@idFilme", idFilme);
                        cmd.Parameters.AddWithValue("@disponibilidade", disponibilidade);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
