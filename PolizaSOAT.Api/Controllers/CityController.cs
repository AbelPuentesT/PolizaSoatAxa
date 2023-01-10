using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nest;
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
    [Route("api/Cities")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        public CityController(ICityService cityService, IMapper mapper, IUriService uriService)
        {
            _cityService = cityService;
            _mapper = mapper;
            _uriService = uriService;
        }
        
        [HttpGet(Name = nameof(GetCities))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<SaleCityDTO>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<SaleCityDTO>>))]

        public async Task<IActionResult> GetCities([FromQuery]CityQueryFilters filters)
        {
            var cities=  _cityService.GetAllCities(filters);
            var citiesDTO = _mapper.Map<IEnumerable<SaleCityDTO>>(cities);
            var metadata = Metadata.Create(
                cities.CurrentPage,
                cities.TotalPages,
                cities.PageSize,
                cities.TotalCount,
                cities.HasPreviousPage,
                cities.HasNextPage,
                _uriService.GetCitiesPaginationUri(filters, Url.RouteUrl(nameof(GetCities))).ToString(),
                _uriService.GetCitiesPaginationUri(filters, Url.RouteUrl(nameof(GetCities))).ToString()
            );
            var response = ApiResponse<IEnumerable<SaleCityDTO>>.Create(citiesDTO,metadata);
            Response.Headers.Add("Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }

        // GET: api/Cities/int
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCity(int id)
        {
            var city= await _cityService.GetCity(id);
            var cityDTO = _mapper.Map<SaleCityDTO>(city);
            var response = ApiResponse<SaleCityDTO>.Create(cityDTO);
            return Ok(response);
        }

        // POST: api/Cities
        [HttpPost]
        public async Task<IActionResult> PostCity(SaleCityDTO cityDTO)
        {
            var city = _mapper.Map<SaleCity>(cityDTO);
            await _cityService.InsertCity(city);
            var cityDto = _mapper.Map<SaleCityDTO>(city);
            var response = ApiResponse<SaleCityDTO>.Create(cityDto);
            return Ok(response);
        }
        
        // PUT: api/Cities/int
        [HttpPut]
        public async Task<IActionResult> PutCity(int id, SaleCityDTO cityDTO)
        {
            var city = _mapper.Map<SaleCity>(cityDTO);
            city.Id = id;
            var result= await _cityService.UpdateCity(city);
            var response = ApiResponse<bool>.Create(result);
            return Ok(response);
        }

        // DELETE: api/Cities/int
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var result= await _cityService.DeleteCity(id);
            var response = ApiResponse<bool>.Create(result);
            return Ok(response);
        }
    }
}
