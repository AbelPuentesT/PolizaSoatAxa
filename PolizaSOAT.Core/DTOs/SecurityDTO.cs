using PolizaSOAT.Core.Enumerations;

namespace PolizaSOAT.Core.DTOs
{
    public class SecurityDTO
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public RoleType Rol { get; set; }
    }
}
