using BackEnd.Domain.Entities;
using BackEnd.Infra.DbContext;

namespace BackEnd.Persistence.Repositories
{
    public class ClienteRepository
    {
        private readonly ApplicationDbContext _context;

        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
        }

        // Métodos para obter, atualizar e remover clientes
    }
}
