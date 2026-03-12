using Application.DTOs;
using Application.Interfaces.IRepos;
using Application.Interfaces.IServices;
using Application.Validators;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.Services
{
    public class TenantService : ITenantService
    {
        ITenantRepo _repo;
        INationalityRepo _nationalityRepo;

        public TenantService(ITenantRepo repo, INationalityRepo nationalityRepo)
        {
            _repo = repo;
            _nationalityRepo = nationalityRepo;
        }

        public async Task<long> AddAsync(TenantDto dto, CancellationToken ct = default)
        {
            ValidRequest(dto);

            long nationalId = 0;
            if (!string.IsNullOrWhiteSpace(dto.NationalName))
            {
                try
                {
                    nationalId = await _nationalityRepo.GetIdByNamAsync(dto.NationalName.Trim());
                    if (nationalId == 0)
                        throw new BusinessException("The selected nationality does not exist");
                }
                catch (Exception ex)
                {
                    throw new DataAccessException("Failed to create new data", innerException: ex);
                }
            }

            var entity = new Tenant
            {
                FullName = dto.FullName.Trim(),
                Phone = dto.Phone?.Trim() ?? null,
                Email = dto.Email?.Trim() ?? null,
                NationalId = nationalId == 0 ? null : nationalId,
            };

            try
            {
                return await _repo.AddAsync(entity, ct);
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Failed to create new data", innerException: ex);
            }
        }

        public async Task<bool> DeleteAsync(long id, CancellationToken ct = default)
        {
            try
            {
                return await _repo.DeleteAsync(id, ct);
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"Failed to delete data with Id [{id}].", innerException: ex);
            }
        }

        public async Task<TenantDto?> GetByIdAsync(long id, CancellationToken ct = default)
        {
            try
            {
                return await _repo.GetByIdAsync(id, ct);
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"Failed to fetch data with Id [{id}].", innerException: ex);
            }
        }

        public async Task<List<TenantDto>> GetListAsync(CancellationToken ct = default)
        {
            try
            {
                return await _repo.GetListAsync(ct: ct);
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"Failed to fetch all data.", innerException: ex);
            }
        }

        public async Task UpdateAsync(TenantDto dto, CancellationToken ct = default)
        {
            ValidRequest(dto);

            long nationalId = 0;
            if (!string.IsNullOrWhiteSpace(dto.NationalName))
            {
                try
                {
                    nationalId = await _nationalityRepo.GetIdByNamAsync(dto.NationalName.Trim());
                    if (nationalId == 0)
                        throw new BusinessException("Failed to get nationality");
                }
                catch (Exception ex)
                {
                    throw new DataAccessException("The selected nationality does not exist", innerException: ex);
                }
            }

            var entity = await _repo.GetRowByIdAsync(dto.Id, ct);
            if (entity == null)
                throw new BusinessException($"Data not found while update with Id: {dto.Id}");

            entity.FullName = dto.FullName.Trim();
            entity.Phone = dto.Phone?.Trim() ?? null;
            entity.Email = dto.Email?.Trim() ?? null;
            entity.NationalId = nationalId == 0 ? null : nationalId;

            try
            {
                await _repo.UpdateAsync(entity, ct);
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"Failed to update the data with Id [{dto.Id}].", innerException: ex);
            }
        }

        void ValidRequest(TenantDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FullName))
                throw new BusinessException($"FullName is required. sent FullName is null, empty or spaces -> [{dto.FullName}].");
        }
    }
}
