using Microsoft.AspNetCore.Mvc;
using BackEnd.Domain.Entities;
using BackEnd.Persistence.Repositories;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly ClienteRepository _clienteRepository;

    public ClienteController(ClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CriarCliente([FromBody] Cliente cliente)
    {
        await _clienteRepository.AdicionarCliente(cliente);
        return Ok(cliente);
    }

    // Outros métodos como GET, PUT, DELETE
}
