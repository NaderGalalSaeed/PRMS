
using API.Helper;
using Application.DTOs;
using Application.Interfaces.IServices;
using Domain.Entities;
using ERPClean.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route($"{APIService.EndPonit}/[controller]")]
    [ApiController]
    public class LeasessController : ControllerBase
    {
        readonly ILeaseService _svc;

        public LeasessController(ILeaseService svc)
        {
            _svc = svc;
        }


        /// <summary>
        /// Get all data.
        /// </summary>
        // GET: api/leases
        [HttpGet]
        [ProducesResponseType(typeof(APIResponse<IEnumerable<LeaseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken ct = default)
        {
            var data = await _svc.GetListAsync(ct) ?? Enumerable.Empty<LeaseDto>();
            return Ok(APIResponse<IEnumerable<LeaseDto>>.Success(data, "All data retrieved successfully."));
        }


        /// <summary>
        /// Get data by ID.
        /// </summary>`
        // GET: api/leases/{id}
        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(APIResponse<LeaseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse<string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(long id, CancellationToken ct = default)
        {
            var data = await _svc.GetByIdAsync(id, ct);
            if (data == null)
            {
                return NotFound(APIResponse<string>.Fail("Data not found."));
            }
            return Ok(APIResponse<LeaseDto>.Success(data!, "Data retrieved successfully."));
        }


        /// <summary>
        /// Create new data.
        /// </summary>
        // POST: api/leases
        [HttpPost]
        [ProducesResponseType(typeof(APIResponse<long>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(APIResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] LeaseDto dto, CancellationToken ct = default)
        {
            if (dto == null)
                return BadRequest(APIResponse<string>.Fail("Invalid request payload."));

            var id = await _svc.AddAsync(dto, ct);
            return CreatedAtAction(nameof(Get), new { id },
            APIResponse<long>.Success(id, "Data created successfully."));
        }


        /// <summary>
        /// Update existing data.
        /// </summary>
        // PUT: api/leases/{id}
        [HttpPut("{id:long}")]
        [ProducesResponseType(typeof(APIResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(APIResponse<string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(long id, [FromBody] LeaseDto dto, CancellationToken ct = default)
        {
            if (dto == null || id != dto.Id)
                return BadRequest(APIResponse<string>.Fail("Request ID mismatch or payload is invalid."));

            await _svc.UpdateAsync(dto, ct);
            return Ok(APIResponse<string>.Success(null, "Data updated successfully."));
        }


        /// <summary>
        /// Delete data.
        /// </summary>
        // DELETE: api/leases/{id}
        [HttpDelete("{id:long}")]
        [ProducesResponseType(typeof(APIResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse<string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long id, CancellationToken ct = default)
        {
            if (!await _svc.DeleteAsync(id, ct))
            {
                return NotFound(APIResponse<string>.Fail("Data not found."));
            }
            
            return Ok(APIResponse<string>.Success(null, "Data deleted successfully."));
        }


        /// <summary>
        /// Get leases of a property.
        /// </summary>`
        // GET: api/leases/property/{propertyId}
        [HttpGet("property/{propertyId:long}")]
        [ProducesResponseType(typeof(APIResponse<List<LeaseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse<string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLeasesOfProperty(long propertyId, CancellationToken ct = default)
        {
            var data = await _svc.GetLeasesOfProperty(propertyId, ct);
            if (data == null)
            {
                return NotFound(APIResponse<string>.Fail("Data not found."));
            }
            return Ok(APIResponse<List<LeaseDto>>.Success(data!, "Data retrieved successfully."));
        }


        /// <summary>
        /// End a lease.
        /// </summary>`
        // GET: api/leases/end/{id}
        [HttpGet("end/{id:long}")]
        [ProducesResponseType(typeof(APIResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse<string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EndLease(long id, CancellationToken ct = default)
        {
            await _svc.EndLeaseAsync(id, ct);
            return Ok(APIResponse<string>.Success(null, "Lease ended successfully."));
        }
    }
}
