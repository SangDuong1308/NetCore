using src.Dtos.Auth;
using src.Dtos.Employee;
using src.Interfaces;
using src.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace src.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signinManager;
        private readonly ILogger<AccountController> _logger;

        public EmployeeController(IEmployeeRepository employeeRepo,UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signinManager, ILogger<AccountController> logger)
        {
            _employeeRepo = employeeRepo;
            _userManager = userManager;
            _tokenService = tokenService;
            _signinManager = signinManager;
            _logger = logger;
        }

        [SwaggerOperation(Summary = "Register a new App User, not create a new Employee yet")]
        [HttpPost]
        // [Authorize(Roles = "HRHead")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Register([FromBody] CreateEmployeeDto createEmployeeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var appUser = new AppUser
                {
                    UserName = createEmployeeDto.Username,
                    Email = createEmployeeDto.Email,
                };

                var createUser = await _userManager.CreateAsync(appUser, createEmployeeDto.Password);
                if (createUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "Employee");
                    if (roleResult.Succeeded)
                    {
                        return Ok(new NewEmployeeResponseDto
                        {
                            UserName = appUser.UserName,
                            Email = appUser.Email,
                        });
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}