using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface ITenantService
    {
        Task<List<TenantDto>> GetListAsync(CancellationToken ct = default);

        Task<TenantDto?> GetByIdAsync(long id, CancellationToken ct = default);

        Task<long> AddAsync(TenantDto dto, CancellationToken ct = default);

        Task UpdateAsync(TenantDto dto, CancellationToken ct = default);

        Task<bool> DeleteAsync(long id, CancellationToken ct = default);
    }
}
