using System;
using System.Collections.Generic;

namespace Locadora.Wipro.Domains
{
    public partial class Filme
    {
        public Filme()
        {
            TbLocacao = new HashSet<Locacao>();
        }

        public int IdFilme { get; set; }
        public string NomeFilme { get; set; }
        public DateTime DtLancamento { get; set; }
        public bool Disponibilidade { get; set; }

        public virtual ICollection<Locacao> TbLocacao { get; set; }
    }
}
