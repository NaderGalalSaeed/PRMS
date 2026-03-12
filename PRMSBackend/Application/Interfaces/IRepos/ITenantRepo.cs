using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepos
{
    public interface ITenantRepo
    {
        Task<List<TenantDto>> GetListAsync(CancellationToken ct = default);
        Task<TenantDto?> GetByIdAsync(long id, CancellationToken ct = default);
        Task<Tenant?> GetRowByIdAsync(long id, CancellationToken ct = default);
        Task<long> AddAsync(Tenant entity, CancellationToken ct = default);
        Task UpdateAsync(Tenant entity, CancellationToken ct = default);
        Task<bool> DeleteAsync(long id, CancellationToken ct = default);

        Task<long> GetIdByNamAsync(string name, CancellationToken ct = default);
    }
}
