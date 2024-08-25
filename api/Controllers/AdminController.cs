using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.AdminDto;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController:ControllerBase
    {
              private readonly UserManager<UserBase> _userManager;
        private readonly ITokenService _tokenService;
        
        private readonly SignInManager<UserBase> _signinManager;
        public AdminController(UserManager<UserBase> userManager, ITokenService tokenService,SignInManager<UserBase> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signinManager = signInManager;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(AdminLoginDto loginDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.Users.FirstOrDefaultAsync(x=>x.UserName == loginDto.UserName.ToLower());
            if(user == null)
            {
                return Unauthorized("Invalid username!");
            }
            var result = await _signinManager.CheckPasswordSignInAsync(user, loginDto.Password,false);
            if (!result.Succeeded) return Unauthorized("Username not found and/or password incorrect");
            return Ok(
                 new NewUserDto
                 {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = await _tokenService.CreateToken(user), 
                 }
            );
        }
    }
}