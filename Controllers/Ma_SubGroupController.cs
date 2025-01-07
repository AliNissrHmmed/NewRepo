/*using AutoMapper;
using Azure;
using ERP.PURCHASES.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ERP.PURCHASES.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Ma_SubGroupController : ControllerBase


    {


        ApplicationDbContext _context;

        private readonly IMapper _mapper;
        private readonly IMaSubgroupRepo _MaSubGroupRepo;
        private readonly APIResponse _response;

        public Ma_SubGroupController(ApplicationDbContext context, IMapper mapper, IMaSubgroupRepo maSubgroupRepo)
        {
            _context = context;
            _MaSubGroupRepo = maSubgroupRepo;
            _mapper = mapper;
            _response=new APIResponse();    
        }


  

        [HttpGet]

        public async Task<ActionResult<APIResponse>> GetSubGroupAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {

            try
            {
                var paginatedResult = await _MaSubGroupRepo.getSubGroupAsync(pageNumber, pageSize);

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


        public async Task<IActionResult> GetSubbyidAsync(Guid id)

        {

           var sub= await _MaSubGroupRepo.GetByIdAsync(id);

            if(sub ==null)

            {

                return NotFound();
            }

            return Ok(sub);

        }


        [HttpPost]

        public async Task<IActionResult> CreateSubGroupAsync(MaSubgroupDto maSubgroupDto)

        {
           await _MaSubGroupRepo.CreateMa_SubgroupAsync(maSubgroupDto);

            return Ok();

        }

        [HttpPut("{id}")]


        public async Task<IActionResult> UpdateSubGroupAsync(Guid id,Update_MaSubgroupDto UpdatemaSubgroupDto)

        {
            var UpSgp=await _MaSubGroupRepo.GetByIdAsync(id);

            if (UpSgp == null)
            {
                return NotFound();

            }


            _mapper.Map( UpdatemaSubgroupDto, UpSgp);

            await _MaSubGroupRepo.UpdateAsync(UpSgp);

            return Ok();


        }


        [HttpDelete("{id}")]


        public async Task<IActionResult> DeleteSubGroupAsync(Guid id)

        {
            var UpSgp = await _MaSubGroupRepo.GetByIdAsync(id);

            if (UpSgp == null)
            {
                return NotFound();

            }

            await _MaSubGroupRepo.RemoveAsync(UpSgp);

            return Ok();


        }


    }
}
*/