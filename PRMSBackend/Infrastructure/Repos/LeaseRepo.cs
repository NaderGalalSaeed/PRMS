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
    public class LeaseRepo : ILeaseRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Lease> _dbSet;

        public LeaseRepo(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Lease>();
        }

        public async Task<long> AddAsync(Lease entity, CancellationToken ct = default)
        {
            await _dbSet.AddAsync(entity, ct);
            await SaveChangesAsync(ct);
            return entity.Id;
        }

        public async Task UpdateAsync(Lease entity, CancellationToken ct = default)
        {
            _dbSet.Update(entity);
            await SaveChangesAsync(ct);
        }

        public async Task<List<LeaseDto>> GetListAsync(CancellationToken ct = default)
        {
            return await _dbSet.Select(a => new LeaseDto
            {
                Id = a.Id,
                PropertyName = a.Property.Name,
                TenantName = a.Tenant.FullName,
                StartDate = a.StartDate,
                EndDate = a.EndDate,
                MonthlyPrice = a.MonthlyPrice,
            }).ToListAsync(ct);
        }

        public async Task<LeaseDto?> GetByIdAsync(long id, CancellationToken ct = default)
        {
            return await _dbSet.Where(a => a.Id == id).Select(a => new LeaseDto
            {
                Id = a.Id,
                PropertyName = a.Property.Name,
                TenantName = a.Tenant.FullName,
                StartDate = a.StartDate,
                EndDate = a.EndDate,
                MonthlyPrice = a.MonthlyPrice,
            }).FirstOrDefaultAsync(ct);
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

        public async Task<Lease?> GetRowByIdAsync(long id, CancellationToken ct = default)
        {
            return await _dbSet.Where(a => a.Id == id).FirstOrDefaultAsync(ct);
        }

        private async Task SaveChangesAsync(CancellationToken ct = default)
        {
            await _context.SaveChangesAsync(ct);
        }

        public async Task<List<LeaseDto>> GetLeasesOfProperty(long propertyId, CancellationToken ct = default)
        {
            return await _dbSet.Where(a => a.PropertyId == propertyId).Select(a => new LeaseDto
            {
                Id = a.Id,
                PropertyName = a.Property.Name,
                TenantName = a.Tenant.FullName,
                StartDate = a.StartDate,
                EndDate = a.EndDate,
                MonthlyPrice = a.MonthlyPrice,
            }).ToListAsync(ct);
        }

        public Task<bool> CheckOverlapping(long prppertyId, DateTime startDate, DateTime endDate, CancellationToken ct = default)
        {
            return _dbSet.AnyAsync(a => a.PropertyId == prppertyId
            && ( (startDate <= a.StartDate  && endDate <= a.EndDate) || (startDate <= a.EndDate && endDate >= a.EndDate)
            || (startDate >= a.StartDate && endDate <= a.EndDate)), ct);
        }
    }
}
