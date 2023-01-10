namespace PolizaSOAT.Core.Entities;

public partial class Policy : BaseEntity
{

    public DateTime StartDate { get; set; }

    public DateTime FinalDate { get; set; }

    public DateTime PolicyEndDate { get; set; }

    public string VehiclePlate { get; set; } =null!;

    public int IdCustomer { get; set; }

    public int IdCity { get; set; }

    public virtual SaleCity City { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;
}
