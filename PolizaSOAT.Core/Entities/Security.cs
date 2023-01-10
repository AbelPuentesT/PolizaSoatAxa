using PolizaSOAT.Core.Enumerations;

namespace PolizaSOAT.Core.Entities
{
    public class Security: BaseEntity
    {
        public string User { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; }= null!;
        public RoleType Rol { get; set; }
    }
}
