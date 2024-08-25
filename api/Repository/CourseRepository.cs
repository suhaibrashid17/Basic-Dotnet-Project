using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.CourseDto;
using api.Interface;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDBContext _context;
        public CourseRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<(bool Success, string Message, Course? CreatedCourse)> CreateAsync(Course course)
        {
            var existingCourse = await _context.Course.FirstOrDefaultAsync(c => c.CourseCode == course.CourseCode);
            if (existingCourse != null)
            {
                return (false, "Course code already exists.", null);
            }
            await _context.Course.AddAsync(course);
            await _context.SaveChangesAsync();
            return (true, "Course created successfully.", course);
        }
        public async Task<Course?> GetByIdAsync(int id)
        {
            return await _context.Course.FirstOrDefaultAsync(x => x.Id == id);
        }

    public async Task<(bool Success, string Message)> DeleteAsync(string courseCode)
    {
        var course = await _context.Course.FirstOrDefaultAsync(c => c.CourseCode == courseCode);
        if (course == null)
        {
            return (false, "Course not found.");
        }

        _context.Course.Remove(course);
        await _context.SaveChangesAsync();
        return (true, "Course deleted successfully.");
    }
    public async Task<List<CourseDto>> GetAllAsync()
        {
            return await _context.Course
                .Select(c => c.ToCourseDto())
                .ToListAsync();
        }
    }
}