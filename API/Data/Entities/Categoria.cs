using System.ComponentModel.DataAnnotations;

namespace API.Data.Entities
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Nome { get; set; }
        public int? ServicoId { get; set; }
        public ICollection<Servico>? Servicos { get; set; }

    }
}
