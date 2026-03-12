using Application.DTOs;
using Application.Interfaces.IRepos;
using Application.Validators;
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
    public class PropertyRepo : IPropertyRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Property> _dbSet;

        public PropertyRepo(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Property>();
        }

        public async Task<long> AddAsync(Property entity, CancellationToken ct = default)
        {
            await _dbSet.AddAsync(entity, ct);
            await SaveChangesAsync(ct);
            return entity.Id;
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

        public async Task<PropertyDto?> GetByIdAsync(long id, CancellationToken ct = default)
        {
            return await _dbSet.Where(a => a.Id == id).Select(a => new PropertyDto
            {
                Id = a.Id,
                Name = a.Name,
                Address = a.Address,
                City = a.City,
                MonthlyPrice = a.MonthlyPrice,
                IsAvailable = a.IsAvailable,
            }).FirstOrDefaultAsync(ct);
        }

        public async Task<List<PropertyDto>> GetListAsync(CancellationToken ct = default)
        {
            return await _dbSet.Select(a => new PropertyDto
            {
                Id = a.Id,
                Name = a.Name,
                Address = a.Address,
                City = a.City,
                MonthlyPrice = a.MonthlyPrice,
                IsAvailable = a.IsAvailable,
            }).ToListAsync(ct);
        }

        public async Task<Property?> GetRowByIdAsync(long id, CancellationToken ct = default)
        {
            return await _dbSet.Where(a => a.Id == id).FirstOrDefaultAsync(ct);
        }

        public async Task UpdateAsync(Property entity, CancellationToken ct = default)
        {
            _dbSet.Update(entity);
            await SaveChangesAsync(ct);
        }

        private async Task SaveChangesAsync(CancellationToken ct = default)
        {
            await _context.SaveChangesAsync(ct);
        }

        public async Task<long> GetIdByNamAsync(string name, CancellationToken ct = default)
        {
            return await _dbSet.Where(a => a.Name == name).Select(a => a.Id).FirstOrDefaultAsync(ct);
        }
    }
}
