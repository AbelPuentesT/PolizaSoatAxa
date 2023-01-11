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

    [Route("api/Policy")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private readonly IPolicyService _policySoatService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        public PolicyController(IPolicyService policySoatService, IMapper mapper, IUriService uriService)
        {
            _policySoatService = policySoatService;
            _mapper = mapper;
            _uriService = uriService;

        }

        // GET: api/Policy
        //[Authorize]
        [HttpGet(Name = nameof(GetPolicies))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<PolicyDTO>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<PolicyDTO>>))]
        public async Task<IActionResult> GetPolicies([FromQuery] PolicyQueryFilters filters)
        {
            var policies = _policySoatService.GetAllPolicies(filters);
            var policiesDTO = _mapper.Map<IEnumerable<PolicyDTO>>(policies);
            var metadata = Metadata.Create(
                policies.CurrentPage,
                policies.TotalPages,
                policies.PageSize,
                policies.TotalCount,
                policies.HasPreviousPage,
                policies.HasNextPage,
                _uriService.GetPolicyPaginationUri(filters, Url.RouteUrl(nameof(GetPolicies))).ToString(),
                _uriService.GetPolicyPaginationUri(filters, Url.RouteUrl(nameof(GetPolicies))).ToString()
            );
            var response = ApiResponse<IEnumerable<PolicyDTO>>.Create(policiesDTO, metadata);
            Response.Headers.Add("Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }

        // GET: api/Policy/int
        //[Authorize(Roles = nameof(RoleType.Administrator))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPolicy(int id)
        {
            var policy = await _policySoatService.GetPolicy(id);
            var policyDTO = _mapper.Map<PolicyDTO>(policy);
            var response = ApiResponse<PolicyDTO>.Create(policyDTO);
            return Ok(response);
        }

        // POST: api/Policy
        //[Authorize(Roles = nameof(RoleType.Administrator))]
        [HttpPost]
        public async Task<IActionResult> PostPolicy(PolicyDTO policyDTO)
        {
            var policy = _mapper.Map<Policy>(policyDTO);
            await _policySoatService.InsertPolicy(policy);
            var policyDto = _mapper.Map<PolicyDTO>(policy);
            var response = ApiResponse<PolicyDTO>.Create(policyDto);
            return Ok(response);

        }

        // PUT: api/Policy/int
        //[Authorize(Roles = nameof(RoleType.Administrator))]
        [HttpPut]
        public async Task<IActionResult> PutPolicy(int id, PolicyDTO policyDTO)
        {
            var policy = _mapper.Map<Policy>(policyDTO);
            policy.Id = id;
            var result = await _policySoatService.UpdatePolicy(policy);
            var response = ApiResponse<bool>.Create(result);
            return Ok(response);
        }


        // DELETE: api/Policy/int
        //[Authorize(Roles = nameof(RoleType.Administrator))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePolicy(int id)
        {
            var result = await _policySoatService.DeletePolicy(id);
            var response = ApiResponse<bool>.Create(result);
            return Ok(response);
        }

    }
}
