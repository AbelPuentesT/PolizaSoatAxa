using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolizaSOAT.Core.DTOs;
using PolizaSOAT.Core.Entities;
using PolizaSOAT.Core.Enumerations;
using PolizaSOAT.Core.Interfaces;
using PolizaSOAT.Infrastructure.Interfaces;

namespace PolizaSOAT.Api.Controllers
{
    [Authorize(Roles = nameof(RoleType.Administrator))]
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService _securityService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public SecurityController(ISecurityService securityService, IMapper mapper, IPasswordService passwordService)
        {
            _securityService = securityService;
            _mapper = mapper;
            _passwordService = passwordService;

        }

        // POST: api/Security
        [HttpPost]
        public async Task<IActionResult> PostSucurity(SecurityDTO securitytDTO)
        {
            var security = _mapper.Map<Security>(securitytDTO);
            security.Password = _passwordService.Hash(security.Password);
            await _securityService.RegisterUser(security);
            return Ok();
        }
    }
}
