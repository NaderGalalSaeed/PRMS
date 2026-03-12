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
    public class LeaseService : ILeaseService
    {
        ILeaseRepo _repo;
        IPropertyRepo _propertyRepo;
        IPropertyService _propertyService;
        ITenantRepo _tenantRepo;

        public LeaseService(ILeaseRepo repo, IPropertyRepo propertyRepo,
            ITenantRepo tenantRepo, IPropertyService propertyService)
        {
            _repo = repo;
            _propertyRepo = propertyRepo;
            _tenantRepo = tenantRepo;
            _propertyService = propertyService;
        }

        public async Task<long> AddAsync(LeaseDto dto, CancellationToken ct = default)
        {
            ValidRequest(dto);

            long prppertyId = 0;
            long tenantId = 0;
            
            try
            {
                prppertyId = await _propertyRepo.GetIdByNamAsync(dto.PropertyName.Trim());
                tenantId = await _tenantRepo.GetIdByNamAsync(dto.TenantName.Trim());

                if (prppertyId == 0)
                    throw new BusinessException("The selected prpperty does not exist");

                if (tenantId == 0)
                    throw new BusinessException("The selected tenant does not exist");

                var IsOverlapping =  await _repo.CheckOverlapping(prppertyId, dto.StartDate, dto.EndDate, ct);
                if (IsOverlapping)
                    throw new BusinessException("There is overlapping");

                await _propertyService.LeasePropertyAsync(prppertyId, ct);
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Failed to create new data", innerException: ex);
            }

            var entity = new Lease
            {
                PropertyId = prppertyId,
                TenantId = tenantId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                MonthlyPrice = dto.MonthlyPrice,
            };

            try
            {
                var leaseId = await _repo.AddAsync(entity, ct);

                return leaseId;
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

        public async Task EndLeaseAsync(long id, CancellationToken ct = default)
        {
            var row = await _repo.GetRowByIdAsync(id, ct);
            if (row is null)    
                throw new DataAccessException("Faild across end lease", new Exception());

            await _propertyService.MakeProprttyAvailable(row.PropertyId, ct);
        }

        public async Task<LeaseDto?> GetByIdAsync(long id, CancellationToken ct = default)
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

        public async Task<List<LeaseDto>> GetLeasesOfProperty(long propertyId, CancellationToken ct = default)
        {
            return await _repo.GetLeasesOfProperty(propertyId, ct);
        }

        public async Task<List<LeaseDto>> GetListAsync(CancellationToken ct = default)
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

        public async Task UpdateAsync(LeaseDto dto, CancellationToken ct = default)
        {
            ValidRequest(dto);

            long prppertyId = 0;
            long tenantId = 0;

            try
            {
                prppertyId = await _propertyRepo.GetIdByNamAsync(dto.PropertyName.Trim());
                tenantId = await _tenantRepo.GetIdByNamAsync(dto.TenantName.Trim());

                if (prppertyId == 0)
                    throw new BusinessException("The selected prpperty does not exist");

                if (tenantId == 0)
                    throw new BusinessException("The selected tenant does not exist");
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Failed to create new data", innerException: ex);
            }


            var entity = await _repo.GetRowByIdAsync(dto.Id, ct);
            if (entity == null)
                throw new BusinessException($"Data not found while update with Id: {dto.Id}");

            entity.PropertyId = prppertyId;
            entity.TenantId = tenantId;
            entity.StartDate = dto.StartDate;
            entity.EndDate = dto.EndDate;
            entity.MonthlyPrice = dto.MonthlyPrice;

            try
            {
                await _repo.UpdateAsync(entity, ct);
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"Failed to update the data with Id [{dto.Id}].", innerException: ex);
            }
        }

        void ValidRequest(LeaseDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.PropertyName))
                throw new BusinessException($"PropertyName is required. sent PropertyName is null, empty or spaces -> [{dto.PropertyName}].");
            if (string.IsNullOrWhiteSpace(dto.TenantName))
                throw new BusinessException($"TenantName is required. sent TenantName is null, empty or spaces -> [{dto.TenantName}].");
            if (string.IsNullOrWhiteSpace(dto.MonthlyPrice.ToString()) || dto.MonthlyPrice <= 0)
                throw new BusinessException($"MonthlyPrice is required. sent MonthlyPrice must be larger than zero -> [{dto.MonthlyPrice}].");
            if (dto.StartDate > dto.EndDate)
                throw new BusinessException($"StartDate must be less than EndDate.");
        }
    }
}
