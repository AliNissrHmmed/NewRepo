namespace ERP 
{
    public class AddMainGroupDto
    {


        public string MainGroupName { get; set; } = string.Empty;
        public bool State { get; set; }
        public List<SubgroupDto> Subgroups { get; set; } = new List<SubgroupDto>();
    }

    public class SubgroupDto
    {
        public bool SupTreeGroup { get; set; }
        public bool State { get; set; }
        public Guid SectionId { get; set; }
        public string Note { get; set; }
        public string TypeItem { get; set; }
    }


    public class UpdateSubgroupDto
    {
        public bool? SupTreeGroup { get; set; }
        public bool? State { get; set; }
        public Guid? SectionId { get; set; }
        public string? Note { get; set; } 
        public string? TypeItem { get; set; }
    }





    public class Or_maingroupDto
    {


        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid UserId { get; set; }
        public bool State { get; set; }


    }


    public class Update_maingroupDto
    {


        public string? Name { get; set; }
        //public string? Code { get; set; }
        public bool? State { get; set; }


    }

}
