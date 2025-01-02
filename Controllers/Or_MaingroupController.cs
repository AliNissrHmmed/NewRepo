using AutoMapper;
using Azure;
using ERP.PURCHASES.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ERP.PURCHASES.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Or_MaingroupController : ControllerBase
    {

       /* ApplicationDbContext _context;

        private readonly IMapper _mapper;
        private readonly IOr_maingroupRepo _MaingroupRepo;
        private readonly APIResponse _response;
           
        public Or_MaingroupController(ApplicationDbContext context, IOr_maingroupRepo MaingroupRepo, IMapper mapper)
        {
            _MaingroupRepo = MaingroupRepo;
            _context = context;
            _mapper = mapper;

            _response = new APIResponse();
        }

       
        [HttpGet]

        public async Task<ActionResult<APIResponse>> GetMaingroupAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {

            try
            {
                var paginatedResult = await _MaingroupRepo.GetMainGroupAsync(pageNumber, pageSize);

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
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var r = await _MaingroupRepo.GetByIdAsync(id);


            if (r == null)
            {
                return NotFound(" error object not found");
            }

            return Ok(r);
        }

        [HttpPost]

        public async Task<IActionResult> CreateMaingroupAsync(Or_maingroupDto or_MaingroupDto)

        {
            await _MaingroupRepo.CreatemainMainGroupAsync(or_MaingroupDto);

            return Ok();

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMaingroupAsync(Guid id,Update_maingroupDto update_maingroupDto)


        {
            var Updmg = await _MaingroupRepo.GetByIdAsync(id);


            if (Updmg == null)
            {
                return NotFound();
            }

            _mapper.Map(update_maingroupDto, Updmg);
            await _MaingroupRepo.UpdateAsync(Updmg);

            return Ok();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteMaingroupAsync(Guid id)
        {
            var result = await _MaingroupRepo.GetByIdAsync(id);

            if (result==null)
            {
                return NotFound();
            }


            await _MaingroupRepo.RemoveAsync(result);
            return Ok();



        }
*/

    }
}
