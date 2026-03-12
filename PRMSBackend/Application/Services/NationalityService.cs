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
    public class NationalityService : INationalityService
    {
        INationalityRepo _repo;

        public NationalityService(INationalityRepo repo)
        {
            _repo = repo;
        }

        public async Task<long> AddAsync(NationalityDto dto, CancellationToken ct = default)
        {
            ValidRequest(dto);

            var entity = new Nationality
            {
                Name = dto.Name.Trim(),
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

        public async Task<NationalityDto?> GetByIdAsync(long id, CancellationToken ct = default)
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

        public async Task<List<NationalityDto>> GetListAsync(CancellationToken ct = default)
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

        public async Task UpdateAsync(NationalityDto dto, CancellationToken ct = default)
        {
            ValidRequest(dto);

            var entity = await _repo.GetRowByIdAsync(dto.Id, ct);
            if (entity == null)
                throw new BusinessException($"Data not found while update with Id: {dto.Id}");

            entity.Name = dto.Name.Trim();

            try
            {
                await _repo.UpdateAsync(entity, ct);
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"Failed to update data with Id [{dto.Id}].", innerException: ex);
            }
        }

        void ValidRequest(NationalityDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new BusinessException($"Name is required. sent Name is null, empty or spaces -> [{dto.Name}].");
        }
    }
}
