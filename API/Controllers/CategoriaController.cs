using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Data.Entities;
using AutoMapper;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly AppDbContext Context;
        private readonly IMapper _mapper; 

        public CategoriaController(AppDbContext context, IMapper mapper)
        {
            Context = context;
            _mapper = mapper;  
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDto>>> BuscaCategorias()
        {
            List<Categoria> categorias;
            try
            {
                categorias = await Context.Categoria.ToListAsync();
                var categoriaDtos = _mapper.Map<List<CategoriaDto>>(categorias);  // Mapeie para CategoriaDto
                return categoriaDtos;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }

        }

        
        [HttpGet("{id:int:min(1)}", Name = "BuscaCategoriaPorId")]
        public async Task<ActionResult<CategoriaDto>> BuscaCategoriaPorId(int id)
        {
            try
            {
                var categoria = await Context.Categoria.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
                if (categoria == null)
                {
                    return NotFound();  // Retorna NotFound se a categoria não for encontrada
                }
                var categoriaDto = _mapper.Map<CategoriaDto>(categoria);  // Mapeie para CategoriaDto
                return categoriaDto;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<CategoriaDto>> CriaCategoria(CategoriaDto categoriaDto)
        {
            try
            {
                if (categoriaDto == null) return BadRequest("Dados Inválidos");

                var categoria = _mapper.Map<Categoria>(categoriaDto);  
                Context.Categoria.Add(categoria);
                await Context.SaveChangesAsync();

                var createdCategoriaDto = _mapper.Map<CategoriaDto>(categoria);  
                return CreatedAtRoute("BuscaCategoriaPorId", new { id = categoria.Id }, createdCategoriaDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }
        

        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> AtualizaCategoria(int id, CategoriaDto categoriaDto)
        {

            try
            {
                if (id != categoriaDto.Id) return BadRequest("Dados Inválidos");

                var categoria = _mapper.Map<Categoria>(categoriaDto);  
                Context.Entry(categoria).State = EntityState.Modified;
                await Context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }


        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletaCategoria(int id)
        {
            try
            {
                var categoria = await Context.Categoria.FirstOrDefaultAsync(c => c.Id == id);
                if (categoria == null) return NotFound($"Categoria com id -> {id} não foi encontrada");

                Context.Categoria.Remove(categoria);
                await Context.SaveChangesAsync();

                var categoriaDto = _mapper.Map<CategoriaDto>(categoria);  
                return Ok(categoriaDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }
    }
}
