using AutoMapper;
using Azure;
using ERP.PURCHASES.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ERP
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase

    {
        ApplicationDbContext _context;

        private readonly ISubCategoryRepo _subCategoryRepo;
        private readonly IMapper _mapper;
        private readonly APIResponse _response;

        public SubCategoryController(ISubCategoryRepo subCategoryRepo, IMapper mapper, ApplicationDbContext context)
        {
            _subCategoryRepo = subCategoryRepo;
            _mapper = mapper;
            _context = context;
            _response = new APIResponse();
        }


        [HttpGet("{id}")]

        public async Task<IActionResult> Getbyidsubcatasync(Guid id)
        {
           

              
            try
            {


                var sub = await _subCategoryRepo.GetByIdAsync(id);

                

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = sub;

               

                if (sub == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.Message = "object not found";
                    return NotFound(_response);
                }
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



        [HttpGet]

        public async Task<ActionResult<APIResponse>> GetSubcategoryasync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {

            try
            {
                var paginatedResult = await _subCategoryRepo.getsubCategoryAsync(pageNumber, pageSize);

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

        [HttpPost]

        public async Task<IActionResult> Createsupcategoryasync([FromBody] Sub_categoryDto sub_CategoryDto)

        {
            try
            {

                if (sub_CategoryDto == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.Message = "object not found";
                    return NotFound(_response);
                }


               var result= await _subCategoryRepo.CreateSubCategoryAsync(sub_CategoryDto);


                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = result;

                _response.Message = "object created successfully";

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

        public async Task<IActionResult> UpdateSupcategoryasync(Guid id,[FromQuery]UpdateSub_categoryDto updateSub_CategoryDto )
        {
            try

            {



                var subCategory = await _context.Subcategories.FirstOrDefaultAsync(r => r.Id == id);

                if (subCategory == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.Message = "object not found";
                    return NotFound(_response);
                }


                await _subCategoryRepo.UpdateSub_categoryAsync(updateSub_CategoryDto, id);


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
                    ErrorMessages = new List<string> { ex.Message }
                });
            }
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteSupcategoryasync(Guid id)
        {

            try

            {



                var subCategory = await _context.Subcategories.FirstOrDefaultAsync(r => r.Id == id);

                if (subCategory == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.Message = "object not found";
                    return NotFound(_response);
                }

                await _subCategoryRepo.RemoveAsync(subCategory);

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Message = "object removed successfully";

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
