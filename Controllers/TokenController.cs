using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TesteJWT.Models;

namespace TesteJWT.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult RequestToToken([FromBody] User user)
        {
            if (user.Username.Equals("batman") && user.Password.Equals("numsey"))
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.Username)
                    //new Claim(ClaimTypes.Role, "Admin")
                };

                // Armazena a chave de criptografia usada na criação do token
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));

                // Define o algortimo de segurança para geração de assinatura de digital para o token
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "https://github.com/Jhow-A",
                    audience: "https://github.com/Jhow-A",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: credentials);

                return Ok(new
                {
                    tokenJWT = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }

            return Unauthorized("Credenciais Inválidas");
            //return Forbid(JwtBearerDefaults.AuthenticationScheme);
        }
    }
}
