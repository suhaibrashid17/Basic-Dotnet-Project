using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.StudentDto;
using api.Interface;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<UserBase> _userManager;
        public StudentRepository(ApplicationDBContext context, UserManager<UserBase> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<Student?> GetByUsernameAsync(string username)
        {
            return await _context.Student.FirstOrDefaultAsync(x => x.UserName == username);
        }
        public async Task<(bool Success, string Message, Student? Student)> CreateAsync(Student student, string password)
        {
            var existingStudent = await _userManager.FindByIdAsync(student.Id);

            if (existingStudent != null)
            {
                return (false, "Student with this ID already exists.", null);
            }

            var result = await _userManager.CreateAsync(student, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(student, "User");
                return (true, "Student created successfully.", student);
            }

            var errorMessage = string.Join(", ", result.Errors.Select(e => e.Description));

            return (false, $"Failed to create student: {errorMessage}", null);
        }
        public async Task<(bool Success, string Message)> DeleteAsync(string userName)
        {
            var student = await _userManager.FindByNameAsync(userName);
            if (student == null)
            {
                return (false, "Student not found.");
            }

            var result = await _userManager.DeleteAsync(student);
            if (result.Succeeded)
            {
                return (true, "Student deleted successfully.");
            }

            return (false, "Failed to delete the student.");
        }
        public async Task<(bool Success, string Message)> UpdateAsync(string username, StudentUpdateDto studentUpdateDto)
        {
            var student = await _userManager.FindByNameAsync(username);
            if (student == null)
            {
                return (false, "Student not found.");
            }

            student.Name = studentUpdateDto.Name;
            var result = await _userManager.UpdateAsync(student);
            if (result.Succeeded)
            {
                return (true, "Student updated successfully.");
            }

            return (false, "Failed to update the student.");
        }
          public async Task<List<StudentDto>> GetAllAsync()
        {
            return await _context.Student
                .Select(s => s.ToStudentDto())
                .ToListAsync();
        }
    }
   
}