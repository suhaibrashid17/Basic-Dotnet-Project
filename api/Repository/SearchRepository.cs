using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Helper;
using api.Interface;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{


    public class SearchRepository:ISearchRepository
    {
        private readonly ApplicationDBContext _context;
        public SearchRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<object> SearchAsync(SearchCriteriaAdmin criteria)
        {
            var results = new Dictionary<string, object>();

            if (criteria.IncludeSection)
            {
                if (!string.IsNullOrEmpty(criteria.StudentUsername))
                {
                    var sections = await _context.Section
                        .Where(s => _context.SectionCourseStudent
                            .Any(scs => scs.SectionId == s.Id &&
                                        _context.Student
                                            .Any(st => st.Id == scs.StudentId && st.UserName == criteria.StudentUsername)))
                        .ToListAsync();
                    results["Sections"] = sections;
                }
                else
                {
                    results["Sections"] = await _context.Section.ToListAsync();
                }
            }

            if (criteria.IncludeCourse)
            {
                if (!string.IsNullOrEmpty(criteria.StudentUsername))
                {
                    var courses = await _context.Course
                        .Where(c => _context.SectionCourseStudent
                            .Any(scs => scs.CourseId == c.Id &&
                                        _context.Student
                                            .Any(st => st.Id == scs.StudentId && st.UserName == criteria.StudentUsername)))
                        .ToListAsync();
                    results["Courses"] = courses;
                }
                else
                {
                    results["Courses"] = await _context.Course.ToListAsync();
                }
            }

            if (criteria.IncludeStudent)
            {
                if (!string.IsNullOrEmpty(criteria.InstructorUsername))
                {
                    var students = await _context.Student
                        .Where(s => _context.SectionCourseInstructor
                            .Any(sci => _context.Instructor
                                .Any(i => i.Id == sci.InstructorId && i.UserName == criteria.InstructorUsername) &&
                                        _context.SectionCourseStudent
                                            .Any(scs => scs.StudentId == s.Id)))
                        .ToListAsync();
                    results["Students"] = students;
                }
                else
                {
                    results["Students"] = await _context.Student.ToListAsync();
                }
            }

            if (criteria.IncludeInstructor)
            {
                if (!string.IsNullOrEmpty(criteria.CourseCode))
                {
                    var instructors = await _context.Instructor
                        .Where(i => _context.SectionCourseInstructor
                            .Any(sci => sci.CourseId == _context.Course
                                            .FirstOrDefault(c => c.CourseCode == criteria.CourseCode).Id &&
                                        sci.InstructorId == i.Id))
                        .ToListAsync();
                    results["Instructors"] = instructors;
                }
                else
                {
                    results["Instructors"] = await _context.Instructor.ToListAsync();
                }
            }

            return results;
        }

    }
}