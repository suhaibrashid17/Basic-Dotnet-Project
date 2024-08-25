using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.InstructorDto;
using api.Models;

namespace api.Interface
{
    public interface IInstructorRepository
    {
        Task<Instructor?> GetByUsernameAsync(string username);
        Task<(bool Success, string Message, Instructor? Instructor)> CreateAsync(Instructor instructor, string Password);
        Task<(bool Success, string Message)> DeleteAsync(string userName);
        Task<(bool Success, string Message)> UpdateAsync(string username, InstructorUpdateDto instructorUpdateDto);

        Task<List<InstructorDto>> GetAllAsync();
    }
}