using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepos
{
    public interface IPropertyRepo
    {
        Task<List<PropertyDto>> GetListAsync(CancellationToken ct = default);
        Task<PropertyDto?> GetByIdAsync(long id, CancellationToken ct = default);
        Task<Property?> GetRowByIdAsync(long id, CancellationToken ct = default);
        Task<long> AddAsync(Property entity, CancellationToken ct = default);
        Task UpdateAsync(Property entity, CancellationToken ct = default);
        Task<bool> DeleteAsync(long id, CancellationToken ct = default);
        Task<long> GetIdByNamAsync(string name, CancellationToken ct = default);
    }
}
