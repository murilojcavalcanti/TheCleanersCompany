using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Data.Entities;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly AppDbContext Context;

        public CategoriaController(AppDbContext context)
        {
            Context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> BuscaCategorias()
        {
            List<Categoria> categorias;
            try
            {
                categorias= await Context.Categoria.ToListAsync();
                return categorias;
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
            
        }

        
        [HttpGet("{id:int:min(1)}",Name = "BuscaCategoriaPorId")]
        public async Task<ActionResult<Categoria>> BuscaCategoriaPorId(int id)
        {
            try
            {
                var categoria = await Context.Categoria.AsNoTracking().FirstOrDefaultAsync(c=>c.Id==id);
                return categoria;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }
        
        [HttpPost]
        public  ActionResult CriaCategoria(Categoria categoria)
        {
            try
            {
                if (categoria is null) return BadRequest("Dados Invalidos");

                Context.Categoria.Add(categoria);
                Context.SaveChanges();
                return new CreatedAtRouteResult("BuscaCategoriaPorId", new { id = categoria.Id }, categoria);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }

        
        [HttpPut("{id:int:min(1)}")]
        public ActionResult AtualizaCategoria(int id, Categoria categoria)
        {

            try
            {
                if (id != categoria.Id) return BadRequest();
                
                Context.Entry(categoria).State = EntityState.Modified;
                Context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }


        
        [HttpDelete("{id}")]
        public ActionResult DeletaCategoria(int id)
        {
            try
            {
                var categoria = Context.Categoria.FirstOrDefault(c => c.Id == id);
                if (categoria is null) return BadRequest($"Categoria com id -> {id} não foi encontrado");
                Context.Categoria.Remove(categoria);
                Context.SaveChanges();

                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }
    }
}
