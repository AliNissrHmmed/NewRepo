using AutoMapper;
using ERP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.Design;

namespace ER 
{
    [Route("api/[controller]")]
    [ApiController]
    public class Attachmentcontroller : ControllerBase
    {
        ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public Attachmentcontroller(ApplicationDbContext context, IAttachmentRepository attachmentRepository, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _attachmentRepository = attachmentRepository;

            _mapper = mapper;

            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        
        [HttpGet("images/{id}")]
        public async Task<IActionResult> GetImages(Guid id)
        {
            var attachments = await _context.Attachments
                .Where(a => a.company_id == id)
                .ToListAsync();

            if (attachments == null || !attachments.Any())
            {
                return NotFound("Attachments not found.");
            }

            var baseUrl = "http://localhost:5200/";

            var validImageUrls = attachments
                .Where(a => !string.IsNullOrEmpty(a.url))
                .Select(a => $"{baseUrl}{a.url}")
                .ToList();

            if (!validImageUrls.Any())
            {
                return NotFound("No valid image URLs found.");
            }

            return Ok(validImageUrls);
        }



        private string GetContentType(string path)
        {
            var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(path, out string? contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }


        [HttpPost]

        public async Task<Attachment> CreateAttachmentAsync(AttachmentDto attachmentDto)

        {
          var result= await _attachmentRepository.CreateAttachmentAsync(attachmentDto);


            return result;


        }


        [HttpPut]

        public async Task<IActionResult> UpdateAttachmentAsync([FromForm] UpdateAttachmentDto updateAttachmentDto,Guid id)

        {
            try
            {
                var result = await _attachmentRepository.UpdateAttachmentAsync(updateAttachmentDto, id);

                return Ok(result);  
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
        

        [HttpGet]
        public async Task<IActionResult> GetAttachmentAsync(  )
        {

            //var res =  _attachmentRepository.gettallattachAsync();
            var atch = await _context.Attachments.ToListAsync();

            return Ok(atch);

        }

    }
}
