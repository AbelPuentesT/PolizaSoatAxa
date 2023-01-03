using AutoMapper;
using PolizaSOAT.Core.DTOs;
using PolizaSOAT.Core.Entities;

namespace PolizaSOAT.Infrastructure.Mapping
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Poliza, PolizaDTO>().ReverseMap();
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            CreateMap<CiudadVenta, CiudadVentaDTO>().ReverseMap();
            CreateMap<Security, SecurityDTO>().ReverseMap();
        }
    }
}
