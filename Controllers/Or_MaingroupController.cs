/*using AutoMapper;
using Azure;
using ERP.PURCHASES.Dto;
using ERP.PURCHASES.Interfaces;
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

        //aoto save check
        ApplicationDbContext _context;

        private readonly IMapper _mapper;
        private readonly IMain_Groups_Repository _MaingroupRepo;
        private readonly APIResponse _response;

        public Or_MaingroupController(ApplicationDbContext context, IMain_Groups_Repository MaingroupRepo, IMapper mapper)
        {
            _MaingroupRepo = MaingroupRepo;
            _context = context;
            _mapper = mapper;

            _response = new APIResponse();
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMaingroupAsync(Guid id,[FromQuery]Update_maingroupDto update_maingroupDto)
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
        public async Task<IActionResult> UpdateSubgroupAsync(Guid id,[FromQuery]UpdateSubgroupDto update_SubgroupDto )
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





        *//*

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
                public async Task<IActionResult> UpdateMaingroupAsync(Guid id, Update_maingroupDto update_maingroupDto)


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

                    if (result == null)
                    {
                        return NotFound();
                    }


                    await _MaingroupRepo.RemoveAsync(result);
                    return Ok();



                }*//*


    }
    }
*/