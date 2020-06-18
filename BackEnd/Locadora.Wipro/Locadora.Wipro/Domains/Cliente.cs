using System;
using System.Collections.Generic;

namespace Locadora.Wipro.Domains
{
    public partial class Cliente
    {
        public Cliente()
        {
            TbLocacao = new HashSet<Locacao>();
        }

        public int IdCliente { get; set; }
        public string NomeCliente { get; set; }
        public string Cpf { get; set; }
        public DateTime? DtNascimento { get; set; }

        public virtual ICollection<Locacao> TbLocacao { get; set; }
    }
}
