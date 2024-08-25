using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.InstructorDto;
using api.Interface;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<UserBase> _userManager;
        public InstructorRepository(ApplicationDBContext context, UserManager<UserBase> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<Instructor?> GetByUsernameAsync(string username)
        {
            return await _context.Instructor.FirstOrDefaultAsync(x => x.UserName == username);
        }
        public async Task<(bool Success, string Message, Instructor? Instructor)> CreateAsync(Instructor instructor, string password)
        {
            var existingInstructor = await _userManager.FindByIdAsync(instructor.Id);

            if (existingInstructor != null)
            {
                return (false, "Instructor with this ID already exists.", null);
            }

            var result = await _userManager.CreateAsync(instructor, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(instructor, "User");
                return (true, "Instructor created successfully.", instructor);
            }

            // Collect and format the error messages
            var errorMessage = string.Join(", ", result.Errors.Select(e => e.Description));

            return (false, $"Failed to create instructor: {errorMessage}", null);
        }

        public async Task<(bool Success, string Message)> DeleteAsync(string userName)
        {
            var instructor = await _userManager.FindByNameAsync(userName);
            if (instructor == null)
            {
                return (false, "Instructor not found.");
            }

            var result = await _userManager.DeleteAsync(instructor);
            if (result.Succeeded)
            {
                return (true, "Instructor deleted successfully.");
            }

            return (false, "Failed to delete the instructor.");
        }


        public async Task<(bool Success, string Message)> UpdateAsync(string username, InstructorUpdateDto instructorUpdateDto)
        {
            var instructor = await _userManager.FindByNameAsync(username);
            if (instructor == null)
            {
                return (false, "Instructor not found.");
            }

            instructor.Name = instructorUpdateDto.Name;
            var result = await _userManager.UpdateAsync(instructor);
            if (result.Succeeded)
            {
                return (true, "Instructor updated successfully.");
            }

            return (false, "Failed to update the instructor.");
        }

          public async Task<List<InstructorDto>> GetAllAsync()
        {
            return await _context.Instructor
                .Select(i => i.ToInstructorDto())
                .ToListAsync();
        }

    }
}