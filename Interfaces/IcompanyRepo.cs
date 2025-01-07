
using ERP.PURCHASES.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ERP
{
    public interface ICompanyRepository : IRepository<Company> 
    {
        public Task<PaginatedResult<CompanyWithAttachmentsDto>> GetCompaniesWithAttachmentsAsync(int pageNumber = 1, int pageSize = 25);
        public  Task<PaginatedResult<Company>> GetPaginatedCompaniesAsync(int pageNumber = 1, int pageSize = 25);
        public Task<Company?> GetByIdAsync(Guid id);

        public Task DeleteCompanyAsync(Guid id);

        // public Task CreateCompanyWithAttachmentsAsync(CompanyDto companyDto, IEnumerable<IFormFile> files);
        public Task CreateCompanyWithAttachmentsAsync(CompanyDto companyDto, IEnumerable<IFormFile> files);

        public Task<APIResponse> UpdateCompanyAsync(CompanyUpdateDto companyUpdateDto, IEnumerable<IFormFile> files, Guid id);

        



    }
}
