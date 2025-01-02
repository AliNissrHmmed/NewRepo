using Azure;
using ERP.PURCHASES.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;


namespace ERP
{
    [Route("API/[Controller]")]
    [ApiController]
    public class MainGroupsSubgroupController : ControllerBase
    {
        private readonly IAdd_Main_Groups_Repository _repository;
        private readonly APIResponse _response;
        private readonly ApplicationDbContext _context;

        public MainGroupsSubgroupController(IAdd_Main_Groups_Repository repository , ApplicationDbContext context)
        {
            _repository = repository;
            _response = new APIResponse();
            _context = context;
        }
        [HttpPost("AddMainGroupWithSubgroups")]
        public async Task<IActionResult> AddMainGroupWithSubgroups([FromBody] AddMainGroupDto addMainGroupDto)
        {
            if (addMainGroupDto == null || string.IsNullOrEmpty(addMainGroupDto.MainGroupName))
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Message = "بيانات المجموعة الرئيسية غير مكتملة.";
                _response.ErrorMessages.Add("اسم المجموعة الرئيسية مطلوب.");
                return BadRequest(_response);
            }

            try
            {
                await _repository.AddMainGroupWithSubgroupsAsync(addMainGroupDto);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Message = "تمت إضافة المجموعة الرئيسية والمجموعات الفرعية بنجاح.";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Message = "حدث خطأ أثناء العملية.";
                _response.ErrorMessages.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, _response);
            }
        }
    }
}

//[HttpPost]
/*        public async Task<IActionResult> AddMainGroupWithSubgroups([FromBody] AddMainGroupDto addMainGroupDto)
        {
            Guid userId = Guid.Parse("9d81668e-0fe0-4e66-8d63-99a15f6e236e");

            if (addMainGroupDto == null || string.IsNullOrEmpty(addMainGroupDto.MainGroupName))
            {
                return BadRequest("Invalid main group data.");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var mainGroup = new Or_Maingroup
                {
                    Id = Guid.NewGuid(),
                    Name = addMainGroupDto.MainGroupName,
                    code = addMainGroupDto.MainGroupCode,
                    CreatedAt = DateTime.UtcNow,
                    Organization_id = Guid.NewGuid(), // Set this to the appropriate value
                    user_id = userId, // Set this to the appropriate value
                    State = true
                };

                await _context.Or_Maingroups.AddAsync(mainGroup);
                await _context.SaveChangesAsync();

                foreach (var subgroupDto in addMainGroupDto.Subgroups)
                {
                    if (subgroupDto.SectionId == Guid.Empty || string.IsNullOrEmpty(subgroupDto.Note))
                    {
                        //_logger.LogError("Invalid subgroup data. Skipping...");
                        continue;
                    }

                    var subgroup = new Ma_Subgroup
                    {
                        Id = Guid.NewGuid(),
                        code = mainGroup.code,
                        user_id = userId,
                        SectionId = subgroupDto.SectionId,
                        note = subgroupDto.Note,
                        itemtype = subgroupDto.TypeItem,
                        suptreegroup = subgroupDto.State,
                        CreatedAt = DateTime.UtcNow,
                        State = subgroupDto.State,
                        maingroup_id = mainGroup.Id
                    };

                    //mainGroup.Ma_Subgroups.Add(subgroup);
                    await _context.Ma_Subgroups.AddRangeAsync(subgroup);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(mainGroup);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
               // _logger.LogError(ex, "An error occurred while adding main group and subgroups.");
                return StatusCode(500, "Internal server error");
            }
        }
*/
