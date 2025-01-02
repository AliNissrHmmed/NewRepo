using ERP.PURCHASES.Dto;
using Microsoft.EntityFrameworkCore;

namespace ERP

{
    public class Or_maingroupRepo : Repository<Or_Maingroup>
    {
        private readonly ApplicationDbContext _context;

        public Or_maingroupRepo(ApplicationDbContext context) : base(context)
        {
            _context = context; 
        }


        public async Task<List<Or_Maingroup>>GetmainMainGroupAsync()

        {
           
             return   await _context.Or_Maingroups.ToListAsync();
 
        }


        public async Task<PaginatedResult<Or_Maingroup>> GetMainGroupAsync(int pageNumber = 1, int pageSize = 25)
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

        }

        public async Task CreatemainMainGroupAsync(Or_maingroupDto or_MaingroupDto)

        {
            Or_Maingroup or_Maingroup = new Or_Maingroup()

            {

                Name = or_MaingroupDto.Name,
                //code = or_MaingroupDto.Code,

                 Organization_id = Guid.NewGuid(),
                user_id = or_MaingroupDto.UserId,
                State = or_MaingroupDto.State,
                CreatedAt= or_MaingroupDto.CreatedAt,

            };
            await _context.Or_Maingroups.AddAsync(or_Maingroup);
            await _context.SaveChangesAsync();




        }


    }
}
