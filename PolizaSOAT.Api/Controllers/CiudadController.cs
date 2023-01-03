using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolizaSOAT.Core.DTOs;
using PolizaSOAT.Core.Entities;
using PolizaSOAT.Core.Enumerations;
using PolizaSOAT.Core.Interfaces;

namespace PolizaSOAT.Api.Controllers
{
    [Authorize(Roles = nameof(RoleType.Administrator))]
    [Route("api/CiudadVenta")]
    [ApiController]
    public class CiudadController : ControllerBase
    {
        private readonly ICiudadService _ciudadService;
        private readonly IMapper _mapper;
        public CiudadController(ICiudadService ciudadService, IMapper mapper)
        {
            _ciudadService = ciudadService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetCiudades()
        {
            var ciudades= await _ciudadService.GetAllCiudades();
            var ciudadesDTO = _mapper.Map<IEnumerable<CiudadVentaDTO>>(ciudades);
            return Ok(ciudadesDTO);
        }

        // GET: api/CiudadVenta/int
        [HttpGet("{id}")]
        public async Task<ActionResult<CiudadVenta>> GetCiudad(int id)
        {
            var ciudad= await _ciudadService.GetCiudad(id);
            var ciudadDTO = _mapper.Map<CiudadVentaDTO>(ciudad);
            return Ok(ciudadDTO);
        }

        // POST: api/CiudadVenta
        [HttpPost]
        public async Task<ActionResult<CiudadVenta>> PostCiudad(CiudadVentaDTO ciudadDTO)
        {
            var ciudad = _mapper.Map<CiudadVenta>(ciudadDTO);
            await _ciudadService.InsertCiudad(ciudad);
            var ciudadDto = _mapper.Map<CiudadVentaDTO>(ciudad);
            return Ok(ciudadDto);
        }
        
        // PUT: api/CiudadVenta/int
        [HttpPut]
        public async Task<IActionResult> PutCiudad(int id, CiudadVentaDTO ciudadDTO)
        {
            var ciudad = _mapper.Map<CiudadVenta>(ciudadDTO);
            ciudad.Id = id;
            var result= await _ciudadService.UpdateCiudad(ciudad);
            return Ok(result);
        }

        // DELETE: api/CiudadVenta/int
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCiudad(int id)
        {
            var result= await _ciudadService.DeleteCiudad(id);
            return Ok(result);
        }
    }
}
