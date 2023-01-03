namespace PolizaSOAT.Core.Entities
{
    public class CiudadVenta : BaseEntity
    {
        public string CiuCiudad { get; set; } = null!;

        public virtual ICollection<Poliza> Polizas { get; } = new List<Poliza>();
    }
}
