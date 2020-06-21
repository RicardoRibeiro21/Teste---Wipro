using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Locadora.Wipro.Domains
{
    public partial class Locacao
    {
        public int IdLocacao { get; set; }

        [Required(ErrorMessage = "Informe o id do filme")]
        [Newtonsoft.Json.JsonIgnore]
        public int IdFilme { get; set; }

        [Required(ErrorMessage = "Informe o id do cliente")]
        [Newtonsoft.Json.JsonIgnore]
        public int IdCliente { get; set; }
        [Required(ErrorMessage = "Informe a data de entrega")]
        [DataType(DataType.Date, ErrorMessage = "Informe uma data válida")]
        public DateTime DtEntrega { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; }
        public virtual Filme IdFilmeNavigation { get; set; }
    }
}
