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
    public class TenantRepo : ITenantRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Tenant> _dbSet;

        public TenantRepo(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Tenant>();
        }

        public async Task<TenantDto?> GetByIdAsync(long id, CancellationToken ct = default)
        {
            return await _dbSet.Where(a => a.Id == id).Select(a => new TenantDto
            {
                Id = a.Id,
                FullName = a.FullName,
                Phone = a.Phone,
                Email = a.Email,
                NationalName = a.National.Name ?? default,
            }).FirstOrDefaultAsync(ct);
        }

        public async Task<long> AddAsync(Tenant entity, CancellationToken ct = default)
        {
            await _dbSet.AddAsync(entity, ct);
            await SaveChangesAsync(ct);
            return entity.Id;
        }

        public async Task UpdateAsync(Tenant entity, CancellationToken ct = default)
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

        public async Task<List<TenantDto>> GetListAsync(CancellationToken ct = default)
        {
            return await _dbSet.Select(a => new TenantDto
            {
                Id = a.Id,
                FullName = a.FullName,
                Phone = a.Phone,
                Email = a.Email,
                NationalName = a.National.Name ?? default,
            }).ToListAsync(ct);
        }

        private async Task SaveChangesAsync(CancellationToken ct = default)
        {
            await _context.SaveChangesAsync(ct);
        }

        public async Task<Tenant?> GetRowByIdAsync(long id, CancellationToken ct = default)
        {
            return await _dbSet.Where(a => a.Id == id).FirstOrDefaultAsync(ct);
        }

        public async Task<long> GetIdByNamAsync(string name, CancellationToken ct = default)
        {
            return await _dbSet.Where(a => a.FullName == name).Select(a => a.Id).FirstOrDefaultAsync(ct);
        }

    }
}
