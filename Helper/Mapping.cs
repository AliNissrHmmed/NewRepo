using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace ERP

{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
           
            CreateMap<Company, CompanyDto>().ReverseMap();
            CreateMap<Company, CompanyUpdateDto>().ReverseMap();

            CreateMap<Attachment, AttachmentDto>().ReverseMap();

            CreateMap<Item,ItemsDto>().ReverseMap();
            CreateMap<Item, UpdateItemsDto>().ReverseMap();

            CreateMap< Maincategory, MainCategoryDto>().ReverseMap();
            CreateMap<Maincategory, MainCatUpdateDto>().ReverseMap();
            
            CreateMap<Subcategory, Sub_categoryDto>().ReverseMap();
            CreateMap<Subcategory, UpdateSub_categoryDto>().ReverseMap();


            CreateMap<Rate, CreateIt_RateDto>().ReverseMap();
            CreateMap<Rate, UpdateIt_RateDto>().ReverseMap();

            CreateMap<Section, SectionDto>().ReverseMap();
            CreateMap<Section, UpdateSectionDto>().ReverseMap();

            CreateMap<Ma_Subgroup, MaSubgroupDto>().ReverseMap();
            CreateMap<Ma_Subgroup, Update_MaSubgroupDto>().ReverseMap();

            CreateMap<Or_Maingroup, Or_maingroupDto>().ReverseMap();
            CreateMap<Or_Maingroup, Update_maingroupDto>().ReverseMap();
            CreateMap<Or_Maingroup, AddMainGroupDto>().ReverseMap();
 


        }
    }
}
