using PolizaSOAT.Core.Enumerations;

namespace PolizaSOAT.Core.Entities;

public partial class Cliente : BaseEntity
{

    public string CliIdentificacion { get; set; } = null!;

    public string CliApellido1 { get; set; } = null!;

    public string CliApellido2 { get; set; } = null!;

    public string CliNombre1 { get; set; } = null!;

    public string CliNombre2 { get; set; } = null!;

    public string CliDireccion { get; set; } = null!;

    public string CliCiudad { get; set; } = null!;

    public string CliCelular { get; set; } = null!;

    public string CliEmail { get; set; } = null!;

    public virtual ICollection<Poliza> Polizas { get; } = new List<Poliza>();
}
