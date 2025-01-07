using AutoMapper;
using Azure;
using ERP.PURCHASES.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using static System.Collections.Specialized.BitVector32;


namespace ERP
{
    [Route("api/[controller]")]
[ApiController]
public class Or_SectionController : ControllerBase
{

        ApplicationDbContext _context;
        private readonly IMapper _mapper;

        private readonly IOr_SectionRepo _or_sectionRepo;
        private readonly  APIResponse _response;
        public Or_SectionController(IOr_SectionRepo or_sectionRepo, ApplicationDbContext context, IMapper mapper)
        {
            _or_sectionRepo=or_sectionRepo;

            _context=context;
            _mapper=mapper;

            _response = new APIResponse();

        }



        [HttpGet]

        public async Task<ActionResult<APIResponse>> GetSectionAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {

            try
            {
                var paginatedResult = await _or_sectionRepo.getSectionAsync(pageNumber, pageSize);

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

      

    // GET api/<Or_SectionController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(Guid id)
    {

            try
            {
                var r = await _or_sectionRepo.GetByIdAsync(id);

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = r;
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

    // POST api/<Or_SectionController>
    [HttpPost]
    public async Task<IActionResult> createsectionAsync( SectionDto createSectionDto)

        {
            
            try
            {

             var result=  await _or_sectionRepo.createsectionAsync(createSectionDto);

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = result;
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

  
        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateSectionAsync(Guid id, [FromQuery] UpdateSectionDto updateSection)
        {


            try
            {


                var existinsection = await _context.Sections.FirstOrDefaultAsync(r => r.Id == id);

                if (existinsection == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.Message = "object not found";
                    return NotFound(_response);
                }


                await _or_sectionRepo.UpdateSectionAsync(updateSection,id);


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

        // DELETE api/<Or_SectionController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSectionAsync(Guid id)
    {
       var section = await _or_sectionRepo.GetByIdAsync(id);

            if (section == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                _response.Message = "object not found";
                return NotFound(_response);
               
            }


            var item = await _context.Ma_Subgroups.Where(i => i.SectionId == id).ToListAsync();
            if (item.Any())
            {
                _context.Ma_Subgroups.RemoveRange(item);

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Message = "Item deleted successfully";

                return Ok(_response);
            }




            await _or_sectionRepo.RemoveAsync(section);

            return Ok();
        }
    }
}
