using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PolizaSOAT.Core.CustomEntities;
using PolizaSOAT.Core.DTOs;
using PolizaSOAT.Core.Entities;
using PolizaSOAT.Core.Enumerations;
using PolizaSOAT.Core.Interfaces;
using PolizaSOAT.Core.QueryFilters;
using PolizaSOAT.Infrastructure.Interfaces;
using PolizaSOAT.Responses;
using System.Net;

namespace PolizaSOAT.Api.Controllers
{
    [Authorize(Roles = nameof(RoleType.Administrator))]
    [Route("api/CiudadVenta")]
    [ApiController]
    public class CiudadController : ControllerBase
    {
        private readonly ICiudadService _ciudadService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        public CiudadController(ICiudadService ciudadService, IMapper mapper, IUriService uriService)
        {
            _ciudadService = ciudadService;
            _mapper = mapper;
            _uriService = uriService;
        }
        
        [HttpGet(Name = nameof(GetCiudades))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<CiudadVentaDTO>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCiudades([FromQuery]CiudadQueryFilters filters)
        {
            var ciudades=  _ciudadService.GetAllCiudades(filters);
            var ciudadesDTO = _mapper.Map<IEnumerable<CiudadVentaDTO>>(ciudades);
            var metadata = new Metadata
            {
                TotalCount = ciudades.TotalCount,
                TotalPages = ciudades.TotalPages,
                HasNextPage = ciudades.HasNextPage,
                HasPreviousPage = ciudades.HasPreviousPage,
                CurrentPage = ciudades.CurrentPage,
                PageSize = ciudades.PageSize,
                NextPageURL = _uriService.GetCiudadesPaginationUri(filters, Url.RouteUrl(nameof(GetCiudades))).ToString(),
                PreviousPageURL = _uriService.GetCiudadesPaginationUri(filters, Url.RouteUrl(nameof(GetCiudades))).ToString()
            };
            var response = new ApiResponse<IEnumerable<CiudadVentaDTO>>(ciudadesDTO)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
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
