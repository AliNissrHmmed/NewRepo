
using ERP.PURCHASES.Dto;
using Microsoft.EntityFrameworkCore;

namespace ERP
{

    public class SubCategoryRepo : Repository<Subcategory>,ISubCategoryRepo
    {

        private readonly ApplicationDbContext _context;

        public SubCategoryRepo(ApplicationDbContext context):base(context)
        {
            _context = context;
        }


        public async Task<Subcategory> CreateSubCategoryAsync(Sub_categoryDto subCategoryDto)

        {
            Subcategory subcategory = new Subcategory()

            {

                name = subCategoryDto.name,

                user_id = Guid.NewGuid(),
                state = false,
                Maincategory_id = subCategoryDto.maincategory_id,
                Organization_id = Guid.NewGuid()


            };
            await _context.Subcategories.AddAsync(subcategory);
            await _context.SaveChangesAsync();
            return subcategory;
        }




        public async Task UpdateSub_categoryAsync(UpdateSub_categoryDto updateSub_categoryDto, Guid id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Fetch the existing main category
                var existingSub_category = await _context.Subcategories.FindAsync(id);

                if (existingSub_category == null)
                {
                    throw new InvalidOperationException("Main category not found.");
                }

                // Update main category details
                existingSub_category.name = updateSub_categoryDto.name ?? existingSub_category.name;
                existingSub_category.state = updateSub_categoryDto.state ?? existingSub_category.state;


                _context.Subcategories.Update(existingSub_category);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new InvalidOperationException("An error occurred while updating the main category.", ex);
            }
        }



        public async Task<PaginatedResult<Subcategory>> getsubCategoryAsync(int pageNumber = 1, int pageSize = 25)
        {
            int totalCount = await _context.Subcategories.CountAsync();

            var subcat = await _context.Subcategories

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
            return new PaginatedResult<Subcategory>
            {
                Data = subcat,
                Metadata = paginationData
            };

        }
    }
}


