using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.InstructorDto;
using api.Interface;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/instructor")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorRepository _instructorRepo;
        private readonly UserManager<UserBase> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<UserBase> _signinManager;

        public InstructorController(UserManager<UserBase> userManager, ITokenService tokenService, SignInManager<UserBase> signInManager, IInstructorRepository instructorRepo)
        {
            _instructorRepo = instructorRepo;
            _userManager = userManager;
            _tokenService = tokenService;
            _signinManager = signInManager;
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetByUserName([FromRoute] string username)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var Instructor = await _instructorRepo.GetByUsernameAsync(username);
            if (Instructor == null)
                return NotFound();
            return Ok(Instructor.ToInstructorDto());
        }
        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateInstructorDto createInstructorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var instructor = createInstructorDto.ToInstructorFromCreate();
            var (success, message, createdInstructor) = await _instructorRepo.CreateAsync(instructor, createInstructorDto.Password);
            if (success)
            {
                return CreatedAtAction(nameof(GetByUserName), new { username = createdInstructor?.UserName }, createdInstructor?.ToInstructorDto());
            }
            else
            {
                return BadRequest(message);
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(InstructorLoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName.ToLower());
            if (user == null)
            {
                return Unauthorized("Invalid username!");
            }
            var result = await _signinManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return Unauthorized("Username not found and/or password incorrect");
            return Ok(
                 new InstructorLoggedInDto
                 {
                     Name = user.Name,
                     UserName = user.UserName,
                     Email = user.Email,
                     Token = await _tokenService.CreateToken(user),
                 }
            );
        }
        [HttpDelete("{userName}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string userName)
        {
            var (success, message) = await _instructorRepo.DeleteAsync(userName);

            if (!success)
            {
                return NotFound(message);
            }

            return Ok(message);
        }
        [HttpPut("update/{username}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(string username, [FromBody] InstructorUpdateDto instructorUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (success, message) = await _instructorRepo.UpdateAsync(username, instructorUpdateDto);

            if (!success)
            {
                return NotFound(message);
            }

            return Ok(message);
        }

                [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var instructors = await _instructorRepo.GetAllAsync();
            return Ok(instructors);
        }


    }
}