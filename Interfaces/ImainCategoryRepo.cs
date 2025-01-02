
using ERP.PURCHASES.Dto;

namespace ERP
{
    public interface ImainCategoryRepo :IRepository<Maincategory>
    {
        public Task<Maincategory> CreatemainCategoryAsync(MainCategoryDto mainCategoryDto);
        public Task RemoveMainCategoryAsync(Guid id);

        //public Task CreatemainCategoryAsync(MainCategoryDto mainCategoryDto);
        public Task<PaginatedResult<Maincategory>> getMainCategoryAsync(int pageNumber = 1, int pageSize = 25);
        public Task UpdateMainCategoryAsync(MainCatUpdateDto updateMainCategoryDto, Guid id);
    }
}