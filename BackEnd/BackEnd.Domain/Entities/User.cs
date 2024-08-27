namespace BackEnd.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string SenhaHash { get; set; }
    }
}
