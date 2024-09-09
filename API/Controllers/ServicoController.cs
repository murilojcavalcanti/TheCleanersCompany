using API.Data;
using API.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace API.Controllers
{

    [ApiController]
    [Route("[Controller]")]
    public class ServicoController : Controller
    {
        private AppDbContext Context;
        private readonly IMapper _mapper;

        public ServicoController(AppDbContext context, IMapper mapper)
        {
            Context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Servico>>> BuscaServico()
        {
            List<Servico> Servicos;
            try
            {
                Servicos = await Context.Servicos.AsNoTracking().ToListAsync();
                var servicosDto = _mapper.Map<IEnumerable<ServicoDto>>(servicos);
                return Ok(servicosDto);
            }catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }

        [HttpGet("{id:int:min(1)}",Name = "BuscaServicoPorId")]
        public async Task<ActionResult<Servico>> BuscaServicoPorId(int id)
        {
            try
            {
                var servico = await Context.Servicos.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
                if (servico == null) return NotFound();
                var servicoDto = _mapper.Map<ServicoDto>(servico);
                return Ok(servicoDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ServicoDto>> CriaServico(ServicoDto servicoDto)
        {
            try
            {
                if (servicoDto == null) return BadRequest("Dados inválidos");

                var servico = _mapper.Map<Servico>(servicoDto);
                Context.Servicos.Add(servico);
                await Context.SaveChangesAsync();

                var novoServicoDto = _mapper.Map<ServicoDto>(servico);
                return CreatedAtRoute("BuscaServicoPorId", new { id = servico.Id }, novoServicoDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult> AtualizaServico(int id, ServicoDto servicoDto)
        {
            try
            {
                if (id != servicoDto.Id) return BadRequest("Dados inválidos");

                var servico = _mapper.Map<Servico>(servicoDto);
                Context.Entry(servico).State = EntityState.Modified;
                await Context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }
        
        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult> DeletaServico(int id)
        {
            try
            {
                var servico = await Context.Servicos.FirstOrDefaultAsync(s => s.Id == id);
                if (servico == null) return NotFound($"Serviço com id -> {id} não foi encontrado");

                Context.Servicos.Remove(servico);
                await Context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }

    }
}
