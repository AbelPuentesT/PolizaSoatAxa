using PolizaSOAT.Core.Entities;

namespace PolizaSOAT.Core.DTOs
{
    public class CustomerDTO: BaseEntity
    {

        public string IdCustomer { get; set; } = null!;

        public string FirstLastName { get; set; } = null!;

        public string SecondLastName { get; set; } 

        public string FirstName { get; set; } = null!; 

        public string SecondName { get; set; } 

        public string Address { get; set; } = null!; 

        public string City { get; set; } = null!; 

        public string Phone { get; set; } = null!; 

        public string Email { get; set; } = null!; 

    }
}
