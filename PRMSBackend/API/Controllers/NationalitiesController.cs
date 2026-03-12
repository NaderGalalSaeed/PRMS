
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
    public class NationalitiesController : ControllerBase
    {
        readonly INationalityService _svc;

        public NationalitiesController(INationalityService svc)
        {
            _svc = svc;
        }


        /// <summary>
        /// Get all data.
        /// </summary>
        // GET: api/nationalities
        [HttpGet]
        [ProducesResponseType(typeof(APIResponse<IEnumerable<NationalityDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken ct = default)
        {
            var data = await _svc.GetListAsync(ct) ?? Enumerable.Empty<NationalityDto>();
            return Ok(APIResponse<IEnumerable<NationalityDto>>.Success(data, "All data retrieved successfully."));
        }


        /// <summary>
        /// Get data by ID.
        /// </summary>`
        // GET: api/nationalities/{id}
        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(APIResponse<NationalityDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse<string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(long id, CancellationToken ct = default)
        {
            var data = await _svc.GetByIdAsync(id, ct);
            if (data == null)
            {
                return NotFound(APIResponse<string>.Fail("Data not found."));
            }
            return Ok(APIResponse<NationalityDto>.Success(data!, "Data retrieved successfully."));
        }


        /// <summary>
        /// Create new data.
        /// </summary>
        // POST: api/nationalities
        [HttpPost]
        [ProducesResponseType(typeof(APIResponse<long>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(APIResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] NationalityDto dto, CancellationToken ct = default)
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
        // PUT: api/nationalities/{id}
        [HttpPut("{id:long}")]
        [ProducesResponseType(typeof(APIResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(APIResponse<string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(long id, [FromBody] NationalityDto dto, CancellationToken ct = default)
        {
            if (dto == null || id != dto.Id)
                return BadRequest(APIResponse<string>.Fail("Request ID mismatch or payload is invalid."));

            await _svc.UpdateAsync(dto, ct);
            return Ok(APIResponse<string>.Success(null, "Data updated successfully."));
        }


        /// <summary>
        /// Delete data.
        /// </summary>
        // DELETE: api/nationalities/{id}
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
    }
}
