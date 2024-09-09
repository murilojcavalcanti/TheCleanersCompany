using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using API.Data;
using API.Data.Entities;
using Org.BouncyCastle.Crypto.Generators;
using AutoMapper; 

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AppDbContext Context;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public AuthController(AppDbContext context, IConfiguration configuration)
    {
        Context = context;
        _configuration = configuration;
        _mapper = mapper;
    }

    // Endpoint para registro
    [HttpPost("register")]
    public async Task<IActionResult> Register(UserDto request)
    {
        if (Context.Users.Any(u => u.Email == request.Email))
        {
            return BadRequest("Usuário já registrado.");
        }

         var user = _mapper.Map<User>(request);
        user.SenhaHash = BCrypt.Net.BCrypt.HashPassword(request.Senha);

        Context.Users.Add(user);
        await Context.SaveChangesAsync();

        return Ok("Usuário registrado com sucesso.");
    }

    // Endpoint para login
    [HttpPost("login")]
    public async Task<IActionResult> Login(UserDto request)
    {
        var user = await Context.Users.FirstOrDefault(u => u.Email == request.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Senha, user.SenhaHash))
        {
            return BadRequest("Credenciais inválidas.");
        }

        string token = CreateToken(user);
        return Ok(new { Token = token });
    }

    // Geração do token JWT
    private string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Nome),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
