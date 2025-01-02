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

        /* [HttpGet("image/{id}")]
         public async Task<IActionResult> GetImage(Guid id)
         {


             var attachment = await _context.Attachments
              .Where(a => a.company_id == id)
                .ToListAsync();

             //var attachment= await _context.Attachments.FirstOrDefaultAsync(a => a.company_id == id);

             if (attachment == null || string.IsNullOrEmpty(attachment.url))
             {
                 return NotFound("Attachment not found.");
             }

             var filePath = Path.Combine(_hostingEnvironment.WebRootPath, attachment.url);

             if (!System.IO.File.Exists(filePath))
             {
                 return NotFound("Image file not found.");
             }

             var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
             var contentType = GetContentType(filePath);

             return File(fileStream, contentType);
         }

         private string GetContentType(string path)
         {
             var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
             if (!provider.TryGetContentType(path, out string contentType))
             {
                 contentType = "application/octet-stream";
             }
             return contentType;
         }

 */







        /*  [HttpGet("images/{id}")]
          public async Task<IActionResult> GetImage(Guid id)
          {
              var attachments = await _context.Attachments
                  .Where(a => a.company_id == id)
                  .ToListAsync();

              if (attachments == null || !attachments.Any())
              {
                  return NotFound("Attachments not found.");
              }

              foreach (var attachment in attachments)
              {
                  if (!string.IsNullOrEmpty(attachment.url))
                  {
                      var filePath = Path.Combine(_hostingEnvironment.WebRootPath, attachment.url);

                      if (System.IO.File.Exists(filePath))
                      {
                          var contentType = GetContentType(filePath);
                          var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                          return File(fileStream, contentType);
                      }
                  }
              }

              return NotFound("Image files not found.");
          }*/

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
        /*
                [HttpPost]
                public async Task<Attachment> CreateAttachmentAsync(  AttachmentDto attachmentDto)
                {
                    if (attachmentDto.url == null || !attachmentDto.url.Any())
                    {
                        throw new ArgumentException("At least one file is required.");
                    }

                    var filePaths = new List<string>();

                    foreach (var file in attachmentDto.url)
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", fileName);

                        // Upload the file
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                        filePaths.Add(fileName);
                    }

                    var combinedFilePaths = string.Join(",", filePaths);



                    var attachment = new Attachment
                    {
                        company_id = attachmentDto.company_id,
                        // company_id = attachment.company_id,
                       // CreatedAt = attachmentDto.CreatedAt,
                        user_id = attachmentDto.user_id,
                        state = attachmentDto.state,
                        url = combinedFilePaths
                    };



                    // Save to the database
                    _context.Attachments.Add(attachment);
                    await _context.SaveChangesAsync();

                    return attachment;
                }*/

/*
        [HttpGet("{companyId}")]
        public async Task<IActionResult> GetAttachmentUrls(Guid companyId)
        {
            var urls = await _context.Attachments
                .Where(a => a.company_id == companyId) // Filter by company ID if needed
                .Select(a => a.url) // Select only URLs
                .ToListAsync();

            return Ok(urls);
        }
*/

        [HttpGet]
        public async Task<IActionResult> GetAttachmentAsync(  )
        {

            //var res =  _attachmentRepository.gettallattachAsync();
            var atch = await _context.Attachments.ToListAsync();

            return Ok(atch);

        }

    }
}
