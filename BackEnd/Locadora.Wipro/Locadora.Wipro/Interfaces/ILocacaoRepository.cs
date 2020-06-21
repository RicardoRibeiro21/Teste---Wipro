using Locadora.Wipro.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locadora.Wipro.Interfaces
{
    interface ILocacaoRepository
    {
        /// <summary>
        /// Registra uma locação
        /// </summary>
        /// <param name="locacao">Objeto do tipo Locacao</param>
        string Post(Locacao locacao);

        /// <summary>
        /// Registra uma locação
        /// </summary>
        /// <param name="idLocacao">Objeto do tipo Locacao</param>
        string PutRealizarEntrega(int idLocacao);

        /// <summary>
        /// Busca todas as locacoes na base de dados
        /// </summary>
        /// <returns>Uma lista de locacoes</returns>
        List<Locacao> GetLocacoes();

        /// <summary>
        /// Busca uma locacao através do id fornecido
        /// </summary>
        /// <param name="idLocacao">Id do tipo inteiro</param>
        /// <returns>Um objeto do tipo Locacao</returns>
        Locacao GetLocacaoById(int idLocacao);
    }
}
