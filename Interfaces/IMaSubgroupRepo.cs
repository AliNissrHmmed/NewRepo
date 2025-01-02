using ERP.PURCHASES.Dto;

namespace ERP
{
    public interface IMaSubgroupRepo : IRepository<Ma_Subgroup>
    {

        public Task CreateMa_SubgroupAsync(MaSubgroupDto maSubgroupDto);

        public Task<PaginatedResult<Ma_Subgroup>> getSubGroupAsync(int pageNumber = 1, int pageSize = 25);


    }
}
