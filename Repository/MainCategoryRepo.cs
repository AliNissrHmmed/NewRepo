
using ERP.PURCHASES.Dto;
using Microsoft.EntityFrameworkCore;

namespace ERP
{

    public class MainCategoryRepo : Repository<Maincategory>, ImainCategoryRepo
    {

        private readonly ApplicationDbContext _context;

        public MainCategoryRepo(ApplicationDbContext context): base (context)
        {
            _context = context;
        }


        public async Task<Maincategory> CreatemainCategoryAsync(MainCategoryDto mainCategoryDto)

        {
            Maincategory maincategory = new Maincategory()

            {

                name = mainCategoryDto.name,
                Organization_id = Guid.NewGuid(),
                user_id = Guid.NewGuid(),
                state = false,


            };
            await _context.Maincategories.AddAsync(maincategory);
            await _context.SaveChangesAsync();

            return maincategory;


        }


        public async Task UpdateMainCategoryAsync(MainCatUpdateDto updateMainCategoryDto, Guid id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Fetch the existing main category
                var existingMainCategory = await _context.Maincategories.FindAsync(id);

                if (existingMainCategory == null)
                {
                    throw new InvalidOperationException("Main category not found.");
                }

                // Update main category details
                existingMainCategory.name = updateMainCategoryDto.name ?? existingMainCategory.name;
                existingMainCategory.state = updateMainCategoryDto.state ?? existingMainCategory.state;
              

                _context.Maincategories.Update(existingMainCategory);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new InvalidOperationException("An error occurred while updating the main category.", ex);
            }
        }



        public async Task RemoveMainCategoryAsync( Guid id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Fetch the existing main category
                var existingMainCategory = await _context.Maincategories.FindAsync(id);

                if (existingMainCategory == null)
                {
                    throw new InvalidOperationException("Main category not found.");
                }

                var existingsubcat = await _context.Subcategories.Where(s => s.Maincategory_id == id).ToListAsync();

                if (existingsubcat.Any())
                {
                    _context.Subcategories.RemoveRange(existingsubcat);
                   await _context.SaveChangesAsync(); 
                }

                _context.Maincategories.Remove(existingMainCategory);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new InvalidOperationException("An error occurred while updating the main category.", ex);
            }
        }

        public async Task<PaginatedResult<Maincategory>> getMainCategoryAsync(int pageNumber = 1, int pageSize = 25)
        {
            int totalCount = await _context.Maincategories.CountAsync();

            var maincat = await _context.Maincategories

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
            return new PaginatedResult<Maincategory>
            {
                Data = maincat,
                Metadata = paginationData
            };

        }


    }
}


