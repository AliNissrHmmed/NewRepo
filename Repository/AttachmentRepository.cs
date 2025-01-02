using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace ERP
{  
    public class AttachmentRepository : Repository<Attachment>, IAttachmentRepository
{
        private readonly IWebHostEnvironment _hostingEnvironment;
        ApplicationDbContext _context;
        public AttachmentRepository(IWebHostEnvironment hostingEnvironment, ApplicationDbContext context):base(context)
        {
            _hostingEnvironment=hostingEnvironment;

            _context=context;


        }


        public async Task gettallattachAsync()

        {
            var res = await _context.Attachments.ToListAsync();
        }








    public async Task<Attachment> CreateAttachmentAsync(AttachmentDto attachmentDto)
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
                company_id= attachmentDto.company_id,
               // company_id = attachment.company_id,
              //  CreatedAt= attachmentDto.CreatedAt,
                user_id = attachmentDto.user_id,
                state= attachmentDto.state,
                url= combinedFilePaths
            };



            // Save to the database
            _context.Attachments.Add(attachment);
            await _context.SaveChangesAsync();

            return attachment;
        }

        public async Task<Attachment> UpdateAttachmentAsync(UpdateAttachmentDto updateAttachmentDto,Guid attachmentId)
        {

            var existingAttachment = await _context.Attachments.FindAsync(attachmentId);

            if (existingAttachment == null)
            {
                throw new KeyNotFoundException("Attachment not found.");
            }


            if (updateAttachmentDto.url == null || !updateAttachmentDto.url.Any())
            {
                throw new ArgumentException("At least one file is required.");
            }

            var filePaths = new List<string>();

            foreach (var file in updateAttachmentDto.url)
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



 
            existingAttachment.company_id = updateAttachmentDto.company_id;
            
            existingAttachment.user_id = updateAttachmentDto.user_id;
            existingAttachment.state = updateAttachmentDto.state;
            existingAttachment.url = combinedFilePaths;
            



            // Save to the database
            _context.Attachments.Update(existingAttachment);
            await _context.SaveChangesAsync();

            return existingAttachment;  

           
        }
    }
}
