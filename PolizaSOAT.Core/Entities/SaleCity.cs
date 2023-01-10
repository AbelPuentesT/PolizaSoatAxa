namespace PolizaSOAT.Core.Entities
{
    public class SaleCity : BaseEntity
    {
        public string City { get; set; } = null!;

        public virtual ICollection<Policy> Policies { get; } = new List<Policy>();
    }
}
