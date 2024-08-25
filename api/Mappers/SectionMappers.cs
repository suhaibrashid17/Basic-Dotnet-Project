using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Section;
using api.Models;

namespace api.Mappers
{
    public static class SectionMappers
    {
         public static SectionDto ToSectionDto(this Section section)
        {
            return new SectionDto{
                Name = section.Name,
            };
        }

         public static Section ToSectionFromCreate(this CreateSectionDto section)
        {
            return new Section{
                Name = section.Name,
            };
        }
    }
}