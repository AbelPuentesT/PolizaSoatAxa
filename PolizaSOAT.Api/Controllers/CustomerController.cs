using AutoMapper;
using Elasticsearch.Net;
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
    //[Authorize(Roles = nameof(RoleType.Administrator))]
    [Route("api/Customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        public CustomerController(ICustomerService customerService, IMapper mapper, IUriService uriService)
        {
            _customerService= customerService;
            _mapper= mapper;
            _uriService = uriService;
        }
        
        // GET: api/Customer
        [HttpGet(Name = nameof(GetCustomers))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<Customer>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<Customer>>))]

        public async Task <IActionResult> GetCustomers([FromQuery] CustomerQueryFilters filters)
        {
            var customers = _customerService.GetAllCustomers(filters);
            var customersDTO = _mapper.Map<IEnumerable<CustomerDTO>>(customers);
            var metadata = Metadata.Create(
                customers.CurrentPage,
                customers.TotalPages,
                customers.PageSize,
                customers.TotalCount,
                customers.HasPreviousPage,
                customers.HasNextPage,
                _uriService.GetCustomersPaginationUri(filters, Url.RouteUrl(nameof(GetCustomers))).ToString(),
                _uriService.GetCustomersPaginationUri(filters, Url.RouteUrl(nameof(GetCustomers))).ToString()
                );
            var response = ApiResponse<IEnumerable<CustomerDTO>>.Create(customersDTO,metadata);
            Response.Headers.Add("Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }

        // GET: api/Customer/int
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _customerService.GetCustomer(id);
            var customerDTO = _mapper.Map<CustomerDTO>(customer);
            var response = ApiResponse<CustomerDTO>.Create(customerDTO);
            return Ok(response);
        }

        // POST: api/Customer
        [HttpPost]
        public async Task<IActionResult> PostCustomer(CustomerDTO customerDTO)
        {
            var customer = _mapper.Map<Customer>(customerDTO);
            await _customerService.InsertCustomer(customer);
            var customerDto = _mapper.Map<CustomerDTO>(customer);
            var response = ApiResponse<CustomerDTO>.Create(customerDto);
            return Ok(response);
        }

        // PUT: api/Customer/int
        [HttpPut]
        public async Task<IActionResult> PutCustomer(int id, CustomerDTO customerDTO)
        {
            var customer = _mapper.Map<Customer>(customerDTO);
            customer.Id= id;
            var result = await _customerService.UpdateCustromer(customer);
            var response = ApiResponse<bool>.Create(result);
            return Ok(response);
        }

        // DELETE: api/Customer/int
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var result = await _customerService.DeleteCustomer(id);
            var response = ApiResponse<bool>.Create(result);
            return Ok(response);
        }
    }
}
