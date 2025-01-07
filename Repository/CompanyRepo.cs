 using ERP;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Net.Mail;
using ERP.PURCHASES.Dto;
using Microsoft.EntityFrameworkCore.Storage;
using Azure;
using System.Net;

namespace ERP
{



    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _context;
         
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly APIResponse _response;

        public CompanyRepository(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment) :base(context)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _response = new APIResponse();
        }



        public async Task<PaginatedResult<CompanyWithAttachmentsDto>> GetCompaniesWithAttachmentsAsync(int pageNumber = 1, int pageSize = 25)
        {
            int totalCount = await _context.Companies.CountAsync();
            var baseUrl = "http://localhost:5200/";
            var companies = await _context.Companies
                .Include(c => c.Attachments)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var companyDtos = companies.Select(company => new CompanyWithAttachmentsDto
            {
                Id = company.Id,
                Name = company.name,
                Phone = company.phone,
                Phone2 = company.phone2,
                Address = company.address,
                CreatedAt = company.CreatedAt,
                UserId = company.user_id,
                OrganizationId = company.Organization_id,
                State = company.State,
                MaincategoryId = company.Maincategory_id,

                AttachmentUrls= company.Attachments
                .Where(a => !string.IsNullOrEmpty(a.url))
                .Select(a => $"{baseUrl}{a.url}")
                .ToList()
            //AttachmentUrls = company.Attachments.Select(a => a.url).ToList()

        }).ToList();

            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var paginationData = new PaginationData
            {
                totalCount = totalCount,
                pageSize = pageSize,
                currentPage = pageNumber,
                totalPages = totalPages
            };

            // Return paginated result with data and metadata
            return new PaginatedResult<CompanyWithAttachmentsDto>
            {
                Data = companyDtos,
                Metadata = paginationData
            };
        }



        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public async Task<PaginatedResult<Company>> GetPaginatedCompaniesAsync( int pageNumber = 1, int pageSize = 25)
       
        {
            int totalCount = await _context.Companies.CountAsync();

            var companies = await _context.Companies
                .Include(c => c.Attachments)

                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();


            // Access the url property from each Attachment
            foreach (var company in companies)
            {
                foreach (var attachment in company.Attachments)
                {
                    var url = attachment.url;
                    // Do something with the url
                }
            }

            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var paginationData = new PaginationData
            {
                totalCount = totalCount,
                pageSize = pageSize,
                currentPage = pageNumber,
                totalPages = totalPages
            };

            // Return paginated result with data and metadata
            return new PaginatedResult<Company>
            {
                Data = companies,
                Metadata = paginationData
            };
        }

    

       

       //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<bool> GetByIdAsync(Guid id)
        {


            bool company = await _context.Companies

                .Include(c => c.Attachments)

                .Include(c => c.Maincategories)
                .Include(c => c.Items)

                .AnyAsync(c => c.Id == id);

           // if (!result.AnyAsync())

                return company;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task CreateCompanyWithAttachmentsAsync(CompanyDto companyDto, IEnumerable<IFormFile> files)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Step 1: Create Company entity
                var company = new Company
                {
                    name = companyDto.name,
                    phone = companyDto.phone,
                    phone2 = companyDto.phone2,
                    address = companyDto.address,
                    CreatedAt = companyDto.CreatedAt,
                    user_id = companyDto.user_id,
                    Organization_id = companyDto.Organization_id,
                    State = companyDto.State,
                    Maincategory_id = companyDto.Maincategory_id
                };

                await _context.Companies.AddAsync(company);
                await _context.SaveChangesAsync();
                var attachments = new List<Attachment>();

                // Step 2: Process Attachments if provided
                if (files != null && files.Any())
                {

                    foreach (var file in files)
                    {
                        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                        var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", fileName);

                        // Save file to server
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                        // Create attachment object
                        attachments.Add(new Attachment
                        {
                            company_id = company.Id, // FK after company creation
                            user_id = companyDto.user_id,
                            state = true, // Default state
                            url = Path.Combine("uploads",fileName), // File path saved in the database

                            CreatedAt = DateTime.Now
                        });
                    }

                    // Save all attachments to the database
                    await _context.Attachments.AddRangeAsync(attachments);
                }

                // Commit transaction
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
               
                 
            }
            catch (Exception ex)
            {
                if (transaction.GetDbTransaction().Connection != null)
                {
                    await transaction.RollbackAsync();
                }
          
                throw new InvalidOperationException("An error occurred while creating the company with attachments.", ex);
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<APIResponse> UpdateCompanyAsync(CompanyUpdateDto companyUpdateDto, IEnumerable<IFormFile> files, Guid id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Fetch the existing company
                var existingCompany = await _context.Companies.FindAsync(id);

                if (existingCompany == null)
                {
                    throw new InvalidOperationException("Company not found.");
                }


                // Check if the subcategory is valid if a new value is provided
                if (companyUpdateDto.Maincategory_id.HasValue)
                {
                    var isMaincategory_idValid = await _context.Maincategories
                        .AnyAsync(s => s.Id == companyUpdateDto.Maincategory_id.Value);

                    if (isMaincategory_idValid)
                    {
                        existingCompany.Maincategory_id = companyUpdateDto.Maincategory_id.Value;
                    }
                }





                // Update company details
                existingCompany.name = companyUpdateDto.name ?? existingCompany.name;
                existingCompany.phone = companyUpdateDto.phone ?? existingCompany.phone;
                existingCompany.phone2 = companyUpdateDto.phone2 ?? existingCompany.phone2;
                existingCompany.address = companyUpdateDto.address ?? existingCompany.address;
                existingCompany.State = companyUpdateDto.State ?? existingCompany.State;
               // existingCompany.Maincategory_id = companyUpdateDto.Maincategory_id ?? existingCompany.Maincategory_id;



                _context.Companies.Update(existingCompany);

                if (files != null && files.Any())
                {
                    var Attach = await _context.Attachments.Where(a => a.company_id == id).ToListAsync();

                    if (Attach != null && Attach.Any())
                    {
                        foreach (var attachment in Attach)
                        {
                            var oldFilePath = Path.Combine(_hostingEnvironment.WebRootPath, attachment.url);
                            if (System.IO.File.Exists(oldFilePath))
                            {
                                System.IO.File.Delete(oldFilePath); // Delete old image file
                            }
                        }

                        _context.Attachments.RemoveRange(Attach);
                    }

                    var attachments = new List<Attachment>();

                    foreach (var file in files)
                    {
                        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                        var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", fileName);

                        // Save file to server
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                        // Create attachment object
                        attachments.Add(new Attachment
                        {
                            company_id = id, // FK after company creation
                            user_id = companyUpdateDto.user_id,
                            state = true, // Default state
                            url = Path.Combine("uploads", fileName), // File path saved in the database
                            CreatedAt = DateTime.Now
                        });
                    }

                    // Save all attachments to the database
                    await _context.Attachments.AddRangeAsync(attachments);
                    
                }

                // Commit transaction
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return _response;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new InvalidOperationException("An error occurred while updating the company with attachments.", ex);
            }
        }



       ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task DeleteCompanyAsync(Guid id)
        {
                var company = await _context.Companies.Where(a => a.Id == id).ToListAsync();


            if (company == null || !company.Any())
            {
                throw new Exception();
            }

            var attachments = await _context.Attachments.Where(a => a.company_id == id).ToListAsync();
            if (attachments.Any())
            {
               

              
             
                
                    foreach (var attachment in attachments)
                    {
                        var oldFilePath = Path.Combine(_hostingEnvironment.WebRootPath, attachment.url);
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath); // Delete old image file
                        }
                    }

                    _context.Attachments.RemoveRange(attachments);
                    await _context.SaveChangesAsync();
                }


            

            // Delete related items
            var items = await _context.Items.Where(i => i.company_id == id).ToListAsync();
            if (items.Any())
            {
                _context.Items.RemoveRange(items);
                await _context.SaveChangesAsync();
            }


              _context.Companies.RemoveRange(company);
            await _context.SaveChangesAsync();

            
        }


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}