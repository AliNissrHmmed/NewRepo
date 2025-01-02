using Azure;
using ERP.PURCHASES.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Net;

namespace ERP
{

    public class ItemsRepository : Repository<Item>, IItemsrepository


    {

        private readonly ApplicationDbContext _context;

        public ItemsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }



        public async Task<IEnumerable<Item>> CreateItemsAsync(IEnumerable<ItemsDto> itemsDto)
        {
            if (itemsDto == null || !itemsDto.Any())
            {
                throw new ArgumentException("No items provided for creation.");
            }

            // Map the DTOs to entities
            var items = itemsDto.Select(dto => new Item
            {
                Id = Guid.NewGuid(), // Ensure a new ID is generated
                name = dto.name,
                CreatedAt = dto.CreatedAt,
                State = dto.State,
                Type = dto.Type,
                user_id = dto.user_id,
                Subcategory_id = dto.Subcategory_id,
                company_id = dto.company_id
            }).ToList();

            // Bulk insert
            await _context.Items.AddRangeAsync(items);
            await _context.SaveChangesAsync();

            return items;
        }
        public async Task UpdateItemAsync(UpdateItemsDto updateItemsDto, Guid id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Fetch the existing item
                var existingItem = await _context.Items.FindAsync(id);

                if (existingItem == null)
                {
                    throw new InvalidOperationException("Item not found.");
                }

                // Check if the subcategory is valid if a new value is provided
                if (updateItemsDto.Subcategory_id.HasValue)
                {
                    var isSubcategoryValid = await _context.Subcategories
                        .AnyAsync(s => s.Id == updateItemsDto.Subcategory_id.Value);

                    if (isSubcategoryValid)
                    {
                        existingItem.Subcategory_id = updateItemsDto.Subcategory_id.Value;
                    }
                }

                // Check if the company is valid if a new value is provided
                if (updateItemsDto.company_id.HasValue)
                {
                    var isCompanyValid = await _context.Companies
                        .AnyAsync(c => c.Id == updateItemsDto.company_id.Value);

                    if (isCompanyValid)
                    {
                        existingItem.company_id = updateItemsDto.company_id.Value;
                    }
                }

                // Update item details
                existingItem.name = updateItemsDto.name ?? existingItem.name;
                existingItem.Type = updateItemsDto.Type ?? existingItem.Type;
                existingItem.State = updateItemsDto.state ?? existingItem.State;

                _context.Items.Update(existingItem);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new InvalidOperationException("An error occurred while updating the item.", ex);
            }
        }


        public async Task<PaginatedResult<Item>> GetallItemsAsync(int pageNumber = 1, int pageSize = 25)
        {

            int totalCount = await _context.Items.CountAsync();

            var item = await _context.Items

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
            return new PaginatedResult<Item>
            {
                Data = item,
                Metadata = paginationData
            };

        }





        public async Task<Item?> GetByidItemsAsync(Guid id)
        {

            //await _context.Items.FindAsync(id);

            var item = await _context.Items.Include(i => i.Rates).
            Include(i => i.Companies).

             Include(i => i.Subcategories).


            FirstOrDefaultAsync(i => i.Id == id);

            return item;

        }


    }

}