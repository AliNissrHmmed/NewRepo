using ERP.PURCHASES.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ERP
{

public class It_RateRepository : Repository<Rate>,IIt_RateRepo


{

 private readonly ApplicationDbContext _context;

        public It_RateRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }



 public async Task<Rate> CreateIt_RateAsync(CreateIt_RateDto createIt_RateDto)
 {

        Rate rate=new Rate
        {
            range=createIt_RateDto.range,
            user_id=createIt_RateDto.user_id,
            Item_id=createIt_RateDto.Item_id,
            note=createIt_RateDto.note,
            Type=createIt_RateDto.Type,
            State=createIt_RateDto.State,
            CreatedAt=createIt_RateDto.CreatedAt


        };

        await _context.Rates.AddAsync(rate);

        await _context.SaveChangesAsync();

            return rate;

        }
        public async Task UpdateRateAsync(UpdateIt_RateDto updateIt_RateDto, Guid id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Fetch the existing rate
                var existingRate = await _context.Rates.FindAsync(id);

                if (existingRate == null)
                {
                    throw new InvalidOperationException("Rate not found.");
                }

            
                existingRate.range = updateIt_RateDto.range ?? existingRate.range;
                existingRate.note =  updateIt_RateDto.note ?? existingRate.note;
                existingRate.Type =  updateIt_RateDto.Type ?? existingRate.Type;
                existingRate.State = updateIt_RateDto.State ?? existingRate.State;

                _context.Rates.Update(existingRate);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new InvalidOperationException("An error occurred while updating the rate.", ex);
            }
        }


        public async Task<PaginatedResult<Rate>> GetallRatesAsync(int pageNumber = 1, int pageSize = 25)
        {
            int totalCount = await _context.Rates.CountAsync();

            var rate= await _context.Rates.Include(r=>r.Items)
                
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
            return new PaginatedResult<Rate>
            {
                Data = rate,
                Metadata = paginationData
            };   

        }




    }

}