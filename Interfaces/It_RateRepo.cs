

using ERP.PURCHASES.Dto;

namespace ERP

{
   public interface IIt_RateRepo : IRepository<Rate>
   {
        public Task UpdateRateAsync(UpdateIt_RateDto updateIt_RateDto, Guid id);

        // public Task CreateIt_Rate(CreateIt_RateDto createIt_RateDto);
        public Task<Rate> CreateIt_RateAsync(CreateIt_RateDto createIt_RateDto);

   public Task<PaginatedResult<Rate>> GetallRatesAsync(int pageNumber = 1, int pageSize = 25);


   }
}


