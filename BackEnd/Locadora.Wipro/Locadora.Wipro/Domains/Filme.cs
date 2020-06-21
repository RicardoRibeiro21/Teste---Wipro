using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Locadora.Wipro.Domains
{
    public partial class Filme
    {
        public Filme()
        {
            TbLocacao = new HashSet<Locacao>();
        }        
        public int IdFilme { get; set; }
        [Required(ErrorMessage = "Informe o nome do Filme")]
        public string NomeFilme { get; set; }
        [Required(ErrorMessage = "Informe a data de lançamento do Filme")]
        [DataType(DataType.Date, ErrorMessage = "Informe uma data válida")]
        public DateTime DtLancamento { get; set; }
        [Required(ErrorMessage = "Informe a disponibilidade do Filme")]
        public bool Disponibilidade { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<Locacao> TbLocacao { get; set; }
    }
}
