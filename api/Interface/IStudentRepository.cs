using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.StudentDto;
using api.Models;

namespace api.Interface
{
    public interface IStudentRepository
    {
        Task<Student?> GetByUsernameAsync(string username);
        Task<(bool Success, string Message, Student? Student)> CreateAsync(Student student, string Password);
        Task<(bool Success, string Message)> DeleteAsync(string userName);

        Task<(bool Success, string Message)> UpdateAsync(string username, StudentUpdateDto studentUpdateDto);

         Task<List<StudentDto>> GetAllAsync();

    }
}