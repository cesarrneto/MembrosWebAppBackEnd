using CadastroMembrosApi.Services;
using CadastroMembrosApi.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IAuthenticate _authentication;
    private readonly UserManager<IdentityUser> _userManager;

    public AccountController(UserManager<IdentityUser> userManager, IConfiguration configuration, IAuthenticate authentication)
    {
        _authentication = authentication;
        _configuration = configuration;
        _userManager = userManager;
    }

    [HttpPost("CreateUser")]
    public async Task<IActionResult> CreateUser([FromBody] RegisterModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid request");

        var user = new IdentityUser { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            return BadRequest(new { CreateUser = errors });
        }

        return Ok(new { Message = "Usuário criado com sucesso" });
    }

    [HttpPost("LoginUser")]
    public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel userInfo)
    {
        var result = await _authentication.Authenticate(userInfo.Email, userInfo.Password);

        if (result)
        {
            return Ok(GenerateToken(userInfo));
        }
        else
        {
            ModelState.AddModelError("LoginUser", "Login inválido");
            return BadRequest(ModelState);
        }
    }

    private UserToken GenerateToken(LoginModel userInfo)
    {
        var claims = new[]
        {
            new Claim("email", userInfo.Email),
            new Claim("meuToken", "token da icerjb"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddMinutes(30);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: creds);

        return new UserToken()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration,
        };
    }
}
