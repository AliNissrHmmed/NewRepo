using ERP.PURCHASES.Dto;

namespace ERP
{
    public interface IAdd_Main_Groups_Repository : IRepository<Or_Maingroup>
    {

        /// إضافة مجموعة رئيسية مع المجموعات الفرعية المرتبطة بها
        Task AddMainGroupWithSubgroupsAsync(AddMainGroupDto addMainGroupDto);
       /* /// جلب المجموعات الرئيسية مع دعم التصفح (Pagination)
        Task<PaginatedResult<Or_Maingroup>> GetMainGroupAsync(int pageNumber = 1, int pageSize = 25);
        /// جلب المجموعات الفرعية مع دعم التصفح (Pagination)      
        Task<PaginatedResult<Ma_Subgroup>> GetSubGroupAsync(int pageNumber = 1, int pageSize = 25);

        /// إضافة مجموعة رئيسية فقط
        Task CreateMainGroupAsync(Or_maingroupDto or_MaingroupDto);

        /// إضافة مجموعة فرعية فقط
        Task CreateMa_SubgroupAsync(MaSubgroupDto maSubgroupDto);*/
    }
}

