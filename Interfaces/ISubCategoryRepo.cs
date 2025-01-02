using ERP.PURCHASES.Dto;

namespace ERP

{
    public interface ISubCategoryRepo : IRepository<Subcategory>
    {
        public Task<Subcategory> CreateSubCategoryAsync(Sub_categoryDto subCategoryDto);
        public Task UpdateSub_categoryAsync(UpdateSub_categoryDto updateSub_categoryDto, Guid id);

        public Task<PaginatedResult<Subcategory>> getsubCategoryAsync(int pageNumber = 1, int pageSize = 25);

    }
}