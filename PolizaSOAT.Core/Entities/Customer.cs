namespace PolizaSOAT.Core.Entities;

public partial class Customer : BaseEntity
{

    public string IdCustomer { get; set; } =null!;

    public string FirstLastName { get; set; } = null!;

    public string SecondLastName { get; set; } = null!; 

    public string FirstName { get; set; } = null!; 

    public string SecondName { get; set; } = null!; 

    public string Address { get; set; } = null!; 

    public string City { get; set; } = null!; 

    public string Phone { get; set; } = null!; 

    public string Email { get; set; } = null!; 

    public virtual ICollection<Policy> Policies { get; } = new List<Policy>();
}
