namespace PolizaSOAT.Core.Entities;

public partial class Poliza : BaseEntity
{

    public DateTime PolFechaInicio { get; set; }

    public DateTime PolFechaFin { get; set; }

    public DateTime PolFechaVencimiento { get; set; }

    public string PolPlacaAutomotor { get; set; }

    public int CliId { get; set; }

    public int CiuId { get; set; }

    public virtual CiudadVenta Ciu { get; set; } = null!;

    public virtual Cliente Cli { get; set; } = null!;
}
