using PolizaSOAT.Core.Entities;

namespace PolizaSOAT.Core.DTOs
{
    public class PolicyDTO: BaseEntity
    {
        public DateTime StartDate { get; set; }

        public DateTime FinalDate { get; set; }

        public DateTime PolicyEndDate { get; set; }

        public string VehiclePlate { get; set; } = null!;

        public int IdCustomer { get; set; }

        public int IdCity { get; set; }
    }
}
