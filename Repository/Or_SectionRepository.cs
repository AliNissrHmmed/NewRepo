


using ERP.PURCHASES.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP
{
    public class Or_SectionRepository : Repository<Section>, IOr_SectionRepo
    {

        private readonly ApplicationDbContext _context;

        public Or_SectionRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

      



        public async Task<Section> createsectionAsync(SectionDto createSectionDto)

        {
            try
            {
                Section section = new Section
                {

                    name = createSectionDto.name,
                    State = false,
                    serial = Guid.NewGuid().ToString(), 
                    user_id = Guid.NewGuid(),
                    CreatedAt = DateTime.Now,
                    Organization_id = Guid.NewGuid(),

                };

                await _context.Sections.AddAsync(section);

                await _context.SaveChangesAsync();
                return section; 
 
            }
            catch (Exception ex)
            {
                throw new ArgumentException(" error");
            }

            

        }


        public async Task<PaginatedResult<Section>> getSectionAsync(int pageNumber = 1, int pageSize = 25)
        {
            int totalCount = await _context.Sections.CountAsync();

            var section = await _context.Sections

                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();


            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var paginationData = new PaginationData
            {
                totalCount = totalCount,
                pageSize = pageSize,
                currentPage = pageNumber,
                totalPages = totalPages
            };

            // Return paginated result with data and metadata
            return new PaginatedResult<Section>
            {
                Data = section,
                Metadata = paginationData
            };

        }
        public async Task UpdateSectionAsync(UpdateSectionDto updateSectionDto, Guid id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
     
                var existingSection = await _context.Sections.FindAsync(id);

                if (existingSection == null)
                {
                    throw new InvalidOperationException("existingSection not found.");
                }

                existingSection.name = updateSectionDto.name ?? existingSection.name;
                existingSection.State = updateSectionDto.state ?? existingSection.State;


                _context.Sections.Update(existingSection);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new InvalidOperationException("An error occurred while updating the Section.", ex);
            }
        }




    }
}
