using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BrunaPhotographSystem.IAm.Data;
using BrunaPhotographSystem.IAm.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BrunaPhotographSystem.IAm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        

        public LoginController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
            
        }

        [HttpPost]
        public async Task<IActionResult> Token(LoginModel model)
        {
            var usuario = new IdentityUser();
            
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Login, model.Password, false, false);
                if (result.Succeeded)
                {
                    //cria token (header + payload >> direitos + signature)
                    var direitos = new List<Claim>()
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, model.Login),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };
                    //var userRoles = await UserManager.GetRolesAsync(new IdentityUser() {UserName = model.Login });
                    
                    //foreach (var userRole in userRoles)
                    //{
                    //    direitos.Add(new Claim("role", userRole));
                    //}
                    var chave = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("brunaphotographsystem-webapi-authentication-valid"));
                    var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        issuer: "BrunaPhotographSystem",
                        audience: "Postman",
                        claims: direitos,
                        signingCredentials: credenciais,
                        expires: DateTime.Now.AddDays(30)
                    );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                    //var Autorize = new { userRoles, tokenString };
                    return Ok(tokenString);
                }
                return Unauthorized(); //401
            }
            return BadRequest(); //400
        }
    }
}