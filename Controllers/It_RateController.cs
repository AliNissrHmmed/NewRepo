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
    
    public class It_RateController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IIt_RateRepo _It_RateRepo;
        private readonly APIResponse _response;
        public It_RateController(IIt_RateRepo it_RateRepo, IMapper mapper, ApplicationDbContext context)
        {
            _It_RateRepo = it_RateRepo;
            _context = context;
            _response = new APIResponse(); 
            _mapper = mapper;
        }



        [HttpGet("{id}")]


        public async Task<IActionResult> getIt_RateByidasync(Guid id)
        {
            var rate = await _It_RateRepo.GetByIdAsync(id);
            if (rate == null)
            {
                return NotFound();
            }

            return Ok(rate);

        }

        [HttpGet]

        public async Task<ActionResult<APIResponse>> GetRatesAsync([FromQuery] int pageNumber = 1, int pageSize = 10)
        {

            try
            {
                var paginatedResult = await _It_RateRepo.GetallRatesAsync(pageNumber, pageSize);

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

        public async Task<ActionResult<APIResponse>> AddItRateasync([FromBody]CreateIt_RateDto createIt_RateDto)
        {

            try { 

            if (createIt_RateDto == null)
            {

                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new() { "error : enter the objects " };

                return BadRequest(_response);


            }


             var result=  await _It_RateRepo.CreateIt_RateAsync(createIt_RateDto);

            _response.StatusCode = HttpStatusCode.Created;
            _response.IsSuccess = true;
            _response.Result = result;    
            _response.Message = "object has been created";
            return Ok(_response);


                }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                // _response.ErrorMessages = new() { "One or more validation errors occurred" };
                _response.ErrorMessages = new List<string> { ex.ToString()
                  };


                    }
             return _response;

        }


        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateRateasync(Guid id,[FromQuery] UpdateIt_RateDto updateit_Ratedto)
        {


            try
            {


                var existingrate= await _context.Rates.FirstOrDefaultAsync(r => r.Id == id);

                if (existingrate==null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.Message = "object not found";
                    return NotFound(_response);
                }


               // _mapper.Map(updateit_Ratedto, existingrate);
                await _It_RateRepo.UpdateRateAsync(updateit_Ratedto, id);
         

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

        public async Task<IActionResult> Rateasync(Guid id)
        {
            try
            {
                var rate = await _It_RateRepo.GetByIdAsync(id);

                if (rate == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.Message = "object not found";
                    return NotFound(_response);
                }

                await _It_RateRepo.RemoveAsync(rate);


                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Message = "Item removed successfully";

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
