using ERP.PURCHASES.Dto;
using ERP.PURCHASES.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ERP

{
    public class Or_maingroupRepo : Repository<Or_Maingroup>,IMain_Groups_Repository
    {
        private readonly ApplicationDbContext _context;

        public Or_maingroupRepo(ApplicationDbContext context) : base(context)
        {
            _context = context; 
        }


       


       /* public async Task<PaginatedResult<Or_Maingroup>> GetMainGroupAsync(int pageNumber = 1, int pageSize = 25)
        {
            int totalCount = await _context.Or_Maingroups.CountAsync();

            var maingroup = await _context.Or_Maingroups

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
            return new PaginatedResult<Or_Maingroup>
            {
                Data = maingroup,
                Metadata = paginationData
            };

        }*/


        public async Task UpdateMainGroupAsync(Update_maingroupDto update_maingroupDto,Guid id)
        {
           
            try
            {
                var existingMaingroup = await _context.Or_Maingroups.FindAsync(id);

                if (existingMaingroup == null)
                {
                    throw new InvalidOperationException("Maingroup not found.");
                }

                // Update main category details

                existingMaingroup.Name = update_maingroupDto.Name ?? existingMaingroup.Name;
                existingMaingroup.State = update_maingroupDto.State ?? existingMaingroup.State;


                 _context.Or_Maingroups.Update(existingMaingroup);

                await _context.SaveChangesAsync();
                

            }
            catch (Exception ex)
            {
                
                throw new InvalidOperationException("An error occurred while update existingMaingroup", ex);
            }
           
        }

        public async Task UpdateSubGroupAsync(UpdateSubgroupDto update_SubgroupDto, Guid id)
        {

            try
            {
                var existingSubgroup = await _context.Ma_Subgroups.FindAsync(id);

                if (existingSubgroup == null)
                {
                    throw new InvalidOperationException("existingSubgroup not found.");
                }



                existingSubgroup.suptreegroup = update_SubgroupDto.SupTreeGroup ?? existingSubgroup.suptreegroup;
                existingSubgroup.State = update_SubgroupDto.State ?? existingSubgroup.State;
                existingSubgroup.itemtype = update_SubgroupDto.TypeItem ?? existingSubgroup.itemtype;
                existingSubgroup.SectionId = update_SubgroupDto.SectionId ?? existingSubgroup.SectionId;
                existingSubgroup.note = update_SubgroupDto.Note ?? existingSubgroup.note;

                _context.Ma_Subgroups.Update(existingSubgroup);

                await _context.SaveChangesAsync();


            }
            catch (Exception ex)
            {

                throw new InvalidOperationException("An error occurred while update existingSubgroup", ex);
            }

        }


    }
}
