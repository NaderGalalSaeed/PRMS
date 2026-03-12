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
    public class PropertyService : IPropertyService
    {
        IPropertyRepo _repo;

        public PropertyService(IPropertyRepo repo)
        {
            _repo = repo;
        }

        public async Task<long> AddAsync(PropertyDto dto, CancellationToken ct = default)
        {
            ValidRequest(dto);

            DateTime date = DateTime.UtcNow;
            var entity = new Property
            {
                Name = dto.Name.Trim(),
                Address = dto.Address.Trim(),
                City = dto.City.Trim(),
                MonthlyPrice = dto.MonthlyPrice,
                IsAvailable = dto.IsAvailable,
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

        public async Task<PropertyDto?> GetByIdAsync(long id, CancellationToken ct = default)
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

        public async Task<List<PropertyDto>> GetListAsync(CancellationToken ct = default)
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

        public async Task LeasePropertyAsync(long id, CancellationToken ct = default)
        {
            var row = await _repo.GetRowByIdAsync(id, ct);
            if (row is null)
                throw new DataAccessException("Faild across lease property", new Exception());

            if (!row.IsAvailable)
                throw new BusinessException("Property not available");

            row.IsAvailable = false;
            await _repo.UpdateAsync(row);
        }

        public async Task MakeProprttyAvailable(long id, CancellationToken ct = default)
        {
            var row = await _repo.GetRowByIdAsync(id, ct);
            if (row is null)
                throw new DataAccessException("Faild across Make Proprtty Available", new Exception());

            row.IsAvailable = true;
            await _repo.UpdateAsync(row);
        }

        public async Task UpdateAsync(PropertyDto dto, CancellationToken ct = default)
        {
            ValidRequest(dto);

            var entity = await _repo.GetRowByIdAsync(dto.Id, ct);
            if (entity == null)
                throw new BusinessException($"Data not found while update with Id: {dto.Id}");

            entity.Name = dto.Name.Trim();
            entity.Address = dto.Address.Trim();
            entity.City = dto.City.Trim();
            entity.MonthlyPrice = dto.MonthlyPrice;
            entity.IsAvailable = dto.IsAvailable;
            //entity.IsAvailable //do not need to update it

            try
            {
                await _repo.UpdateAsync(entity, ct);
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"Failed to update the data with Id [{dto.Id}].", innerException: ex);
            }
        }

        void ValidRequest(PropertyDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new BusinessException($"Name is required. sent Name is null, empty or spaces -> [{dto.Name}].");
            if (string.IsNullOrWhiteSpace(dto.Address))
                throw new BusinessException($"Address is required. sent Address is null, empty or spaces -> [{dto.Address}].");
            if (string.IsNullOrWhiteSpace(dto.City))
                throw new BusinessException($"City is required. sent City is null, empty or spaces -> [{dto.City}].");
            if (string.IsNullOrWhiteSpace(dto.MonthlyPrice.ToString()) || dto.MonthlyPrice <= 0)
                throw new BusinessException($"MonthlyPrice is required. sent MonthlyPrice must be larger than zero -> [{dto.MonthlyPrice}].");
        }
    }
}
