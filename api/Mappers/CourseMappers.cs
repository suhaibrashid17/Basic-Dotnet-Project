using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.CourseDto;
using api.Models;

namespace api.Mappers
{
    public static class CourseMappers
    {
        public static Course ToCourseFromCreate(this CreateCourseDto createCourseDto)
        {
            return new Course{
                 CourseCode = createCourseDto.CourseCode,
                 Name = createCourseDto.Name,
            };
        }
        public static CourseDto ToCourseDto(this Course course)
        {
            return new CourseDto{
                CourseCode= course.CourseCode,
                Name=course.Name
            };

        }
    }
}