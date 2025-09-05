using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratacaoService.Domain.Entities
{
    public class Contrato
    {
        public Guid Id { get; set; }
        public Guid PropostaId { get; set; }
        public DateTime DataContratacao { get; set; }
    }

}
