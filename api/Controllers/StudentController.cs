using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.StudentDto;
using api.Interface;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepo;
        private readonly UserManager<UserBase> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<UserBase> _signinManager;
        public StudentController(UserManager<UserBase> userManager, ITokenService tokenService, SignInManager<UserBase> signInManager, IStudentRepository studentRepo)
        {
            _studentRepo = studentRepo;
            _userManager = userManager;
            _tokenService = tokenService;
            _signinManager = signInManager;

        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetByUserName([FromRoute] string username)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var Student = await _studentRepo.GetByUsernameAsync(username);
            if (Student == null)
                return NotFound();
            return Ok(Student.ToStudentDto());
        }
        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateStudentDto createStudentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = createStudentDto.ToStudentFromCreate();
            var (success, message, createdStudent) = await _studentRepo.CreateAsync(student, createStudentDto.Password);
            if (success)
            {
                return CreatedAtAction(nameof(GetByUserName), new { username = createdStudent?.UserName }, createdStudent?.ToStudentDto());
            }
            else
            {
                return BadRequest(message);
            }
        }
        [HttpGet("login")]
        public async Task<IActionResult> Login(StudentLoginDto loginDto)
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
                 new StudentLoggedInDto
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
            var (success, message) = await _studentRepo.DeleteAsync(userName);

            if (!success)
            {
                return NotFound(message);
            }

            return Ok(message);
        }

        [HttpPut("update/{username}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(string username, [FromBody] StudentUpdateDto studentUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (success, message) = await _studentRepo.UpdateAsync(username, studentUpdateDto);

            if (!success)
            {
                return NotFound(message);
            }

            return Ok(message);
        }
         [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var students = await _studentRepo.GetAllAsync();
            return Ok(students);
        }

    }
}