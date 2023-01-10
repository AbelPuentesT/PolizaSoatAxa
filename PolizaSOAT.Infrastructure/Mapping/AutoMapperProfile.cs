using AutoMapper;
using PolizaSOAT.Core.DTOs;
using PolizaSOAT.Core.Entities;

namespace PolizaSOAT.Infrastructure.Mapping
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Policy, PolicyDTO>().ReverseMap();
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<SaleCity, SaleCityDTO>().ReverseMap();
            CreateMap<Security, SecurityDTO>().ReverseMap();
        }
    }
}
