using ERP;
using ERP.PURCHASES.Dto;
using Microsoft.EntityFrameworkCore;

namespace ERPPurchases.Repository
{
    public class MaSubgroupRepo : Repository<Ma_Subgroup>, IMaSubgroupRepo
    {
        private readonly ApplicationDbContext _db;

        public MaSubgroupRepo(ApplicationDbContext db) : base(db)
        {

            _db = db;   
        }

        public async Task CreateMa_SubgroupAsync(MaSubgroupDto maSubgroupDto)
        { 

        Ma_Subgroup ma_Subgroup = new Ma_Subgroup()

        {
            //code = maSubgroupDto.Code,
            suptreegroup = maSubgroupDto.SupTreeGroup,

            note = maSubgroupDto.Note,
            itemtype = maSubgroupDto.ItemType,  
            SectionId= maSubgroupDto.SectionId,
            CreatedAt = maSubgroupDto.CreatedAt,
            State = maSubgroupDto.State,
            user_id = maSubgroupDto.UserId,


        };
        await _db.Ma_Subgroups.AddAsync(ma_Subgroup);
        await _db.SaveChangesAsync();




    }





    public async Task<List<Ma_Subgroup>> getSubGroupAsync()
    {

        return await _db.Ma_Subgroups.ToListAsync();



        }


        public async Task<PaginatedResult<Ma_Subgroup>> getSubGroupAsync(int pageNumber = 1, int pageSize = 25)
        {
            int totalCount = await _db.Ma_Subgroups.CountAsync();

            var subgroup = await _db.Ma_Subgroups

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
            return new PaginatedResult<Ma_Subgroup>
            {
                Data = subgroup,
                Metadata = paginationData
            };

        }


    }
}
