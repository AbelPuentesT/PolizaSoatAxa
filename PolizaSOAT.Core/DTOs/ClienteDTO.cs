using PolizaSOAT.Core.Entities;
using PolizaSOAT.Core.Enumerations;

namespace PolizaSOAT.Core.DTOs
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        public string CliIdentificacion { get; set; } = null!;

        public string CliApellido1 { get; set; } = null!;

        public string CliApellido2 { get; set; } = null!;

        public string CliNombre1 { get; set; } = null!;

        public string CliNombre2 { get; set; } = null!;

        public string CliDireccion { get; set; } = null!;

        public string CliCiudad { get; set; } = null!;

        public string CliCelular { get; set; } = null!;

        public string CliEmail { get; set; } = null!;

    }
}
