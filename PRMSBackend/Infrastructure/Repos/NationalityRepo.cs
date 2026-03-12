using Application.DTOs;
using Application.Interfaces.IRepos;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repos
{
    public class NationalityRepo : INationalityRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Nationality> _dbSet;

        public NationalityRepo(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Nationality>();
        }

        public async Task<NationalityDto?> GetByIdAsync(long id, CancellationToken ct = default)
        {
            return await _dbSet.Where(a => a.Id == id).Select(a => new NationalityDto
            {
                Id = a.Id,
                Name = a.Name,
            }).FirstOrDefaultAsync(ct);
        }

        public async Task<long> AddAsync(Nationality entity, CancellationToken ct = default)
        {
            await _dbSet.AddAsync(entity, ct);
            await SaveChangesAsync(ct);
            return entity.Id;
        }

        public async Task UpdateAsync(Nationality entity, CancellationToken ct = default)
        {
            _dbSet.Update(entity);
            await SaveChangesAsync(ct);
        }

        public async Task<bool> DeleteAsync(long id, CancellationToken ct = default)
        {
            var entity = await _dbSet.Where(a => a.Id == id).FirstOrDefaultAsync(ct);
            if (entity == null)
                return false;

            _dbSet.Remove(entity);
            await SaveChangesAsync(ct);
            return true;
        }

        public async Task<List<NationalityDto>> GetListAsync(CancellationToken ct = default)
        {
            return await _dbSet.Select(a => new NationalityDto
            {
                Id = a.Id,
                Name = a.Name,
            }).ToListAsync(ct);
        }

        private async Task SaveChangesAsync(CancellationToken ct = default)
        {
            await _context.SaveChangesAsync(ct);
        }

        public async Task<long> GetIdByNamAsync(string name, CancellationToken ct = default)
        {
            return await _dbSet.Where(a => a.Name == name).Select(a => a.Id).FirstOrDefaultAsync(ct);
        }

        public async Task<Nationality?> GetRowByIdAsync(long id, CancellationToken ct = default)
        {
            return await _dbSet.Where(a => a.Id == id).FirstOrDefaultAsync(ct);
        }
    }
}
