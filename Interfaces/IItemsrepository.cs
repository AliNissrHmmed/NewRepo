
using ERP.PURCHASES.Dto;

namespace ERP
{
    public interface IItemsrepository: IRepository<Item>
    {
        public  Task<PaginatedResult<Item>> GetallItemsAsync(int pageNumber = 1, int pageSize = 25);

        public Task UpdateItemAsync(UpdateItemsDto updateItemsDto, Guid id);
        public Task<Item?> GetByidItemsAsync(Guid id);
        public Task<IEnumerable<Item>> CreateItemsAsync(IEnumerable<ItemsDto> itemsDto);


    }
}
