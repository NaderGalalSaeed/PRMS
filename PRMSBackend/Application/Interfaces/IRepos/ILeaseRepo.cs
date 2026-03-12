using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepos
{
    public interface ILeaseRepo
    {
        Task<List<LeaseDto>> GetListAsync(CancellationToken ct = default);
        Task<LeaseDto?> GetByIdAsync(long id, CancellationToken ct = default);
        Task<Lease?> GetRowByIdAsync(long id, CancellationToken ct = default);
        Task<long> AddAsync(Lease entity, CancellationToken ct = default);
        Task UpdateAsync(Lease entity, CancellationToken ct = default);
        Task<bool> DeleteAsync(long id, CancellationToken ct = default);
        Task<bool> CheckOverlapping(long prppertyId, DateTime startDate, DateTime endDate, CancellationToken ct = default);

        Task<List<LeaseDto>> GetLeasesOfProperty(long propertyId, CancellationToken ct = default);
    }
}
