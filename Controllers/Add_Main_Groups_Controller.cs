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

    }
}

