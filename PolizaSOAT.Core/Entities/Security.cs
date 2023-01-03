using PolizaSOAT.Core.Enumerations;

namespace PolizaSOAT.Core.Entities
{
    public class Security: BaseEntity
    {
        public string User { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public RoleType Rol { get; set; }
    }
}
