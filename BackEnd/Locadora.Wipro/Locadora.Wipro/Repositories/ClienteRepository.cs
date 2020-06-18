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
    public class ClienteRepository : IClienteRepository
    {
        Conexao conexao = new Conexao();

        public Cliente GetClienteById(int idCliente)
        {
            try
            {                
                string query = "SELECT idCliente, nomeCliente, CPF, dtNascimento " +
                    "             FROM tb_Cliente " +
                    "            WHERE idCliente = @idCliente";

                using (SqlConnection con = new SqlConnection(conexao.StringConexao))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@idCliente", idCliente);

                        SqlDataReader sqr = cmd.ExecuteReader();
                        if (sqr.HasRows)
                        {
                            while (sqr.Read())
                            {
                                Cliente cliente = new Cliente()
                                {
                                    IdCliente = Convert.ToInt32(sqr["idCliente"]),
                                    NomeCliente = sqr["nomeCliente"].ToString(),
                                    Cpf = sqr["CPF"].ToString(),
                                    DtNascimento = Convert.ToDateTime(sqr["dtNascimento"])                                    
                                };
                                return cliente;
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex) { throw ex; }
        }

        public List<Cliente> GetClientes()
        {
            try
            {
                string query = @"SELECT idCliente, nomeCliente, CPF, dtNascimento FROM tb_Cliente;";                                

                List<Cliente> listClientes = new List<Cliente>();

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
                                Cliente cliente = new Cliente()
                                {
                                    IdCliente= Convert.ToInt32(sqr["idCliente"]),
                                    NomeCliente = sqr["nomeCliente"].ToString(),
                                    Cpf = sqr["CPF"].ToString(),
                                    DtNascimento= Convert.ToDateTime(sqr["dtNascimento"])                                    
                                };
                                listClientes.Add(cliente);
                            }
                        }
                    }
                }
                return listClientes;

            }
            catch (Exception ex) { throw ex; }
        }

        public void Post(Cliente cliente)
        {
            try
            {
                string query = @"INSERT INTO tb_Cliente (nomeCliente, CPF, dtNascimento) VALUES (@nomeCliente, @CPF, @dtNascimento);";

                using (SqlConnection con = new SqlConnection(conexao.StringConexao))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@nomeCliente", cliente.NomeCliente);
                    cmd.Parameters.AddWithValue("@CPF", cliente.Cpf);
                    cmd.Parameters.AddWithValue("@dtNascimento", cliente.DtNascimento);                    
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
