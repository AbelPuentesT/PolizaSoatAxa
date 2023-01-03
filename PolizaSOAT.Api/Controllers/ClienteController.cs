using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolizaSOAT.Core.DTOs;
using PolizaSOAT.Core.Entities;
using PolizaSOAT.Core.Enumerations;
using PolizaSOAT.Core.Interfaces;
using System.Data;

namespace PolizaSOAT.Api.Controllers
{
    [Authorize(Roles = nameof(RoleType.Administrator))]
    [Route("api/Cliente")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;
        public ClienteController(IClienteService clienteService, IMapper mapper)
        {
            _clienteService= clienteService;
            _mapper= mapper;
        }
        // GET: api/Cliente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            var clientes = await _clienteService.GetAllClientes();
            var clientesDTO = _mapper.Map<IEnumerable<ClienteDTO>>(clientes);
            return Ok(clientesDTO);
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
