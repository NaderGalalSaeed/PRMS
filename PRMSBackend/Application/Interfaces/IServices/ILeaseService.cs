using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface ILeaseService
    {
        Task<List<LeaseDto>> GetListAsync(CancellationToken ct = default);

        Task<LeaseDto?> GetByIdAsync(long id, CancellationToken ct = default);

        Task<long> AddAsync(LeaseDto dto, CancellationToken ct = default);

        Task UpdateAsync(LeaseDto dto, CancellationToken ct = default);

        Task<bool> DeleteAsync(long id, CancellationToken ct = default);

        Task EndLeaseAsync(long id, CancellationToken ct = default);

        Task<List<LeaseDto>> GetLeasesOfProperty(long propertyId, CancellationToken ct = default);
    }
}
