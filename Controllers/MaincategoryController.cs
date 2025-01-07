using AutoMapper;
using Azure;
using ERP.PURCHASES.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net;

namespace ERP
{
    [Route("API/[Controller]")]
    [ApiController]

    public class MaincategoryController : ControllerBase
    {
        ApplicationDbContext _context;

        private readonly IMapper _mapper;
        private readonly ImainCategoryRepo _maincategoryRepo;
        private readonly APIResponse _response;
        public MaincategoryController(ApplicationDbContext context,ImainCategoryRepo maincategoryRepo, IMapper mapper)
        {
            _maincategoryRepo = maincategoryRepo;
            _context = context; 
            _mapper = mapper;
            _response = new APIResponse();
        }


        [HttpGet]

        public async Task<ActionResult<APIResponse>> GetmaincategoryAsync([FromQuery] int pageNumber = 1, int pageSize = 10)
        {

            try
            {
                var paginatedResult = await _maincategoryRepo.getMainCategoryAsync(pageNumber, pageSize);

                _response.Result = new
                {

                    pagination = paginatedResult.Metadata,
                    Data = paginatedResult.Data,


                };
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);


            }
            catch (Exception ex)
            {


                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);

            }


        }


        [HttpGet("{id}")]

        public async Task<IActionResult> GetByidMaincategoryasync(Guid id)
        {
            var mcat = await _maincategoryRepo.GetByIdAsync(id);

            if (mcat == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                _response.Message = "object not found";
                return NotFound(_response);
            }

            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = mcat;
         

            return Ok(_response);
        }

        [HttpPost]

        public async Task<IActionResult> Addmaincategory([FromBody] MainCategoryDto createMcatgory)
        {
            try
            {

                  var result= await _maincategoryRepo.CreatemainCategoryAsync(createMcatgory);


                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = result;
                _response.Message = "";

                return Ok(_response);
            }
            catch (Exception ex)

            {
                return StatusCode(StatusCodes.Status500InternalServerError, new APIResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    IsSuccess = false,
                    ErrorMessages = new List<string> { ex.Message }
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMainCategory(Guid id, [FromQuery] MainCatUpdateDto updateMainCategoryDto)
        {
            if (updateMainCategoryDto == null)
            {
                return BadRequest("MainCatUpdateDto is null.");
            }

            try
            {



                var existingmaincat = await _context.Maincategories.FirstOrDefaultAsync(m => m.Id == id);

                if (existingmaincat == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.Message = "object not found";
                    return NotFound(_response);
                }




                await _maincategoryRepo.UpdateMainCategoryAsync(updateMainCategoryDto, id);

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Message = "object updated successfully";

                return Ok(_response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new APIResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    IsSuccess = false,
                    ErrorMessages = new List<string> { ex.Message }
                });
            }
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteMaincategoryasync(Guid id)
        {


            try
            {


                var existingmaincat = await _maincategoryRepo.GetByIdAsync(id);

                if (existingmaincat == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.Message = "object not found";
                    return NotFound(_response);
                }
            

                await _maincategoryRepo.RemoveMainCategoryAsync(id);

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Message = "object removed  successfully";

                return Ok(_response);

            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new APIResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    IsSuccess = false,
                    ErrorMessages = new List<string> { ex.Message }
                });
            }


        }


    }
}
