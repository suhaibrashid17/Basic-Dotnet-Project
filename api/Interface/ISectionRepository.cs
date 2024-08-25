using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Section;
using api.Models;

namespace api.Interface
{
    public interface ISectionRepository
    {
         Task<Section> CreateAsync(Section section);
         Task<List<SectionDto>> GetAllAsync();
         Task<Section?> GetByIdAsync(int id);

         Task<bool> DeleteByNameAsync(string name);
    }
}