using Locadora.Wipro.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locadora.Wipro.Interfaces
{
    interface IClienteRepository
    {
        /// <summary>
        /// Recebe um cliente como parâmetro e cadastra na base de dados
        /// </summary>
        /// <param name="cliente">Objeto do tipo Cliente</param>
        void Post(Cliente cliente);

        /// <summary>
        /// Busca todos os clientes cadastrados
        /// </summary>
        /// <returns>Retorna uma lista de clientes</returns>
        List<Cliente> GetClientes();

        /// <summary>
        /// Busca um cliente através do id fornecido
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Um objeto do tipo Cliente</returns>
        Cliente GetClienteById(int idCliente);
    }
}
