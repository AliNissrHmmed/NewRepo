using AutoMapper;
using Azure;
using ERP.PURCHASES.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Net;

namespace ERP
{
    [Route("API/[Controller]")]
    [ApiController]

    public class ItemsController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IItemsrepository _Itemsrepository;

        private readonly ApplicationDbContext _context;
        private readonly APIResponse _response;

        public ItemsController(IItemsrepository itemsrepository, IMapper mapper, ApplicationDbContext context)
        {
            _Itemsrepository = itemsrepository;

            _mapper = mapper;

            _context = context;

            _response = new APIResponse();  
        }


        [HttpGet]


        public async Task<ActionResult<APIResponse>> GetItems([FromQuery]int pagenumber=1,[FromQuery]int pagesize=10)
        {
            try
            {
                var paginatedResult = await _Itemsrepository.GetallItemsAsync(pagenumber, pagesize);

                _response.Result = new
                {
                    pagination = paginatedResult.Metadata,

                    Data = paginatedResult.Data
                    

                };
                _response.StatusCode = HttpStatusCode.OK;
                _response.Message = "Items retrieved successfully";
                _response.IsSuccess = true;





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
        public async Task<IActionResult> GetbyidItemasync(Guid id)
        {
            var item = await _Itemsrepository.GetByidItemsAsync(id);
            _response.StatusCode = HttpStatusCode.OK;
            _response.Message = "Items retrieved successfully";
            _response.IsSuccess = true;

            if (item == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Message = "Items not fund ";
                 
                return NotFound(_response);
            }

            _response.Result = item;
            return Ok(_response);
        }




        [HttpPost("createItems")]
        public async Task<ActionResult<APIResponse>> CreateItemsBulk([FromBody] IEnumerable<ItemsDto> itemsDtos)
        {
            try
            {
                if (itemsDtos == null || !itemsDtos.Any())
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.Message = "Error: No items provided for creation.";
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                var companyIds = itemsDtos.Select(item => item.company_id).Distinct();
                var validCompanyIds = await _context.Companies
                    .Where(c => companyIds.Contains(c.Id))
                    .Select(c => c.Id)
                    .ToListAsync();

                if (validCompanyIds.Count != companyIds.Count())
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new() { "One or more companyIds not found" };
                    return NotFound(_response);
                }

                var subcategoryIds = itemsDtos.Select(item => item.Subcategory_id).Distinct();
                var validSubcategoryIds = await _context.Subcategories
                    .Where(s => subcategoryIds.Contains(s.Id))
                    .Select(s => s.Id)
                    .ToListAsync();

                if (validSubcategoryIds.Count != subcategoryIds.Count())
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new() { "One or more Subcategory_id not found" };
                    return NotFound(_response);
                }

                // Call the service method to create the items and get the created items
                var createdItems = await _Itemsrepository.CreateItemsAsync(itemsDtos);

                _response.StatusCode = HttpStatusCode.Created;
                _response.IsSuccess = true;
                _response.Message = "Objects have been created";
                _response.Result = createdItems;
                return Ok(_response);
            }
            catch (ArgumentException ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItemasync(Guid id, [FromQuery] UpdateItemsDto updateItemsDto)
        {
            try
            {
               

                var existingItem = await _context.Items.AnyAsync(item=>item.Id==id);

                if (!existingItem)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.Message = "object not found";
                    return NotFound(_response);
                }



                await _Itemsrepository.UpdateItemAsync(updateItemsDto,id);

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
        public async Task<IActionResult> DeleteItemasync(Guid id)
        {
            try
            {
                var item = await _Itemsrepository.GetByIdAsync(id);

                if (item == null)
                {
                   
                    _response.Message = "item NotFound.";
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }

                await _Itemsrepository.RemoveAsync(item);

                return Ok(new APIResponse
                {
                    StatusCode = HttpStatusCode.OK,
                    IsSuccess = true,
                    Message = "Item deleted successfully"
                });
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
