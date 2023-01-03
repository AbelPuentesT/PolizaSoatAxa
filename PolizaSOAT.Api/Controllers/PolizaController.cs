using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolizaSOAT.Core.DTOs;
using PolizaSOAT.Core.Entities;
using PolizaSOAT.Core.Enumerations;
using PolizaSOAT.Core.Interfaces;
using PolizaSOAT.Core.QueryFilters;

namespace PolizaSOAT.Api.Controllers
{
    
    [Route("api/Poliza")]
    [ApiController]
    public class PolizaController : ControllerBase
    {
        private readonly IPolizaService _polizaService;
        private readonly IMapper _mapper;
        public PolizaController(IPolizaService polizaService, IMapper mapper)
        {
            _polizaService = polizaService;
            _mapper= mapper;

        }

        // GET: api/Poliza
        [Authorize]
        [HttpGet]   
        public async Task<IActionResult> GetAllPolizas([FromQuery]PolizaQueryFilters filters)
        {
            var polizas =  await _polizaService.GetAllPolizas(filters);
            var polizasDTO = _mapper.Map<IEnumerable<PolizaDTO>>(polizas);
            return Ok(polizasDTO);
        }

        // GET: api/Poliza/int
        [Authorize(Roles = nameof(RoleType.Administrator))]
        [HttpGet("{int}")]
        public async Task<IActionResult> GetPoliza(int id)
        {
            var poliza = await _polizaService.GetPoliza(id);
            var polizaDTO= _mapper.Map<PolizaDTO>(poliza);
            return Ok(polizaDTO);
        }

        // POST: api/Poliza
        [Authorize(Roles = nameof(RoleType.Administrator))]
        [HttpPost]
        public async Task<IActionResult> PostPoliza(PolizaDTO polizaDTO)
        {
            var poliza= _mapper.Map<Poliza>(polizaDTO);
            await _polizaService.InsertPoliza(poliza);
            var polizaDto=_mapper.Map<PolizaDTO>(poliza);
            return Ok(polizaDto);
            
        }

        // PUT: api/Poliza/int
        [Authorize(Roles = nameof(RoleType.Administrator))]
        [HttpPut]
        public async Task<IActionResult> PutPoliza(int id, PolizaDTO polizaDTO)
        {
            var poliza = _mapper.Map<Poliza>(polizaDTO);
            poliza.Id = id;
            var result= await _polizaService.UpdatePoliza(poliza);
            return Ok(result);
        }


        // DELETE: api/Poliza/int
        [Authorize(Roles = nameof(RoleType.Administrator))]
        [HttpDelete("{int}")]
        public async Task<IActionResult> DeletePoliza(int id)
        {
            var result = await _polizaService.DeletePoliza(id);
            return Ok(result);
        }

    }
}
