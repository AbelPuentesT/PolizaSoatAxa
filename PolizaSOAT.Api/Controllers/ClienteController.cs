using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PolizaSOAT.Core.CustomEntities;
using PolizaSOAT.Core.DTOs;
using PolizaSOAT.Core.Entities;
using PolizaSOAT.Core.Interfaces;
using PolizaSOAT.Core.QueryFilters;
using PolizaSOAT.Infrastructure.Interfaces;
using PolizaSOAT.Responses;
using System.Net;

namespace PolizaSOAT.Api.Controllers
{
    //[Authorize(Roles = nameof(RoleType.Administrator))]
    [Route("api/Cliente")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        public ClienteController(IClienteService clienteService, IMapper mapper, IUriService uriService)
        {
            _clienteService= clienteService;
            _mapper= mapper;
            _uriService = uriService;
        }
        // GET: api/Cliente
        
        [HttpGet(Name = nameof(GetClientes))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<CiudadVentaDTO>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task <ActionResult> GetClientes([FromQuery] ClienteQueryFilters filters)
        {
            var clientes = _clienteService.GetAllClientes(filters);
            var clientesDTO = _mapper.Map<IEnumerable<ClienteDTO>>(clientes);
            var metadata = new Metadata
            {
                TotalCount = clientes.TotalCount,
                TotalPages = clientes.TotalPages,
                HasNextPage = clientes.HasNextPage,
                HasPreviousPage = clientes.HasPreviousPage,
                CurrentPage = clientes.CurrentPage,
                PageSize = clientes.PageSize,
                NextPageURL = _uriService.GetClientesPaginationUri(filters, Url.RouteUrl(nameof(GetClientes))).ToString(),
                PreviousPageURL = _uriService.GetClientesPaginationUri(filters, Url.RouteUrl(nameof(GetClientes))).ToString()
            };
            var response = new ApiResponse<IEnumerable<ClienteDTO>>(clientesDTO)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }

        // GET: api/Cliente/int
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _clienteService.GetCliente(id);
            var clienteDTO = _mapper.Map<ClienteDTO>(cliente);
            return Ok(clienteDTO);
        }

        // POST: api/Cliente
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente clienteDTO)
        {
            var cliente = _mapper.Map<Cliente>(clienteDTO);
            await _clienteService.InsertCliente(cliente);
            var polizaDto = _mapper.Map<ClienteDTO>(cliente);
            return Ok(polizaDto);
        }

        // PUT: api/Cliente/int
        [HttpPut]
        public async Task<IActionResult> PutCliente(int id, Cliente clienteDTO)
        {
            var cliente = _mapper.Map<Cliente>(clienteDTO);
            cliente.Id= id;
            var result = await _clienteService.UpdateCliente(cliente);
            return Ok(result);
        }

        // DELETE: api/Cliente/int
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var result = await _clienteService.DeleteCliente(id);
            return Ok(result);
        }
    }
}
