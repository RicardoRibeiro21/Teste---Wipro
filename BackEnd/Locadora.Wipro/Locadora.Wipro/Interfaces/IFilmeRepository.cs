using Locadora.Wipro.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locadora.Wipro.Interfaces
{
    interface IFilmeRepository
    {
        /// <summary>
        /// Cadastra um Filme na base de dados
        /// </summary>
        /// <param name="filme">Recebe um objeto do tipo Filme</param>
        void Post(Filme filme);

        /// <summary>
        /// Busca todos os filmes cadastrados na base de dados
        /// </summary>
        /// <returns>Retorna uma lista de filmes</returns>
        List<Filme> GetFilmes();

        /// <summary>
        /// Busca um filme através do id fornecido
        /// </summary>
        /// <param name="idFilme">Id do tipo Inteiro</param>
        /// <returns>Um objeto do tipo Filme</returns>
        Filme GetFilmeById(int idFilme);

        /// <summary>
        /// Atualiza o filme, assim como sua disponibilidade
        /// </summary>
        /// <param name="filme">Objeto filme</param>
        void Put(Filme filme);

    }
}
