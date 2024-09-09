using System.ComponentModel.DataAnnotations;

namespace API.Data.Entities
{
    public class Servico
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set;}
    }
}
