using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Domain.Entities
{
    public  class Endereco:BaseEntity
    {
        public string? Estado { get; set; }
        public string? Cidade { get; set; }
        public string? Bairro { get; set; }
        public string? CEP { get; set; }
        public string?  Rua { get; set; }
        public string? Complemento { get; set; }
    }
}
