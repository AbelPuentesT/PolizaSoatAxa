using Microsoft.AspNetCore.Mvc;
using PolizaSOAT.Core.Entities;
using PolizaSOAT.Infrastructure.Interfaces;

namespace PolizaSOAT.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        // POST: api/Token
        [HttpPost]
        public async Task<IActionResult> Authentication(UserLogin login)
        {
            var validation = await _tokenService.IsValidUser(login);

            if (validation.Item1)
            {
                var token = _tokenService.GenerateToken(validation.Item2);
                return Ok(new { token });
            }
            return NotFound();

        }
    }
}
