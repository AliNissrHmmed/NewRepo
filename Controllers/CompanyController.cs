using AutoMapper;
using Azure;
using ERP.PURCHASES.Dto;
 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ERP
{
    [Route("API/[Controller]")]
    [ApiController]

    public class CompanyController : ControllerBase
    {
        ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepo;
        private readonly IAttachmentRepository _attachmentRepo;
        private readonly IItemsrepository _itemRepo;
        private readonly ISubCategoryRepo _subcategoryRepo;
        private readonly ImainCategoryRepo _imainCategoryRepo;
         
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly APIResponse _response;

        public CompanyController(  ApplicationDbContext context, ICompanyRepository companyrepo, IMapper mapper,

            IAttachmentRepository attachmentRepository, IItemsrepository itemsrepository, ImainCategoryRepo mainCategoryRepo, IWebHostEnvironment webHostEnvironment)
        {
            _companyRepo = companyrepo;
            _response = new APIResponse();
            _attachmentRepo = attachmentRepository;
            _itemRepo = itemsrepository;
            _imainCategoryRepo = mainCategoryRepo;

            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;

            _context = context;
             
        }




        [HttpGet("GetCompany")]
        public async Task<ActionResult<APIResponse>> GetCompanies([FromQuery] int pageNumber = 1, int pageSize = 10)
        {

            try
            {
                var paginatedResult = await _companyRepo.GetPaginatedCompaniesAsync(pageNumber, pageSize);

                _response.Result = new
                {

                    pagination = paginatedResult.Metadata,
                    Data = paginatedResult.Data,


                };
              //  _response.Message = "Company updated successfully";

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
        public async Task<IActionResult> GetCompanyById(Guid id)
        {

            try
            {
                var company = await _companyRepo.GetByIdAsync(id);
                if (company == null)
                {
                    //return NotFound($"Company with ID {id} not found.");

                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new() { "NotFound" };
                    return NotFound(_response);

                }
                else
                 _response.Message = "Company updated successfully";
                _response.StatusCode = HttpStatusCode.OK;

                _response.Result = company;
                return Ok(_response);


            }
            catch (Exception ex)
            {


                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);

            }
        }


        


        [HttpPost("CreateCompanyWithAttachments")]
        public async Task<ActionResult<APIResponse>> CreateCompanyWithAttachments([FromForm] CompanyDto companyDto)
        {


            try
            {
                if (companyDto == null|| !companyDto.Attachments.Any())
                {

                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new() { "error : enter the objects " };

                    return BadRequest(_response);


                }
                var isMainCategoryValid = await _context.Maincategories
                    .AnyAsync(m => m.Id == companyDto.Maincategory_id);


                if (!isMainCategoryValid)
                {

                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new() { "Maincategory_id NotFound" };
                    return _response;
                }


                await _companyRepo.CreateCompanyWithAttachmentsAsync(companyDto, companyDto.Attachments);

                  

                _response.StatusCode = HttpStatusCode.Created;
                _response.IsSuccess = true;
                _response.Message = "object has been created";
                return Ok(_response);


            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                // _response.ErrorMessages = new() { "One or more validation errors occurred" };
                _response.ErrorMessages = new List<string> { ex.ToString() };


            }
            return _response;
        }

                        

        [HttpPut("{companyId}")]
        public async Task<ActionResult<APIResponse>> UpdateCompany(Guid companyId, [FromQuery] CompanyUpdateDto companyDto)
        {
            try
            {
                if (companyDto == null)
                {
                    _response.Message = "Invalid company data.";
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                var companyexisted = await _context.Companies.AnyAsync(a => a.Id == companyId);

                if (!companyexisted)
                {
                    _response.StatusCode = HttpStatusCode.ExpectationFailed;
                    _response.IsSuccess = false;
                    _response.Message = "object not fund";
                    return BadRequest(_response);
                }


                
                    await _companyRepo.UpdateCompanyAsync(companyDto, companyDto.Attachments, companyId);

                    _response.StatusCode = HttpStatusCode.OK;
                    _response.IsSuccess = true;
                _response.Message = "Company updated successfully";
                    return Ok(_response);
                



            }
            catch (Exception ex)
            {

                _response.StatusCode = HttpStatusCode.ExpectationFailed;
                _response.IsSuccess = false;
                _response.Message = "enter object is not valid";
                _response.ErrorMessages = new List<string> { ex.ToString() };

            }

            return _response;
        }






        [HttpDelete("{id}")]
        public async Task<ActionResult<APIResponse>> DeleteCompanyAsync(Guid id)
        {

            try
            {
                var company = await _context.Companies.Where(a => a.Id == id).ToListAsync();


                if (company == null||!company.Any())
                {


                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;

                    _response.Message = "object not fund";
                    return BadRequest(_response);



                }



                if (id == null)
                {
                    return BadRequest();
                }

                await _companyRepo.DeleteCompanyAsync(id);



                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Message = "object deleted successufully ";

                return Ok(_response);


            }

            catch (Exception ex)
            {

                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;



                _response.ErrorMessages = new List<string> { ex.ToString() };

            }
            return _response;

        }




    }
}
