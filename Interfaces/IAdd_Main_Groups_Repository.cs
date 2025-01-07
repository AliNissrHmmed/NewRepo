using ERP.PURCHASES.Dto;

namespace ERP
{
    public interface IAdd_Main_Groups_Repository : IRepository<Or_Maingroup>
    {

        /// إضافة مجموعة رئيسية مع المجموعات الفرعية المرتبطة بها
        Task AddMainGroupWithSubgroupsAsync(AddMainGroupDto addMainGroupDto);
       // Task<MainGroupWithSubGroupsDto> GetMainGroupWithSubGroupsAutomaticallyAsync();
       // Task<PaginatedResult<Or_Maingroup>> GetMainGroupsWithSubGroupsByCodeAsync(int pageNumber = 1, int pageSize = 25);
        Task<PaginatedResult<MainGroupWithSubGroupsDto>> GetMainGroupsWithSubGroupsByCodeAsync(int pageNumber = 1, int pageSize = 25);
        Task<MainGroupWithSubGroupsDto> GetMainGroupWithSubGroupsByIdAsync(Guid id);
        Task<bool> DeleteMainGroupWithSubGroupsAsync(Guid id);

    }
}
