using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interface;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class SectionCourseStudentRepository : ISectionCourseStudentRepository
    {
        private readonly ApplicationDBContext _context;

        public SectionCourseStudentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<(bool isSuccess, string message, SectionCourseStudent? sectionCourseStudent)> RegisterStudentToSectionCourseAsync(string sectionName, string courseCode, string studentUsername)
        {
            // Find the SectionCourse pair
            var sectionCourse = await _context.SectionCourse
                .Include(sc => sc.Section)
                .Include(sc => sc.Course)
                .FirstOrDefaultAsync(sc => sc.Section.Name == sectionName && sc.Course.CourseCode == courseCode);

            if (sectionCourse == null)
            {
                return (false, "Section-Course pair does not exist.", null); // Section-Course pair does not exist
            }

            // Find the student by username
            var student = await _context.Student
                .FirstOrDefaultAsync(s => s.UserName == studentUsername);

            if (student == null)
            {
                return (false, "Student not found.", null); // Student not found
            }

            // Check if the student is already registered for this section-course pair
            var existingRegistration = await _context.SectionCourseStudent
                .FirstOrDefaultAsync(scs => scs.SectionId == sectionCourse.SectionId && scs.CourseId == sectionCourse.CourseId && scs.StudentId == student.Id);

            if (existingRegistration != null)
            {
                return (false, "This student is already registered for this section-course pair.", null); // Student already registered
            }

            var sectionCourseStudent = new SectionCourseStudent
            {
                SectionId = sectionCourse.SectionId,
                CourseId = sectionCourse.CourseId,
                StudentId = student.Id
            };

            _context.SectionCourseStudent.Add(sectionCourseStudent);
            await _context.SaveChangesAsync();

            return (true, "Student successfully registered for the Section-Course pair.", sectionCourseStudent); // Successfully registered
        }


    }

}