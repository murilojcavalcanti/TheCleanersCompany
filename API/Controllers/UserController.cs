using API.Data;
using API.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                var users = await _context.Users
                    .Include(u => u.Categorias)
                    .Include(u => u.Servicos)
                    .AsNoTracking()
                    .ToListAsync();
                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }

        [HttpGet("{id:int:min(1)}", Name = "GetUserById")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            try
            {
                var user = await _context.Users
                    .Include(u => u.Categorias)
                    .Include(u => u.Servicos)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                    return NotFound($"Usuário com id {id} não encontrado.");

                return Ok(user);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("Dados inválidos.");
                }

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return new CreatedAtRouteResult("GetUserById", new { id = user.Id }, user);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult> UpdateUser(int id, User user)
        {
            try
            {
                if (id != user.Id)
                    return BadRequest("Dados inválidos.");

                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(user);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _context.Users
                    .Include(u => u.Categorias)
                    .Include(u => u.Servicos)
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                    return NotFound($"Usuário com id {id} não encontrado.");

                // Remover categorias e serviços associados
                _context.Categorias.RemoveRange(user.Categorias);
                _context.Servicos.RemoveRange(user.Servicos);
                _context.Users.Remove(user);

                await _context.SaveChangesAsync();

                return Ok(user);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }
    }
}
