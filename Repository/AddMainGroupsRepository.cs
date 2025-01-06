
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


        /*    public async Task<PaginatedResult<Or_Maingroup>> GetMainGroupsWithSubGroupsByCodeAsync(int pageNumber = 1, int pageSize = 25)
            {
                // ملاحظة مهم : لم استطع استخدام ال Include في هذا الكود وعندما قمت باستخدامه لم يقوم بجلب مجموعات الفرعية المتعلقة بالمجموعة الرئيسية 
                // والسبب ان لا يوجد فورنكي بين الجدولين 



                // تنفيذ الاستعلام لجلب المجموعات الرئيسية
                var mainGroupsQuery = _context.Or_Maingroups
                    .Where(mg => _context.Ma_Subgroups.Any(sg => sg.code == mg.code)); // تحقق من وجود تطابق

                // حساب العدد الإجمالي للعناصر
                var totalItems = await mainGroupsQuery.CountAsync();

                // التحقق إذا لم يتم العثور على بيانات
                if (totalItems == 0)
                {
                    return new PaginatedResult<Or_Maingroup>
                    {
                        Data = new List<Or_Maingroup>(),
                        Metadata = new PaginationData
                        {
                            totalCount = 0,
                            pageSize = pageSize,
                            currentPage = pageNumber,
                            totalPages = 0
                        }
                    };
                }

                // تطبيق التصفح على المجموعات الرئيسية
                var mainGroups = await mainGroupsQuery
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                // جلب المجموعات الفرعية المطابقة لكل مجموعة رئيسية
                var allSubGroups = await _context.Ma_Subgroups.ToListAsync();
                foreach (var mainGroup in mainGroups)
                {
                    mainGroup.Ma_Subgroups = allSubGroups
                        .Where(sg => sg.code == mainGroup.code)
                        .ToList();
                }

                // إرجاع النتيجة مع بيانات التصفح
                return new PaginatedResult<Or_Maingroup>
                {
                    Data = mainGroups,
                    Metadata = new PaginationData
                    {
                        totalCount = totalItems,
                        pageSize = pageSize,
                        currentPage = pageNumber,
                        totalPages = (int)Math.Ceiling((double)totalItems / pageSize)
                    }
                };
            }*/











        public async Task<PaginatedResult<MainGroupWithSubGroupsDto>> GetMainGroupsWithSubGroupsByCodeAsync(int pageNumber = 1, int pageSize = 25)
        {
            // تنفيذ الاستعلام لجلب المجموعات الرئيسية
            var mainGroupsQuery = _context.Or_Maingroups
                .Where(mg => _context.Ma_Subgroups.Any(sg => sg.code == mg.code)); // تحقق من وجود تطابق

            // حساب العدد الإجمالي للعناصر
            var totalItems = await mainGroupsQuery.CountAsync();

            // التحقق إذا لم يتم العثور على بيانات
            if (totalItems == 0)
            {
                return new PaginatedResult<MainGroupWithSubGroupsDto>
                {
                    Data = new List<MainGroupWithSubGroupsDto>(),
                    Metadata = new PaginationData
                    {
                        totalCount = 0,
                        pageSize = pageSize,
                        currentPage = pageNumber,
                        totalPages = 0
                    }
                };
            }

            // تطبيق التصفح على المجموعات الرئيسية
            var mainGroups = await mainGroupsQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // جلب جميع المجموعات الفرعية
            var allSubGroups = await _context.Ma_Subgroups.ToListAsync();

            // تحويل البيانات إلى MainGroupWithSubGroupsDto
            var items = mainGroups.Select(mg => new MainGroupWithSubGroupsDto
            {
                MainGroupName = mg.Name,
                MainGroupCode = mg.code.ToString(),
               // OrganizationId = mg.Organization_id,
                CreatedAt = mg.CreatedAt,
                State = mg.State,
                SubGroups = allSubGroups
                    .Where(sg => sg.code == mg.code)
                    .Select(sg => new SubGroupDto
                    {
                        Code = sg.code.ToString(),
                        Note = sg.note,
                        ItemType = sg.itemtype,
                        SupTreeGroup = sg.suptreegroup,
                        SectionId = sg.SectionId,
                        State = sg.State,
                        CreatedAt = sg.CreatedAt
                    }).ToList()
            }).ToList();

            // إرجاع النتيجة مع بيانات التصفح
            return new PaginatedResult<MainGroupWithSubGroupsDto>
            {
                Data = items,
                Metadata = new PaginationData
                {
                    totalCount = totalItems,
                    pageSize = pageSize,
                    currentPage = pageNumber,
                    totalPages = (int)Math.Ceiling((double)totalItems / pageSize)
                }
            };
        }

        public async Task<MainGroupWithSubGroupsDto> GetMainGroupWithSubGroupsByIdAsync(Guid id)
        {
            // البحث عن المجموعة الرئيسية باستخدام id
            var mainGroup = await _context.Or_Maingroups
                .FirstOrDefaultAsync(mg => mg.Id == id);

            // التحقق إذا لم يتم العثور على بيانات
            if (mainGroup == null)
            {
                return null; // أو يمكنك إرجاع رسالة خطأ مخصصة
            }

            // جلب جميع المجموعات الفرعية المطابقة
            var subGroups = await _context.Ma_Subgroups
                .Where(sg => sg.code == mainGroup.code) // مطابقة المجموعات الفرعية باستخدام code
                .ToListAsync();

            // تحويل البيانات إلى MainGroupWithSubGroupsDto
            var result = new MainGroupWithSubGroupsDto
            {
                MainGroupName = mainGroup.Name,
                MainGroupCode = mainGroup.code.ToString(),
                OrganizationId = mainGroup.Organization_id,
                CreatedAt = mainGroup.CreatedAt,
                SubGroups = subGroups.Select(sg => new SubGroupDto
                {
                    Code = sg.code.ToString(),
                    Note = sg.note,
                    ItemType = sg.itemtype,
                    SupTreeGroup = sg.suptreegroup,
                    SectionId = sg.SectionId,
                    State = sg.State,
                    CreatedAt = sg.CreatedAt
                }).ToList()
            };

            return result;
        }

        public async Task<bool> DeleteMainGroupWithSubGroupsAsync(Guid id)
        {
            // البحث عن المجموعة الرئيسية باستخدام id
            var mainGroup = await _context.Or_Maingroups
                .FirstOrDefaultAsync(mg => mg.Id == id);

            // التحقق إذا لم يتم العثور على المجموعة
            if (mainGroup == null)
            {
                return false; // أو يمكنك رمي استثناء
            }

            // جلب المجموعات الفرعية المرتبطة وحذفها
            var subGroups = await _context.Ma_Subgroups
                .Where(sg => sg.code == mainGroup.code)
                .ToListAsync();

            // حذف المجموعات الفرعية
            _context.Ma_Subgroups.RemoveRange(subGroups);

            // حذف المجموعة الرئيسية
            _context.Or_Maingroups.Remove(mainGroup);

            // حفظ التغييرات
            await _context.SaveChangesAsync();

            return true;
        }


    }


}
  
