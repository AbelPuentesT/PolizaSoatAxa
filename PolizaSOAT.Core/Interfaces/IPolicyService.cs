using PolizaSOAT.Core.CustomEntities;
using PolizaSOAT.Core.Entities;
using PolizaSOAT.Core.QueryFilters;

namespace PolizaSOAT.Core.Interfaces
{
    public interface IPolicyService
    {
        PagedList<Policy> GetAllPolicies(PolicyQueryFilters filters);
        Task<Policy> GetPolicy(int id);
        Task InsertPolicy(Policy poliza);
        Task<bool> UpdatePolicy(Policy poliza);
        Task<bool> DeletePolicy(int id);
    }
}