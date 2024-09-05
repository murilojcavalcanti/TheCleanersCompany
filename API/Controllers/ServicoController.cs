using API.Data;
using API.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    [ApiController]
    [Route("[Controller]")]
    public class ServicoController : Controller
    {
        private AppDbContext Context;

        public ServicoController(AppDbContext context)
        {
            Context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Servico>>> BuscaServico()
        {
            List<Servico> Servicos;
            try
            {
                Servicos = await Context.Servicos.AsNoTracking().ToListAsync();
                return Servicos;
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
                Servico servico = await Context.Servicos.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
                return servico;
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }

        [HttpPost]
        public ActionResult CriaServico(Servico servico)
        {
            try
            {
                if(servico is null)
                {
                    return BadRequest("Dados invalidos");
                }
                Context.Servicos.Add(servico);
                Context.SaveChanges();
                return new CreatedAtRouteResult("BuscaServicoPorId", new {id = servico.Id}, servico);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }

        [HttpPut("{id:int:min(1)}")]
        public ActionResult Atualizaservico(int id,Servico servico)
        {
            try
            {
                if (id != servico.Id) return BadRequest("Dados Inválidos");
                Context.Entry(servico).State= EntityState.Modified;
                Context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }

        [HttpDelete("{id:int:min(1)}")]
        public ActionResult DeletaServico(int id)
        {
            try
            {
                var servico = Context.Servicos.FirstOrDefault(s => s.Id == id);
                if (servico is null) return BadRequest($"Serviço com id -> {id} não foi encontrado");
                Context.Servicos.Remove(servico);
                Context.SaveChanges();
                return Ok(servico);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua Solicitação");
            }
        }

    }
}
