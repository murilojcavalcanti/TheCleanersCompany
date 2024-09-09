using System.ComponentModel.DataAnnotations;

namespace API.Data.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Senha{ get; set; }

        [Required]
        public string SenhaHash { get; set; }

    }
}