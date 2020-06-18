using System;
using System.Collections.Generic;

namespace Locadora.Wipro.Domains
{
    public partial class Locacao
    {
        public int IdLocacao { get; set; }
        public int IdFilme { get; set; }
        public int IdCliente { get; set; }
        public DateTime DtEntrega { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; }
        public virtual Filme IdFilmeNavigation { get; set; }
    }
}
