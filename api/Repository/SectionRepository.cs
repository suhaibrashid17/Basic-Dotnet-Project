using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Section;
using api.Interface;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class SectionRepository : ISectionRepository
    {
        private readonly ApplicationDBContext _context;

        public SectionRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Section> CreateAsync(Section section)
        {
            // Check if a section with the same name already exists
            var existingSection = await _context.Section
                .FirstOrDefaultAsync(s => s.Name == section.Name);
            
            if (existingSection != null)
            {
                // Handle the case where a section with the same name already exists
                throw new InvalidOperationException("A section with the same name already exists.");
            }

            await _context.Section.AddAsync(section);
            await _context.SaveChangesAsync();
            return section;
        }

        public async Task<List<SectionDto>> GetAllAsync()
        {
            return await _context.Section
                .Select(s => s.ToSectionDto())
                .ToListAsync();
        }

        public async Task<Section?> GetByIdAsync(int id)
        {
            var section = await _context.Section.FindAsync(id);
            if (section != null)
                return section;
            return null;
        }
        public async Task<bool> DeleteByNameAsync(string name)
        {
            var section = await _context.Section.FirstOrDefaultAsync(s => s.Name == name);
            if (section == null)
            {
                return false;
            }

            _context.Section.Remove(section);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
