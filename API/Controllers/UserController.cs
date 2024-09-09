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
    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext Context;
        private readonly IMapper _mapper;

        public UserController(AppDbContext context, IMapper mapper)
        {
            Context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            try
            {
                var users = await Context.Users
                
                    .AsNoTracking()
                    .ToListAsync();

                var userDtos = _mapper.Map<List<UserDto>>(users);
                return Ok(userDtos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }

        [HttpGet("{id:int:min(1)}", Name = "GetUserById")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            try
            {
                var user = await Context.Users

                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                    return NotFound($"Usuário com id {id} não encontrado.");

                var userDto = _mapper.Map<UserDto>(user);
                return Ok(userDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser(UserDto userDto)
        {
            try
            {
                if (userDto == null)
                {
                    return BadRequest("Dados inválidos.");
                }

                var user = _mapper.Map<User>(userDto);
                user.SenhaHash = BCrypt.Net.BCrypt.HashPassword(userDto.Senha);  // Hash a senha

                Context.Users.Add(user);
                await Context.SaveChangesAsync();

                var createdUserDto = _mapper.Map<UserDto>(user);
                return CreatedAtRoute("GetUserById", new { id = user.Id }, createdUserDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult> UpdateUser(int id, UserDto userDto)
        {
            try
            {
                if (id != userDto.Id)
                    return BadRequest("Dados inválidos.");

                var user = await Context.Users.FindAsync(id);
                if (user == null)
                    return NotFound($"Usuário com id {id} não encontrado.");

                _mapper.Map(userDto, user);  // Atualize as propriedades do usuário existente

                Context.Entry(user).State = EntityState.Modified;
                await Context.SaveChangesAsync();

                return Ok(userDto);
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
                var user = await Context.Users.FindAsync(id);

                if (user == null)
                    return NotFound($"Usuário com id {id} não encontrado.");


                Context.Users.Remove(user);

                await Context.SaveChangesAsync();

                var userDto = _mapper.Map<UserDto>(user);
                return Ok(userDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }
    }
}
