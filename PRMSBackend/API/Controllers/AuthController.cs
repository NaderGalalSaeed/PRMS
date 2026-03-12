
using API.Helper;
using API.Services;
using Application.DTOs;
using Application.Interfaces.IServices;
using Azure.Core;
using Domain.Entities;
using ERPClean.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route($"{APIService.EndPonit}/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly IConfiguration _configuration;

        public AuthController(AuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var result = await _authService.RegisterAsync(request);

            if (result == "Email already exists")
                return BadRequest(new { message = result });

            return Ok(new { message = result });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var token = await _authService.LoginAsync(dto.Email, dto.Password);

            if (token == null)
                return Unauthorized(new { message = "Invalid email or password" });

            var expiresIn = int.Parse(_configuration["Jwt:AccessTokenMinutes"]) * 60;

            return Ok(new { token, expiresIn });
        }

        //[HttpPost("refresh-token")]
        //public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        //{
        //    var result = await _authService.RefreshTokenAsync(request.RefreshToken);

        //    if (result is { message: string })
        //        return Unauthorized(new { message = result.message });

        //    return Ok(result);
        //}
    }
}
