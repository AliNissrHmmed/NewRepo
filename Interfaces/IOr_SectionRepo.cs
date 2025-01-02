using ERP.PURCHASES.Dto;

namespace ERP
{
    public interface IOr_SectionRepo :IRepository<Section>
    {

        public Task<PaginatedResult<Section>> getSectionAsync(int pageNumber = 1, int pageSize = 25);
        public Task UpdateSectionAsync(UpdateSectionDto updateSectionDto, Guid id);

        public Task<Section> createsectionAsync(SectionDto createSectionDto);

    }
}
