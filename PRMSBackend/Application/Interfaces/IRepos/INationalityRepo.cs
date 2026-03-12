using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepos
{
    public interface INationalityRepo
    {
        Task<List<NationalityDto>> GetListAsync(CancellationToken ct = default);
        Task<NationalityDto?> GetByIdAsync(long id, CancellationToken ct = default);
        Task<Nationality?> GetRowByIdAsync(long id, CancellationToken ct = default);
        Task<long> AddAsync(Nationality entity, CancellationToken ct = default);
        Task UpdateAsync(Nationality entity, CancellationToken ct = default);
        Task<bool> DeleteAsync(long id, CancellationToken ct = default);


        Task<long> GetIdByNamAsync(string name, CancellationToken ct = default);
    }
}
