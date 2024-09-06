using System.ComponentModel.DataAnnotations;

namespace API.Data.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

    }
}