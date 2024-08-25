using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.CourseDto;
using api.Models;

namespace api.Interface
{
    public interface ICourseRepository
    {
        Task<(bool Success, string Message, Course? CreatedCourse)> CreateAsync(Course course);
        Task<Course?> GetByIdAsync(int id);
        Task<(bool Success, string Message)> DeleteAsync(string courseCode);
        Task<List<CourseDto>> GetAllAsync();


    }
}