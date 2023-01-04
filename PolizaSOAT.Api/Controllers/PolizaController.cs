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

    [Route("api/Poliza")]
    [ApiController]
    public class PolizaController : ControllerBase
    {
        private readonly IPolizaService _polizaService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        public PolizaController(IPolizaService polizaService, IMapper mapper, IUriService uriService)
        {
            _polizaService = polizaService;
            _mapper= mapper;
            _uriService= uriService;

        }

        // GET: api/Poliza
        //[Authorize]
        [HttpGet(Name = nameof(GetPolizas))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<PolizaDTO>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetPolizas([FromQuery]PolizaQueryFilters filters)
        {
            var polizas =  _polizaService.GetAllPolizas(filters);
            var polizasDTO = _mapper.Map<IEnumerable<PolizaDTO>>(polizas);
            var metadata = new Metadata
            {
                TotalCount = polizas.TotalCount,
                TotalPages = polizas.TotalPages,
                HasNextPage = polizas.HasNextPage,
                HasPreviousPage = polizas.HasPreviousPage,
                CurrentPage = polizas.CurrentPage,
                PageSize = polizas.PageSize,
                NextPageURL = _uriService.GetPolizaPaginationUri(filters, Url.RouteUrl(nameof(GetPolizas))).ToString(),
                PreviousPageURL = _uriService.GetPolizaPaginationUri(filters, Url.RouteUrl(nameof(GetPolizas))).ToString()
            };
            var response = new ApiResponse<IEnumerable<PolizaDTO>>(polizasDTO)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }

        // GET: api/Poliza/int
        //[Authorize(Roles = nameof(RoleType.Administrator))]
        [HttpGet("{int}")]
        public async Task<IActionResult> GetPoliza(int id)
        {
            var poliza = await _polizaService.GetPoliza(id);
            var polizaDTO= _mapper.Map<PolizaDTO>(poliza);
            return Ok(polizaDTO);
        }

        // POST: api/Poliza
        //[Authorize(Roles = nameof(RoleType.Administrator))]
        [HttpPost]
        public async Task<IActionResult> PostPoliza(PolizaDTO polizaDTO)
        {
            var poliza= _mapper.Map<Poliza>(polizaDTO);
            await _polizaService.InsertPoliza(poliza);
            var polizaDto=_mapper.Map<PolizaDTO>(poliza);
            return Ok(polizaDto);
            
        }

        // PUT: api/Poliza/int
        //[Authorize(Roles = nameof(RoleType.Administrator))]
        [HttpPut]
        public async Task<IActionResult> PutPoliza(int id, PolizaDTO polizaDTO)
        {
            var poliza = _mapper.Map<Poliza>(polizaDTO);
            poliza.Id = id;
            var result= await _polizaService.UpdatePoliza(poliza);
            return Ok(result);
        }


        // DELETE: api/Poliza/int
        //[Authorize(Roles = nameof(RoleType.Administrator))]
        [HttpDelete("{int}")]
        public async Task<IActionResult> DeletePoliza(int id)
        {
            var result = await _polizaService.DeletePoliza(id);
            return Ok(result);
        }

    }
}
