
using ERP.PURCHASES.Dto;
using ERP.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Repositories
{
    public class AddMainGroupsRepository : Repository<Or_Maingroup>, IAdd_Main_Groups_Repository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AddMainGroupsRepository> _logger;

        public AddMainGroupsRepository(ApplicationDbContext context, ILogger<AddMainGroupsRepository> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddMainGroupWithSubgroupsAsync(AddMainGroupDto addMainGroupDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            Guid userId = Guid.Parse("9d81668e-0fe0-4e66-8d63-99a15f6e236e");
            var cod = Guid.NewGuid();
            try
            {
                var mainGroup = new Or_Maingroup
                {
                    Name = addMainGroupDto.MainGroupName,
                    code = Guid.NewGuid(),
                    Organization_id = userId,
                    user_id = userId,
                    CreatedAt = DateTime.Now,
                    State = addMainGroupDto.State
                };

                await _context.Or_Maingroups.AddAsync(mainGroup);
                await _context.SaveChangesAsync();

                foreach (var subgroupDto in addMainGroupDto.Subgroups)
                {
                    var subgroup = new Ma_Subgroup
                    {
                        code = mainGroup.code, // المفتاح الأجنبي المرتبط بالمجموعة الرئيسية
                        user_id = userId,
                        SectionId = subgroupDto.SectionId,
                        note = subgroupDto.Note,
                        itemtype = subgroupDto.TypeItem,
                        suptreegroup = subgroupDto.SupTreeGroup,
                        CreatedAt = DateTime.UtcNow,
                        State = subgroupDto.State // يمكن تعديل القيمة حسب الحاجة

                    };
                    await _context.Ma_Subgroups.AddRangeAsync(subgroup);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

            }
            catch (Exception ex)
            {
                if (transaction.GetDbTransaction().Connection != null)
                {
                    await transaction.RollbackAsync();
                }

                throw new InvalidOperationException("An error occurred while creating the company with attachments.", ex);
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        }
    }
  

}
 
