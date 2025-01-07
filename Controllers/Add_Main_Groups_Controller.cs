using Azure;
using ERP.PURCHASES.Dto;
using ERP.PURCHASES.Interfaces;
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
        private readonly IMain_Groups_Repository _MaingroupRepo;
        public MainGroupsSubgroupController(IAdd_Main_Groups_Repository repository, IMain_Groups_Repository MaingroupRepo, ApplicationDbContext context)
        {
            _repository = repository;
            _MaingroupRepo = MaingroupRepo;
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
        [HttpGet("GetMainGroupsWithMatchingSubGroups")]
        public async Task<IActionResult> GetMainGroupsWithMatchingSubGroups([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                // استدعاء الدالة من الـ Repository لجلب البيانات
                var result = await _repository.GetMainGroupsWithSubGroupsByCodeAsync(pageNumber, pageSize);

                // التحقق إذا لم يتم العثور على بيانات
                if (result == null || !result.Data.Any())
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.Message = "لم يتم العثور على أي بيانات مطابقة.";
                    return NotFound(_response);
                }

                // استجابة ناجحة
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Message = "تم استرجاع البيانات بنجاح.";
                _response.Result = result;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                // استجابة الخطأ
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Message = "حدث خطأ أثناء استرجاع البيانات.";
                _response.ErrorMessages.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, _response);
            }
        }
        [HttpGet("GetMainGroupWithSubGroupsById")]
        public async Task<IActionResult> GetMainGroupWithSubGroupsById([FromQuery] Guid id)
        {
            try
            {
                var result = await _repository.GetMainGroupWithSubGroupsByIdAsync(id);

                if (result == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.Message = "لم يتم العثور على أي بيانات مطابقة.";
                    return NotFound(_response);
                }

                // استجابة ناجحة
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Message = "تم استرجاع البيانات بنجاح.";
                _response.Result = result;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                // استجابة الخطأ
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Message = "حدث خطأ أثناء استرجاع البيانات.";
                _response.ErrorMessages.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, _response);
            }
        }
        [HttpDelete("DeleteMainGroupWithSubGroups")]
        public async Task<IActionResult> DeleteMainGroupWithSubGroups([FromQuery] Guid id)
        {
            try
            {
                var isDeleted = await _repository.DeleteMainGroupWithSubGroupsAsync(id);

                if (!isDeleted)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.Message = "لم يتم العثور على المجموعة الرئيسية المطلوبة.";
                    return NotFound(_response);
                }
                // استجابة ناجحة
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Message = "تم حذف المجموعة الرئيسية والمجموعات الفرعية المرتبطة بها بنجاح";
                return Ok(_response);
                //return Ok(new { Message = "تم حذف المجموعة الرئيسية والمجموعات الفرعية المرتبطة بها بنجاح." });
            }
            catch (Exception ex)
            {
                // استجابة الخطأ
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Message = "حدث خطأ أثناء الحذف";
                _response.ErrorMessages.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, _response);
            }
        }

        [HttpPut("Update_maingroup/{id}")]
        public async Task<IActionResult> UpdateMaingroupAsync(Guid id, [FromQuery] Update_maingroupDto update_maingroupDto)
        {

            try

            {
                var existin_Maingroup = await _MaingroupRepo.GetByIdAsync(id);


                if (existin_Maingroup == null)
                {


                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.Message = "object not found";
                    return NotFound(_response);

                }


                await _MaingroupRepo.UpdateMainGroupAsync(update_maingroupDto, id);

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Message = "Item updated successfully";

                return Ok(_response);
            }


            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new APIResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    IsSuccess = false,
                    ErrorMessages = new List<string> { ex.Message
                }
                });

            }
        }


        [HttpPut("update_subgroup/{id}")]
        public async Task<IActionResult> UpdateSubgroupAsync(Guid id, [FromQuery] UpdateSubgroupDto update_SubgroupDto)
        {

            try

            {
                var existingSubgroup = await _context.Ma_Subgroups.FindAsync(id);


                if (existingSubgroup == null)
                {


                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.Message = "object not found";
                    return NotFound(_response);

                }


                await _MaingroupRepo.UpdateSubGroupAsync(update_SubgroupDto, id);

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Message = "Item updated successfully";

                return Ok(_response);
            }


            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new APIResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    IsSuccess = false,
                    ErrorMessages = new List<string> { ex.Message
                }
                });

            }
        }

    }
}

