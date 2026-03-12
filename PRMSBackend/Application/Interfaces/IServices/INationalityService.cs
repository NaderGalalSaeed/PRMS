using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface INationalityService
    {
        Task<List<NationalityDto>> GetListAsync(CancellationToken ct = default);

        Task<NationalityDto?> GetByIdAsync(long id, CancellationToken ct = default);

        Task<long> AddAsync(NationalityDto dto, CancellationToken ct = default);

        Task UpdateAsync(NationalityDto dto, CancellationToken ct = default);

        Task<bool> DeleteAsync(long id, CancellationToken ct = default);
    }
}
