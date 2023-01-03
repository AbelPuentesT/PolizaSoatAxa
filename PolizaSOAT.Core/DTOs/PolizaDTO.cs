using PolizaSOAT.Core.Entities;

namespace PolizaSOAT.Core.DTOs
{
    public class PolizaDTO
    {
        public int Id { get; set; }
        public DateTime PolFechaInicio { get; set; }

        public DateTime PolFechaFin { get; set; }

        public DateTime PolFechaVencimiento { get; set; }

        public string PolPlacaAutomotor { get; set; } 

        public int CliId { get; set; }

        public int CiuId { get; set; }
    }
}
