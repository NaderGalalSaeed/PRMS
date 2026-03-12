using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface IPropertyService
    {
        Task<List<PropertyDto>> GetListAsync(CancellationToken ct = default);

        Task<PropertyDto?> GetByIdAsync(long id, CancellationToken ct = default);

        Task<long> AddAsync(PropertyDto dto, CancellationToken ct = default);

        Task UpdateAsync(PropertyDto dto, CancellationToken ct = default);

        Task<bool> DeleteAsync(long id, CancellationToken ct = default);

        Task LeasePropertyAsync(long id, CancellationToken ct = default);

        Task MakeProprttyAvailable(long id, CancellationToken ct = default);
    }
}
